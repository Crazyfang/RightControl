using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions;
using FreeSql.DataAnnotations;

namespace RightControl.Model.Permissions
{
    [FreeSql.DataAnnotations.Table(Name = "t_DepartmentUser")]
    [DapperExtensions.Table("t_DepartmentUser")]
    public class DepartmentUserModel
    {
        [DapperExtensions.Key(true)]
        [FreeSql.DataAnnotations.Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }

        [Computed]
        public DepartmentModel Department { get; set; }

        public int DepartmentId { get; set; }

        [Computed]
        public UserModel User { get; set; }

        public int UserId { get; set; }
    }
}
