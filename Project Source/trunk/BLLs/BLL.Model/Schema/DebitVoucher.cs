
using BLL.Model.Repositories;

namespace BLL.Model.Schema
{
    public class DebitVoucher : VoucherBase
    {
        public DebitVoucher(IRecordRepository recordRepository)
            : base(recordRepository)
        { }
        //public bool IsPaymentInCheque;
        //public ChequeInfo ChequeInfo;

        public override string VoucherTypeKey
        {
            get { return Constants.DebitVoucherShortKey; }
        }

        public override double Debit
        {
            get { return Amount; }
        }

        public override double Credit
        {
            get { return 0; }
        }


        public static DebitVoucher Clone(Record record)
        {
            return new DebitVoucher(record.RecordRepository)
                       {
                           VoucherSerialNo = record.VoucherSerialNo,
                           ProjectName = record.ProjectName,
                           Date = record.Date,
                           Narration = record.Narration,
                           LinkedVoucherNo = record.LinkedVoucherNo,
                           Tag = record.Tag,
                           VoucherTypeKey = record.VoucherTypeKey,
                           HeadName = record.HeadName,
                           Debit = record.Debit,
                           Credit = record.Credit
                       };
        }
    }
}
