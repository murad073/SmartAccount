using BLL.Model.Entity;
using BLL.Model.Repositories;

namespace BLL.Model.Schema
{
    public class TransactionInCash : Record
    {
        public TransactionInCash()
        {
        }

        public TransactionInCash(IRepository<Record> recordRepository)
        {
            base.RecordRepository = recordRepository;
        }

        public override string LedgerType
        {
            get { return "CashBook"; }
        }

        public override bool Save()
        {
            //return RecordRepository.InsertCashBookRow(this);
            //TODO: change the save logic
            return true;
        }

        public override string HeadName() 
        {
            return "Cash Book"; 
        }

        //public override bool IsValid()
        //{
        //    return true;
        //}
    }
}
