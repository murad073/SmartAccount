using System;

namespace GKS.Model
{
    public class ViewableGridRow
    {
        public string Head { get; set; }
        public DateTime Date { get; set; }
        public string VoucherNo { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
        public double Balance { get; set; }
    }
}