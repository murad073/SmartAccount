using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Model;
using BLL.Model.Repositories;
using BLL.Model.Schema;


namespace SQL2K8
{
    public class LedgerRepository : ILedgerRepository
    {
        SmartAccountEntities db = new SmartAccountEntities();

        public IList<Ledger> GetLedger(int projectId, int headId)
        {
            int projectHeadId =
                db.ProjectHeads.Where(pc => pc.ProjectID == projectId && pc.HeadID == headId).SingleOrDefault().ID;
            double balance = 0;
            return db.Records.Where(r => r.ProjectHeadID == projectHeadId).ToList()
                .Select(r => GetLedger(r, ref balance)).ToList();
        }

        private BankRecord GetBankRecord(int recordId)
        {
            return db.BankRecords.Where(br => br.RecordID == recordId).SingleOrDefault();
        }

        private Ledger GetLedger(Record record, ref double previousBalance)
        {
            BankRecord bankRecord = GetBankRecord(record.ID);
            double newBalance = previousBalance + record.Debit - record.Credit;
            Ledger ledger = new Ledger
                                   {
                                       Credit = record.Credit,
                                       Debit = record.Debit,
                                       Date = record.Date,
                                       VoucherNo = record.VoucherType + "-" + record.VoucherSerialNo,
                                       Particular = bankRecord != null ? "Bank" : "Cash",
                                       ChequeNo = bankRecord != null ? bankRecord.ChequeNo : "",
                                       Remarks = record.Narration,
                                       Balance = newBalance
                                   };
            previousBalance = newBalance;
            return ledger;
        }

        public IList<Ledger> GetLedger(int projectId, int headId, TimeSpan dateRange)
        {
            throw new NotImplementedException();
        }

        public IList<Ledger> GetLedger(int projectId, int headId, DateTime endDate)
        {
            throw new NotImplementedException();
        }
    }
}
