using DapperExtensions;
using System;
using System.ComponentModel.DataAnnotations;
using FreeSql.DataAnnotations;
using System.Collections;
using System.Collections.Generic;

namespace RightControl.Model.Permissions
{
    [FreeSql.DataAnnotations.Table(Name = "t_department")]
    [DapperExtensions.Table("t_department")]
    public class DepartmentModel:Entity
    {
        /// <summary>
        /// 部门编号
        /// </summary>
        [Display(Name = "部门编号")]
        public string DepartmentCode { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        [Display(Name = "部门名称")]
        public string DepartmentName { get; set; }

        /// <summary>
        /// 所属街区
        /// </summary>
        public string BelongStreet { get; set; }

        /// <summary>
        /// 上级部门编号
        /// </summary>
        [Display(Name = "上级部门编号")]
        public int ParentId { get; set; }

        [Computed]
        [Navigate(ManyToMany = typeof(DepartmentUserModel))]
        public ICollection<UserModel> UserList { get; set; }
    }
}
