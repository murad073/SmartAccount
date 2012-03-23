using System;
using System.ComponentModel;

namespace BLL.Model.Schema
{
    public class Ledger
    {
        public DateTime Date { get; set; }

        public string VoucherNo { get; set; }

        public double Debit { get; set; }
        public double Credit { get; set; }
        public double Balance { get; set; }

        public string Particular { get; set; }
        public string ChequeNo { get; set; }
        public string Remarks { get; set; }
    }
}


