using BLL.Model.Entity;
using BLL.Model.Repositories;

namespace BLL.Model.Schema
{
    public class TransactionInCheque : Record
    {
        private readonly IRepository<BankBook> _bankBookRepository;
        public BankBook BankBook;

        public TransactionInCheque()
        {
        }

        public TransactionInCheque(IRepository<Record> recordRepository, IRepository<BankBook> bankBookRepository)
        {
            base.RecordRepository = recordRepository;
            _bankBookRepository = bankBookRepository;
        }

        public override string LedgerType
        {
            get { return "BankBook"; }
        }

        public override bool Save()
        {
            RecordRepository.Insert(this);

            if (BankBook != null)
            {
                BankBook.Record = this;
                _bankBookRepository.Insert(BankBook);
            }

            if (RecordRepository.Save() > 0 || _bankBookRepository.Save() > 0)
                return true;
            RecordRepository.Discard();
            return false;
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
