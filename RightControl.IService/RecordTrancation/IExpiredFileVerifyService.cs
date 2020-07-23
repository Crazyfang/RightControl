using RightControl.Model.RecordTrancation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightControl.IService.RecordTrancation
{
    public interface IExpiredFileVerifyService
    {
        bool InsertItems(List<ExpiredFileVerifyEntity> entitys);

        bool RefuseExpiredFileChange(string reason, string recordId);

        bool AcceptExpiredFileChange(string recordId);
    }
}
