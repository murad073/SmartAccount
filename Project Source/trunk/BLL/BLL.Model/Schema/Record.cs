using System;
using BLL.Model.Repositories;
using BLL.Utils;

namespace BLL.Model.Schema
{
    public abstract class Record
    {
        internal IRecordRepository RecordRepository;

        protected Record(IRecordRepository recordRepository)
        {
            RecordRepository = recordRepository;
        }

        public int VoucherSerialNo { get; set; }
        public string ProjectName { get; set; }
        public DateTime Date { get; set; }
        public string Narration { get; set; }
        public string LinkedVoucherNo { get; set; }
        public string Tag { get; set; }

        public virtual string VoucherTypeKey { get; set; }
        public virtual string HeadName { get; set; }
        public virtual double Debit { get; set; }
        public virtual double Credit { get; set; }

        public abstract string LedgerType { get; }
        public abstract bool Save();
        public abstract bool IsValid();

        public string VoucherNo
        {
            get { return VoucherNumberHelper.GetVoucherNumber(VoucherTypeKey, VoucherSerialNo); }
        }
        public double Balance
        {
            get { return Debit - Credit; }
        }
    }
}
