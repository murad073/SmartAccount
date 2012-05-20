using System;

namespace GKS.Model
{
    public class VoucherItem
    {
        public string ProjectName { get; set; }
        public DateTime Date { get; set; }
        public string VoucherNo { get; set; }
        public string HeadOfAccount { get; set; }       
        public double Amount { get; set; }
        public string CashOrBank { get; set; }
        public string Narration { get; set; }
        public string ChequeNo { get; set; }
        public DateTime ChequeDate { get; set; }
        public string BankName { get; set; }
    }
}