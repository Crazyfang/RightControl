using DapperExtensions;
using FreeSql.DataAnnotations;
using RightControl.Model.Permissions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RightControl.Model
{
    [FreeSql.DataAnnotations.Table(Name = "t_User")]
    [DapperExtensions.Table("t_User")]
    public class UserModel:Entity
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Display(Name = "用户名")]
        public string UserName { get; set; }
        /// <summary>
        /// 真实名称
        /// </summary>
        [Display(Name = "真实名称")]
        public string RealName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 性别（0：女，1：男）
        /// </summary>
        [Display(Name = "性别")]
        public int Gender { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        [Display(Name = "手机")]
        public string Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [Display(Name = "邮箱")]
        public string Email { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [Display(Name = "头像")]
        public string HeadShot { get; set; }

        [Computed]
        [FreeSql.DataAnnotations.Column(IsIgnore = true)]
        [Display(Name = "角色名称")]
        public string RoleName { get; set; }

        [Computed]
        [FreeSql.DataAnnotations.Column(IsIgnore = true)]
        [Display(Name = "部门名称")]
        public string DepartmentName { get; set; }

        [Computed]
        [FreeSql.DataAnnotations.Column(IsIgnore = true)]
        [Display(Name = "部门ID")]
        public int DepartmentId { get; set; }

        [Computed]
        [FreeSql.DataAnnotations.Column(IsIgnore = true)]
        [Display(Name = "部门代码")]
        public string DepartmentCode { get; set; }

        [Computed]
        [Navigate(ManyToMany = typeof(DepartmentUserModel))]
        public ICollection<DepartmentModel> DepartmentList { get; set; }

        [Computed]
        [Navigate(ManyToMany = typeof(UserRoleModel))]
        public ICollection<RoleModel> RoleList { get; set; }
    }
}
