using BLL.Model.Repositories;

namespace BLL.Model.Schema
{
    public class TransactionInCash : Record
    {
        public TransactionInCash(IRecordRepository recordRepository) : base(recordRepository)
        {
        }

        public override string LedgerType
        {
            get { return "CashBook"; }
        }

        public override bool Save()
        {
            return RecordRepository.InsertCashBookRow(this);
        }

        public override string HeadName 
        {
            get { return "Cash Book"; }
        }

        public override bool IsValid()
        {
            return true;
        }

        //public static TransactionInCash Clone(Record record)
        //{
        //    return new TransactionInCash(record.RecordRepository)
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
