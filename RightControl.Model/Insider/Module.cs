using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RightControl.Model.Insider
{
    public class Module
    {
        [DapperExtensions.Key(true)]
        public int No { get; set; }

        [Display(Name = "模块名称")]
        public string Name { get; set; }

        [Display(Name = "模块描述")]
        public string Description { get; set; }

        [Display(Name = "添加时间")]
        public DateTime CreateOn { get; set; }
    }
}
