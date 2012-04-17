using BLL.Model.Repositories;

namespace BLL.Model.Schema
{
    public class TransactionInCheque : Record
    {
        public ChequeInfo ChequeInfo;

        public TransactionInCheque(IRecordRepository recordRepository) : base(recordRepository)
        {
        }

        public override string LedgerType
        {
            get { return "BankBook"; }
        }

        public override bool Save()
        {
            return RecordRepository.InsertBankBookRow(this);
        }

        public override string HeadName
        {
            get { return "Bank Book"; }
        }

        public override bool IsValid()
        {
            return true;
        }

        //public static TransactionInCheque Clone(Record record)
        //{
        //    return new TransactionInCheque(record.RecordRepository)
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
