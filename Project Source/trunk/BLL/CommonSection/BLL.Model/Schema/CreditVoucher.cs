
using BLL.Model.Repositories;

namespace BLL.Model.Schema
{
    public class CreditVoucher : VoucherBase
    {
        public CreditVoucher(IRecordRepository recordRepository) : base(recordRepository)
        {
        }

        public override string VoucherTypeKey
        {
            get { return Constants.CreditVoucherShortKey; }
        }

        public override double Debit
        {
            get { return 0; }
        }

        public override double Credit
        {
            get { return Amount; }
        }

        //public static CreditVoucher Clone(Record record)
        //{
        //    return new CreditVoucher(record.RecordRepository)
        //    {
        //        VoucherSerialNo = record.VoucherSerialNo,
        //        ProjectName = record.ProjectName,
        //        Date = record.Date,
        //        Narration = record.Narration,
        //        LinkedVoucherNo = record.LinkedVoucherNo,
        //        Tag = record.Tag,
        //        VoucherTypeKey = record.VoucherTypeKey,
        //        HeadName = record.HeadName,
        //        Debit = record.Debit,
        //        Credit = record.Credit
        //    };
        //}
    }
}


