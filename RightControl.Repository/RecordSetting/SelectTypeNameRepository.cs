using Dapper;
using RightControl.IRepository.RecordSetting;
using RightControl.Model.RecordSetting;
using System.Data;

namespace RightControl.Repository.RecordSetting
{
    public class SelectTypeNameRepository:BaseRepository<SelectTypeName>, ISelectTypeNameRepository
    {
        public bool DeleteSelectTypeName(int id)
        {
            using (var conn = SqlHelper.SqlConnection())
            {
                IDbTransaction transaction = conn.BeginTransaction();
                try
                {
                    var sql1 = $"delete from SelectTypeName where Id={id}";
                    var sql2 = $"delete from SelectType where SelectTypeNum={id}";

                    conn.Execute(sql1, null, transaction);
                    conn.Execute(sql2, null, transaction);

                    transaction.Commit();
                    return true;
                }
                catch (System.Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
    }
}
