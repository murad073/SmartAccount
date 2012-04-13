using System;
using System.Collections.Generic;
using BLL.Model.Schema;

namespace BLL.Model.Managers
{
    public interface ILedgerManager
    {
        DateTime LedgerEndDate { get; set; }
        bool Validate(Project project, Head head, bool showAllAdvance);
        //Message GetLatestMessage();
        IList<Ledger> GetLedgerBook(int projectId, int headId, bool isCashBankShown = false);
        IList<Ledger> GetAllAdvance(int projectId);
        DateTime GetDateAt12AM(DateTime date);
    }
}