using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightControl.Common
{
    /// <summary>
    /// 常用公共类
    /// </summary>
    public class Common
    {
        #region 自动生成编号
        /// <summary>
        /// 表示全局唯一标识符 (GUID)。
        /// </summary>
        /// <returns></returns>
        public static string GuId()
        {
            return Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 自动生成编号  201008251145409865
        /// </summary>
        /// <returns></returns>
        public static string CreateNo()
        {
            Random random = new Random();
            string strRandom = random.Next(1000, 10000).ToString(); //生成编号 
            string code = DateTime.Now.ToString("yyyyMMddHHmmss") + strRandom;//形如
            return code;
        }

        /// <summary>
        /// 自动生成档案编号
        /// </summary>
        /// <param name="recordType">档案类型编号</param>
        /// <param name="departmentCode">机构号</param>
        /// <param name="sequenceNumber">序数</param>
        /// <returns></returns>
        public static string CreateRecordId(string recordType, string departmentCode, int sequenceNumber)
        {
            var recordId = "AA" + recordType + departmentCode + sequenceNumber.ToString().PadLeft(5, '0');
            return recordId;
        }
        #endregion

        public static string TransferDateTime(DateTime? value)
        {
            if (value.HasValue)
            {
                return value.Value.ToString("yyyy-MM-dd");
            }
            else
            {
                return "无";
            }
        }
    }
}
