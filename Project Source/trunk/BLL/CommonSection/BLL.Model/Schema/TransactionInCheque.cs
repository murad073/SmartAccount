using BLL.Model.Entity;
using BLL.Model.Repositories;

namespace BLL.Model.Schema
{
    public class TransactionInCheque : Record
    {
        //public ChequeInfo ChequeInfo;

        public TransactionInCheque(IRepository<Record> recordRepository)
        {
            base.RecordRepository = recordRepository;
        }

        public override string LedgerType
        {
            get { return "BankBook"; }
        }

        public override bool Save()
        {
            //return RecordRepository.InsertBankBookRow(this);
            //TODO: change the save logic
            return true;
        }

        public override string HeadName()
        {
            return "Bank Book"; 
        }

        //public override bool IsValid()
        //{
        //    return true;
        //}

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
