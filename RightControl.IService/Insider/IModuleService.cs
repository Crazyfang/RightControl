﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RightControl.Model.Insider;

namespace RightControl.IService.Insider
{
    public interface IModuleService : IBaseService<Module>
    {
        IEnumerable<Module> GetModule();
    }
}
