using System;

namespace BLL.Model.Entity
{
    public class BankRecord
    {
        public int ID { get; set; }
        public int RecordID { get; set; }
        public string ChequeNo { get; set; }
        public string BankName { get; set; }
        public string Branch { get; set; }
        public DateTime ChequeDate { get; set; }
    }
}
