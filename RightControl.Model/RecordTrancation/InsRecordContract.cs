namespace RightControl.Model.RecordTrancation
{
    public class InsRecordContract
    {
        [DapperExtensions.Key(true)]
        public int ID { get; set; }

        public string CustINNO { get; set; }

        public string ContractNo { get; set; }

        public int DeleteSign { get; set; }
    }
}
