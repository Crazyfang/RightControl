using FreeSql.DataAnnotations;
using System;

namespace RightControl.Model.RecordSetting
{
    [Table(Name = "freesql_test")]
    public class Test
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public long BlogId { get; set; }

        public string Url { get; set; }

        public int Rating { get; set; }
    }
}
