using RightControl.Common;
using RightControl.IService.RecordTrancation;
using RightControl.Model.RecordTrancation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightControl.Service.RecordTrancation
{
    public class ExpiredFileVerifyService:IExpiredFileVerifyService
    {
        private readonly IFreeSql fsql;

        public ExpiredFileVerifyService(IFreeSql freeSql)
        {
            fsql = freeSql;
        }

        public bool InsertItems(List<ExpiredFileVerifyEntity> entitys)
        {
            try
            {
                fsql.Transaction(() =>
                {
                    foreach (var item in entitys)
                    {
                        if (item.RecordFileId != null)
                        {
                            if (item.RecordDelSign == false && item.RecordFileDate == null)
                            {

                            }
                            else
                            {
                                var count = fsql.Insert(item).ExecuteAffrows();
                            }
                        }
                        if (item.OtherFileId != null)
                        {
                            if (item.OtherDelSign == false && item.OtherFileDate == null)
                            {

                            }
                            else
                            {
                                var count = fsql.Insert(item).ExecuteAffrows();
                            }
                        }
                    }
                    
                });
                return true;
            }
            catch(Exception e)
            {
                Log.WriteFatal(e);
                return false;
            }
        }

        public bool RefuseExpiredFileChange(string reason, string recordId)
        {
            var list1 = fsql.Select<ExpiredFileVerifyEntity>()
                .Where(i => i.RecordFile.RecordId == recordId || i.OtherFile.RecordID == recordId)
                .Include(a => a.RecordFile)
                .Include(a => a.OtherFile)
                .ToList(a => a.Id);

            var list = fsql.Update<ExpiredFileVerifyEntity>()
                .Where(i => list1.Contains(i.Id))
                .Set(a => a.RefuseReason, reason).Set(a => a.Status, 2).Set(a => a.OperateTime, DateTime.Now).ExecuteAffrows();

            if (list > 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AcceptExpiredFileChange(string recordId)
        {
            try
            {
                fsql.Transaction(() =>
                {
                    var expiredList = fsql.Select<ExpiredFileVerifyEntity>()
                   .Where(i => i.RecordFile.RecordId == recordId || i.OtherFile.RecordID == recordId)
                   .Include(a => a.RecordFile)
                   .Include(a => a.OtherFile)
                   .ToList();

                    var list1 = expiredList.Select(i => i.Id);

                    var list = fsql.Update<ExpiredFileVerifyEntity>()
                        .Where(i => list1.Contains(i.Id)).Set(a => a.Status, 1).Set(a => a.OperateTime, DateTime.Now).ExecuteAffrows();

                    foreach (var item in expiredList)
                    {
                        if (item.RecordFileId != null)
                        {
                            if (item.RecordDelSign == true)
                            {
                                fsql.Delete<RecordList>().Where(i => i.ID == item.RecordFileId).ExecuteAffrows();
                            }
                            else
                            {
                                fsql.Update<RecordList>().Where(i => i.ID == item.RecordFileId).Set(i => i.ExpirationTime, item.RecordFileDate).ExecuteAffrows();
                            }
                        }
                        else
                        {
                            if (item.OtherDelSign == true)
                            {
                                fsql.Delete<OtherFileList>().Where(i => i.ID == item.RecordFileId).ExecuteAffrows();
                            }
                            else
                            {
                                fsql.Update<OtherFileList>().Where(i => i.ID == item.RecordFileId).Set(i => i.ExpirationTime, item.RecordFileDate).ExecuteAffrows();
                            }
                        }
                    }
                });
                return true;
            }
            catch(Exception e)
            {
                Log.WriteFatal(e);
                return false;
            }
            
        }
    }
}
