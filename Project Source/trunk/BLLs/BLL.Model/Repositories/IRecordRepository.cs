using BLL.Model.Schema;

namespace BLL.Model.Repositories
{
    public interface IRecordRepository
    {
        int GetMaxVoucherNo(string voucherType, int projectId);

        bool BeginTransaction();

        bool CommitTransaction();

        bool RollbackTransaction();

        bool InsertLedgerBookRow(VoucherBase record);

        bool InsertLedgerBookRow(VoucherBase record, FixedAsset fixedAsset);

        bool InsertCashBookRow(TransactionInCash cashRecord);

        bool InsertBankBookRow(TransactionInCheque chequeRecord);

        bool IsRecordFound(string key, int serialNo);
        
        bool IsRecordFound(int projectId, int headId);
    }
}
