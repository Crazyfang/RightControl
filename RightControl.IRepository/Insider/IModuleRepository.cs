﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RightControl.Model.Insider;

namespace RightControl.IRepository.Insider
{
    public interface IModuleRepository : IBaseRepository<Module>
    {
        IEnumerable<Module> GetModule();
    }
}
