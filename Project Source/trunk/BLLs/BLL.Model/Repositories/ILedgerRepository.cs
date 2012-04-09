using System;
using System.Collections.Generic;
using BLL.Model.Schema;

namespace BLL.Model.Repositories
{
    public interface ILedgerRepository
    {
        IList<Ledger> GetLedger(int projectId);
        IList<Ledger> GetLedger(int projectId, int headId);
        IList<Ledger> GetLedger(int projectId, int headId, TimeSpan dateRange);
        IList<Ledger> GetLedger(int projectId, int headId, DateTime endDate);
    }
}


