using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RightControl.Model;
using RightControl.Model.RecordTrancation;

namespace RightControl.IRepository.RecordTrancation
{
    public interface IRecordRepository:IBaseRepository<Record>
    {
        /// <summary>
        /// 插入档案及其已维护文件清单和其他文件清单
        /// </summary>
        /// <param name="record">档案信息</param>
        /// <param name="filelist">已维护清单信息</param>
        /// <param name="otherFileList">其他信息清单</param>
        /// <returns>插入是否成功</returns>
        bool InsertRecord(Record record, List<RecordList> filelist, List<OtherFileList> otherFileList);

        /// <summary>
        /// 通过档案编号获取其所有的已维护的文件清单
        /// </summary>
        /// <param name="recordId">档案编号</param>
        /// <returns>已维护文件清单列表</returns>
        List<RecordList> GetRecordListByRecordId(string recordId);

        /// <summary>
        /// 通过档案编号获取其所有的其他文件清单
        /// </summary>
        /// <param name="recordId">档案编号</param>
        /// <returns>其他文件清单列表</returns>
        List<OtherFileList> GetOtherFileListByRecordId(string recordId);

        /// <summary>
        /// 修改未改变档案类别的档案信息
        /// </summary>
        /// <param name="fileIdList">档案原有已维护文件清单的编号</param>
        /// <param name="otherFileIdList">档案原有其他文件清单的编号(所属文件类别_编号)</param>
        /// <param name="otherFileList">修改后的其他文件清单列表</param>
        /// <param name="fileList">修改后的已维护文件清单列表</param>
        /// <param name="record">修改后的档案信息</param>
        /// <returns>修改是否成功</returns>
        bool EditRecordNotChangeType(List<string> fileIdList, List<string> otherFileIdList, List<OtherFileList> otherFileList,
            List<RecordList> fileList, Record record, OperateLog operaterLog);

        /// <summary>
        /// 修改修改档案类别的档案信息
        /// </summary>
        /// <param name="otherFileList">新勾选的其他文件清单列表</param>
        /// <param name="fileList">新勾选的已维护文件清单列表</param>
        /// <param name="record">修改后的档案信息</param>
        /// <returns>修改是否成功</returns>
        bool EditRecordChangeType(List<OtherFileList> otherFileList, List<RecordList> fileList, Record record, OperateLog operaterLog);

        /// <summary>
        /// 获取档案的文件类型列表
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        List<ContractFileType> GetTheTypeList(string recordId);

        /// <summary>
        /// 根据档案编号获取档案(包括档案归属客户经理的姓名，档案归属部门的部门名称)
        /// </summary>
        /// <param name="recordId">档案编号</param>
        /// <returns>档案信息</returns>
        Record GetRecord(string recordId);

        bool DeleteRecord(string recordId);

        dynamic GetRecordInfoDif();

        List<Record> GetExpiredRecord();

        IEnumerable<Record> GetPageMulTable(SearchFilter filter, out long total);

        bool ChangeRecordBelong(string originalUser, string nowUser);
    }
}
