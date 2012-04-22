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
    }
}
