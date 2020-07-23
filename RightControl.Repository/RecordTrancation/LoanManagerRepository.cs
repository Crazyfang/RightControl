using RightControl.IRepository.RecordTrancation;
using RightControl.Model.RecordTrancation;
using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace RightControl.Repository.RecordTrancation
{
    public class LoanManagerRepository:BaseRepository<LoanManager>, ILoanManagerRepository
    {
        public List<LoanManager> GetLoanManagersByCustInCode(string custInCode)
        {
            using (var conn = SqlHelper.SqlConnection())
            {
                string sql = "select * into #temp from dbo.ZH_XDLZ_LoanCustOfEmp " +
                             "where CustINNO = @CustINNO " +
                             "select distinct Instcode as DepartmentCode, a.Tlrno1 as ManagerCode, a.EMPName1 as ManagerName from " +
                             "(select Instcode, Tlrno1, EMPName1 " +
                             "from #temp " +
                             "union " +
                             "select Instcode, Tlrno2, EMPName2 " +
                             "from #temp " +
                             "where Tlrno2 <> '' and EMPName2 <> '' " +
                             "union " +
                             "select Instcode, Tlrno3, EMPName3 " +
                             "from #temp " +
                             "where Tlrno3 <> '' and EMPName3 <> '') a " +
                             "drop table #temp ";

                return conn.Query<LoanManager>(sql, new {CustINNO = custInCode}).ToList();
            }
        }
    }
}
