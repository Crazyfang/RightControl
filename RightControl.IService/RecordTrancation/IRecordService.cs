using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RightControl.Model;
using RightControl.Model.RecordTrancation;

namespace RightControl.IService.RecordTrancation
{
    public interface IRecordService:IBaseService<Record>
    {
        /// <summary>
        /// 获取递增文件编号
        /// </summary>
        /// <returns>文件编号</returns>
        int GetNum();

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

        dynamic TestGetRecordPage(Record record, PageInfo pageInfo);

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
            List<RecordList> fileList, Record record, OperateLog operateLog);

        /// <summary>
        /// 修改修改档案类别的档案信息
        /// </summary>
        /// <param name="otherFileList">新勾选的其他文件清单列表</param>
        /// <param name="fileList">新勾选的已维护文件清单列表</param>
        /// <param name="record">修改后的档案信息</param>
        /// <returns>修改是否成功</returns>
        bool EditRecordChangeType(List<OtherFileList> otherFileList, List<RecordList> fileList, Record record, OperateLog operateLog);

        /// <summary>
        /// 获取档案所有的文件类型
        /// </summary>
        /// <param name="recordId">档案编号</param>
        /// <returns>文件类型列表</returns>
        List<ContractFileType> GetTheTypeList(string recordId);

        /// <summary>
        /// 根据档案编号获取档案信息(包括档案归属部门名称，档案归属客户经理姓名)
        /// </summary>
        /// <param name="recordId">档案编号</param>
        /// <returns>档案信息</returns>
        Record GetRecord(string recordId);

        bool DeleteRecord(string recordId);

        dynamic GetRecordInfoDif();

        List<Record> GetExpiredRecord();

        dynamic GetPageMulTable(Record filter, PageInfo pageInfo);

        bool ChangeRecordBelong(string originalUser, string nowUser);

        dynamic RecordBelongList(string userCode);

        bool RecordBelongChange(JArray changeRecordList, string operater);

        dynamic GetExpiredFileRecord(Record filter, PageInfo pageInfo);

        dynamic GetExpiredFileRecord(Record filter, PageInfo pageInfo, string userCode);

        dynamic NeedVerifyExpiredFileList(PageInfo pageInfo);

        ExpiredClass GetExpiredFileCompare(string recordId);

        List<OperateLog> GetRecordHistory(string recordId);


    }
}
