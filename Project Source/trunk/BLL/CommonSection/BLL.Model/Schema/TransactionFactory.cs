using System;
using BLL.Model.Repositories;

namespace BLL.Model.Schema
{
    public class TransactionFactory
    {
        public Record NewTransaction(IRecordRepository recordRepository, string bankBookOrCashBook, ChequeInfo chequeInfo = null)
        {
            Record transaction = default(Record);
            switch (bankBookOrCashBook)
            {
                case "BankBook":
                    if (chequeInfo == null) throw new Exception("chequeInfo can not be null");
                    transaction = new TransactionInCheque(recordRepository) { ChequeInfo = chequeInfo };
                    break;
                case "CashBook":
                    transaction = new TransactionInCash(recordRepository);
                    break;
                default:
                    throw new Exception("bankBookOrCashBook can be only BankBook or CashBook");
            }
            return transaction;
        }
    }
}
