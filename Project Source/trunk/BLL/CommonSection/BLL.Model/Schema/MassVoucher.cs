using System;
using BLL.Model.Entity;

namespace BLL.Model.Schema
{
    public class MassVoucher
    {

        #region Voucher Info
        public string VoucherType { get; set; }
        public int VoucherSerialNo { get; set; }

        public Project Project { get; set; }
        public Head Head { get; set; }
        public DateTime VoucherDate { get; set; }
        public double Amount { get; set; }
        public string Narration { get; set; }

        public string LinkedVoucherNo { get; set; }
        public string Tag { get; set; }
        #endregion


        public bool IsFixedAsset { get; set; }
        #region Fixed Asset Information
        public string FixedAssetName { get; set; }
        public double FixedAssetDepreciationRate { get; set; }
        #endregion


        public bool IsCheque { get; set; }
        #region Cheque Information
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string ChequeNo { get; set; }
        public DateTime ChequeDate { get; set; }
        #endregion

        public string JVDebitOrCredit { get; set; }
        public string ContraType { get; set; }

    }
}
