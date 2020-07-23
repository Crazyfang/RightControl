﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RightControl.Model.RecordTrancation;

namespace RightControl.IService.RecordTrancation
{
    public interface ILoanManagerService:IBaseService<LoanManager>
    {
        List<LoanManager> GetLoanManagersByCustInCode(string custInCode);
    }
}
