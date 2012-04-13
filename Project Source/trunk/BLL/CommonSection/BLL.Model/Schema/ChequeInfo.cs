using System;

namespace BLL.Model.Schema
{
    public class ChequeInfo
    {
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string ChequeNo { get; set; }
        public DateTime Date { get; set; }
    }
}
