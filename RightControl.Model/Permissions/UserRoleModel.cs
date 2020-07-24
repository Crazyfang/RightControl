using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightControl.Model.Permissions
{
    [Table(Name = "UserRole")]
    public class UserRoleModel
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }

        public UserModel User { get; set; }

        /// <summary>
        /// 用户主键
        /// </summary>
        public int UserId { get; set; }

        public RoleModel Role { get; set; }

        /// <summary>
        /// 角色主键
        /// </summary>
        public int RoleId { get; set; }

    }
}
