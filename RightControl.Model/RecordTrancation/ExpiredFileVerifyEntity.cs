using FreeSql.DataAnnotations;
using System;

namespace RightControl.Model.RecordTrancation
{
    [Table(Name="ExpiredFileVerify")]
    public class ExpiredFileVerifyEntity
    {
        [Column(IsIdentity = true)]
        public int Id { get; set; }

        public string RecordItemId { get; set; }

        [Navigate(nameof(RecordItemId))]
        public Record RecordItem { get; set; }

        /// <summary>
        /// 档案文件主键
        /// </summary>
        public int? RecordFileId { get; set; }

        public DateTime? RecordFileDate { get; set; }

        [Navigate(nameof(RecordFileId))]
        public RecordList RecordFile { get; set; }

        /// <summary>
        /// 档案文件删除标记
        /// false-保留
        /// true-删除
        /// </summary>
        public bool RecordDelSign { get; set; }

        /// <summary>
        /// 其他文件主键
        /// </summary>
        public int? OtherFileId { get; set; }

        public DateTime? OtherFileDate { get; set; }

        [Navigate(nameof(OtherFileId))]
        public OtherFileList OtherFile { get; set; }

        /// <summary>
        /// 其他文件删除标记
        /// false-保留
        /// true-删除
        /// </summary>
        public bool OtherDelSign { get; set; }

        /// <summary>
        /// 审核状态
        /// 0-待审核
        /// 1-审核通过
        /// 2-审核不通过
        /// </summary>
        public int Status { get; set; }

        public DateTime? OperateTime { get; set; }

        public string RefuseReason { get; set; }

        /// <summary>
        /// 更新原因 0-过期更新 1-主动更新
        /// </summary>
        public int Type { get; set; }
    }
}
