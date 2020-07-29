using RightControl.Common;
using RightControl.IService;
using RightControl.Model;
using RightControl.WebApp.Areas.Admin.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RightControl.IService.Permissions;
using System.Text;
using RightControl.IRepository.RecordTrancation;
using RightControl.Model.Permissions;
using Newtonsoft.Json;

namespace RightControl.WebApp.Areas.Permissions.Controllers
{
    public class UserController : BaseController
    {
        public IUserService userService { get; set; }
        public IRoleService roleService { get; set; }
        public IDepartmentService departmentService { get; set; }
        public IDepartmentUserService departmentUserService { get; set; }
        public SelectList RoleList { get { return new SelectList(roleService.GetRoleList(), "Id", "RoleName"); } }

        // GET: Permissions/User
        public override ActionResult Index(int? id)
        {
            ViewBag.RoleId = RoleList;
            base.Index(id);
            return View();
        }

        /// <summary>
        /// 加载数据列表
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult List(UserModel filter, PageInfo pageInfo)
        {
            var result = userService.GetListByFilter(filter, pageInfo);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Detail(int Id)
        {
            var model = userService.GetDetail(Id);
            return View(model);
        }

        public ActionResult Edit(int Id)
        {
            var model = userService.ReadModel(Id);
            var departmentuser = departmentUserService.GetDepartmentByUserId(model.Id);
            if (departmentuser != null)
            {
                var department = departmentService.ReadModel(departmentuser.DepartmentId);
                model.DepartmentName = department.DepartmentName;
                model.DepartmentId = department.Id;
            }
            ViewBag.RoleId = RoleList;
            return View(model);
        }

        public ActionResult GetRoleList()
        {
            var data = roleService.GetRoleList().Select(i => new { Id = i.Id, RoleName = i.RoleName }).ToList();

            return Content(JsonConvert.SerializeObject(data), "application/json");
        }

        //public ActionResult GetOwnedRoleList()
        //{

        //}

        [HttpPost]
        public ActionResult Edit(UserModel model)
        {
            model.UpdateOn = DateTime.Now;
            model.UpdateBy = Operator.UserId;

            var departmentuser = departmentUserService.GetByWhere("where UserId=@UserId", new {UserId=model.Id}).FirstOrDefault();
            if (departmentuser != null)
            {
                departmentuser.DepartmentId = model.DepartmentId;
                departmentUserService.UpdateModel(departmentuser);
            }
            else
            {
                departmentUserService.CreateModel(new DepartmentUserModel()
                {
                    DepartmentId = model.DepartmentId,
                    UserId = model.Id
                });
            }
            
            var result = userService.UpdateModel(model) ? SuccessTip() : ErrorTip();
            return Json(result);
        }
        public ActionResult Add()
        {
            ViewBag.RoleId = RoleList;
            return View();
        }

        [HttpPost]
        public ActionResult Add(UserModel model)
        {
            model.CreateOn = DateTime.Now;
            model.CreateBy = Operator.UserId;
            model.PassWord=Md5.md5(Configs.GetValue("InitUserPwd"), 32);
            var userId = userService.CreateModelReturnId(model);
            if (userId > 0)
            {
                var departmentUser = new DepartmentUserModel()
                {
                    DepartmentId = model.DepartmentId,
                    UserId = userId
                };
                if (departmentUserService.CreateModel(departmentUser))
                {
                    return Json(SuccessTip());
                }
                else
                {
                    userService.DeleteModel(userId);
                    return Json(ErrorTip());
                }
            }
            else
            {
                return Json(ErrorTip());
            }
        }
        [HttpPost]
        public ActionResult Delete(int Id)
        {
            departmentUserService.DeleteByWhere("where UserId=" + Id);
            var result = userService.DeleteModel(Id) ? SuccessTip() : ErrorTip();
            return Json(result);
        }

        [HttpPost]
        public ActionResult InitPwd(int Id)
        {
            var initPwd = Md5.md5(Configs.GetValue("InitUserPwd"), 32);
            UserModel model = new UserModel { Id = Id, PassWord = initPwd };
            var result = userService.InitPwd(model) ? SuccessTip() : ErrorTip();
            return Json(result);
        }

        public ActionResult UserBelonged(string departmentCode)
        {
            var userList = departmentUserService.GetDepartmentUser(departmentCode);

            var sbJson = new StringBuilder();
            sbJson.Append("[");

            foreach (var item in userList)
            {
                sbJson.Append("{\"id\":\"" + item.UserName + "\", \"value\":\""+ item.UserName + "-" + item.RealName + "\"},");
            }
            if (userList.Count != 0)
                sbJson.Remove(sbJson.Length - 1, 1);
            sbJson.Append("]");

            return Json(sbJson.ToString());
        }

        public ActionResult UserListByDeId(int departmentId)
        {
            var data = userService.GetUserByDepartmentCode(departmentId);

            return Content(JsonConvert.SerializeObject(data), "application/json");
        }
    }
}