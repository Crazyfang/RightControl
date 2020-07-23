using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightControl.Model.Permissions
{
    public class DisplayTreeModel
    {
        public int id { get; set; }
        /// <summary>
        /// 操作编码
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 操作名称
        /// </summary>
        public string pId { get; set; }
        /// <summary>
        /// 显示位置
        /// </summary>
        public bool isParent { get; set; }
    }
}
