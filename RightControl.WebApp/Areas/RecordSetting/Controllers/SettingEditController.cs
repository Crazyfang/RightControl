using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RightControl.IService.RecordSetting;
using RightControl.IService.RecordTrancation;
using RightControl.Model;
using RightControl.Model.RecordSetting;
using RightControl.Model.RecordTrancation;
using RightControl.WebApp.Areas.Admin.Controllers;

namespace RightControl.WebApp.Areas.RecordSetting.Controllers
{
    public class SettingEditController : BaseController
    {
        public ISelectTypeNameService SelectTypeNameService { get; set; }

        public ISelectTypeService SelectTypeService { get; set; }

        public IFileListService FileListService { get; set; }

        public IRecordFileTypeService RecordFileTypeService { get; set; }
        
        // GET: RecordSetting/SettingEdit
        public override ActionResult Index(int? id)
        {
            base.Index(id);
            return View();
        }

        /// <summary>
        /// 档案类型操作
        /// </summary>
        /// <param name="value">传递过来的名字</param>
        /// <param name="type">1-增、2-改、3-删</param>
        /// <returns></returns>
        public ActionResult RecordTypeProcess(string value, int type)
        {
            //增加
            if (type == 1)
            {
                if (!SelectTypeNameService.GetByWhere(" where SelectTypeNameString=@SelectTypeNameString",
                    new {SelectTypeNameString = value}).Any())
                {
                    var id = SelectTypeNameService.CreateModelReturnId(new SelectTypeName()
                        {SelectTypeNameString = value});
                    return Json(new {state = true, data = $"SelectType_{id}"}, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(ErrorTip($"添加失败，失败原因：已存在相同名称的条目!"));
                    //return Json(new {status = false, msg = $"添加失败，失败原因："}, JsonRequestBehavior.AllowGet);
                }
            }
            //编辑
            else if (type == 2)
            {
                var name = string.IsNullOrEmpty(Request["name"])?new string[0]:Request["name"].Split('_');
                if (name.Length == 0)
                {
                    return Json(ErrorTip($"编辑失败，失败原因：未传递被更改条目的名称!"));
                }
                else
                {
                    var result =
                        SelectTypeNameService.UpdateModel(new SelectTypeName()
                            {Id = int.Parse(name[1]), SelectTypeNameString = value})
                            ? SuccessTip($"编辑成功!")
                            : ErrorTip($"编辑失败!");

                    return Json(result);
                }
            }
            //删除
            else
            {
                var name = string.IsNullOrEmpty(Request["name"]) ? new string[0] : Request["name"].Split('_');
                if (name.Length == 0)
                {
                    return Json(ErrorTip($"编辑失败，失败原因：未传递被更改条目的名称!"));
                }
                else
                {
                    var result = SelectTypeNameService.DeleteModel(int.Parse(name[1])) ? SuccessTip($"删除成功!") : ErrorTip($"删除失败!");

                    return Json(result);
                }
            }
        }

        /// <summary>
        /// 返回档案类型列表
        /// </summary>
        /// <returns>档案类型列表</returns>
        public ActionResult ReturnRecordType()
        {
            var selectTypeNameList = SelectTypeNameService.GetAll();

            return Json(new {state = true, data = selectTypeNameList}, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 返回档案类型所拥有的文件类型
        /// </summary>
        /// <param name="recordTypeId">档案类型编号</param>
        /// <returns>文件类型列表</returns>
        public ActionResult ReturnRecordTypeOfFileType(int recordTypeId)
        {
            var result = SelectTypeService.GetSelectTypeName(recordTypeId);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 返回所有文件类型
        /// </summary>
        /// <returns>所有文件类型列表</returns>
        public ActionResult ReturnFileType()
        {
            var fileList = RecordFileTypeService.GetAll().ToList();

            return Json(new {state = true, data = fileList});
        }

        /// <summary>
        /// 返回文件类型所属的文件
        /// </summary>
        /// <param name="fileTypeId">文件类型编号</param>
        /// <returns>文件类型所属的文件列表</returns>
        public ActionResult ReturnFileTypeOfFile(int fileTypeId)
        {
            var fileList = FileListService.GetFileListByFileTypeId(fileTypeId);

            return Json(fileList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 新增档案类型
        /// </summary>
        /// <param name="recordTypeName">档案类型名称</param>
        /// <returns>档案类型编号</returns>
        public ActionResult InsertRecordType(string recordTypeName)
        {
            if (!SelectTypeNameService.GetByWhere(" where SelectTypeNameString=@SelectTypeNameString",
                new { SelectTypeNameString = recordTypeName }).Any())
            {
                var id = SelectTypeNameService.CreateModelReturnId(new SelectTypeName()
                    { SelectTypeNameString = recordTypeName });
                return Json(new { state = true, Id = $"{id}" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(ErrorTip($"添加失败，失败原因：已存在相同名称的条目!"));
                //return Json(new {status = false, msg = $"添加失败，失败原因："}, JsonRequestBehavior.AllowGet);
            }

            //return Json(new {state = true, Id = 3, msg = "操作成功!"}, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 编辑档案类型
        /// </summary>
        /// <param name="model"></param>
        /// <returns>修改成功与否标记</returns>
        public ActionResult EditRecordType(SelectTypeName model)
        {
            var result = SelectTypeNameService.UpdateModel(model) ? SuccessTip("修改成功!") : ErrorTip("修改失败!");

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除档案类型
        /// </summary>
        /// <param name="Id">档案类型编号</param>
        /// <returns>删除成功与否标记</returns>
        public ActionResult DeleteRecordType(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return Json(new {state = false, msg = $"请求删除的档案类型编号不能为空!"}, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var result = SelectTypeNameService.DeleteSelectTypeName(int.Parse(Id)) ? SuccessTip("删除成功!") : ErrorTip("删除失败!");

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 档案类型未勾选文件类型列表
        /// </summary>
        /// <param name="Id">档案类型编号</param>
        /// <returns>文件类型列表</returns>
        public ActionResult FileTypeListOfUnselected(int Id)
        {
            var list = RecordFileTypeService
                .GetByWhere($" where ID not in (select FileType from SelectType where SelectTypeNum = {Id})").ToList();

            var temp = new List<FormSelect>();

            foreach (var item in list)
            {
                var formSelect = new FormSelect()
                {
                    name = item.RecordTypeName,
                    value = item.ID
                };
                temp.Add(formSelect);
            }

            return Json(temp, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 新增档案类型的文件类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult InsertFileTypeOfRt(int id)
        {
            try
            {
                var strFileType = Request["Tags[]"];

                foreach (var temp in strFileType.Split(','))
                {
                    var selectType = new SelectType()
                    {
                        FileType = int.Parse(temp),
                        SelectTypeNum = id
                    };
                    SelectTypeService.CreateModel(selectType);
                }

                return Json(new {state = true}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new {state = true, msg = e.Message}, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 删除档案类型的文件类型
        /// </summary>
        /// <param name="deleteId">需要删除的文档类型和文件类型的映射主键</param>
        /// <returns></returns>
        public ActionResult DeleteFileTypeOfRt(int deleteId)
        {
            var result =
                SelectTypeService.DeleteModel(deleteId)
                    ? SuccessTip()
                    : ErrorTip();
            return Json(result);
        }

        /// <summary>
        /// 新增文件类型
        /// </summary>
        /// <param name="fileTypeName">文件类型名称</param>
        /// <returns>新增成功与否标记</returns>
        public ActionResult InsertFileType(string fileTypeName)
        {
            if (RecordFileTypeService.GetByWhere($" where RecordTypeName='{fileTypeName}'").Any())
            {
                return Json(new {state = false, msg = "增加失败，已有相同条目存在!"}, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var maxTypeString = RecordFileTypeService.GetMaxTypeString().RecordFileTypeString;
                var newTypeString = "";

                var charTypeString = maxTypeString.ToCharArray();
                if (charTypeString[1] == 'Z')
                {
                    char dou = Convert.ToChar((int) Convert.ToByte(charTypeString[0]) + 1);
                    char single = 'A';

                    newTypeString = dou.ToString() + single.ToString();
                }
                else
                {
                    char single = Convert.ToChar((int)Convert.ToByte(charTypeString[1]) + 1);
                    newTypeString = charTypeString[0].ToString() + single.ToString();
                }

                var id = RecordFileTypeService.CreateModelReturnId(new RecordFileType
                    {RecordFileTypeString = newTypeString, RecordTypeName = fileTypeName});

                return Json(new {state = true, Id = id}, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 编辑文件类型名称
        /// </summary>
        /// <param name="id">文件类型编号</param>
        /// <param name="fileTypeName">新文件类型名称</param>
        /// <returns>编辑成功与否标记</returns>
        public ActionResult EditFileType(int id, string fileTypeName)
        {
            var model = RecordFileTypeService.ReadModel(id);

            model.RecordTypeName = fileTypeName;

            var result = RecordFileTypeService.UpdateModel(model) ? SuccessTip() : ErrorTip();
            return Json(result);
        }

        /// <summary>
        /// 删除文件类型
        /// </summary>
        /// <param name="id">文件类型编号</param>
        /// <returns>删除成功与否标记</returns>
        public ActionResult DeleteFileType(int id)
        {
            var result = RecordFileTypeService.DeleteFileType(id) ? SuccessTip() : ErrorTip();
            return Json(result);
        }

        /// <summary>
        /// 新增文件
        /// </summary>
        /// <param name="fileTypeId">文件类型编号</param>
        /// <param name="fileName">文件名称</param>
        /// <returns>新增成功与否</returns>
        public ActionResult InsertFile(int fileTypeId, string fileName)
        {
            var file = new FileList()
            {
                ActiveMark = true,
                FileName = fileName,
                RecordFileType = fileTypeId
            };

            var result = FileListService.CreateModel(file) ? SuccessTip() : ErrorTip();

            return Json(result);
        }

        /// <summary>
        /// 编辑文件
        /// </summary>
        /// <param name="fileId">文件编号</param>
        /// <param name="fileName">新文件名称</param>
        /// <returns>编辑成功与否</returns>
        public ActionResult EditFile(int fileId, string fileName)
        {
            var file = FileListService.ReadModel(fileId);
            file.FileName = fileName;

            var result = FileListService.UpdateModel(file) ? SuccessTip() : ErrorTip();

            return Json(result);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileId">文件编号</param>
        /// <returns>删除成功与否</returns>
        public ActionResult DeleteFile(int fileId)
        {
            var result = FileListService.DeleteModel(fileId) ? SuccessTip() : ErrorTip();

            return Json(result);
        }

        public ActionResult GetRecordType()
        {
            var recordTypeList = SelectTypeNameService.GetAll().ToList();

            return Json(recordTypeList, JsonRequestBehavior.AllowGet);
        }
    }
}