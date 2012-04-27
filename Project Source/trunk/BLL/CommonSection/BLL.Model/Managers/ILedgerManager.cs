using System;
using System.Collections.Generic;
using BLL.Model.Entity;

namespace BLL.Model.Managers
{
    public interface ILedgerManager
    {
        DateTime LedgerEndDate { get; set; }
        bool Validate(ProjectHead projectHead, bool showAllAdvance);
        //IList<Ledger> GetLedgerBook(int projectId, int headId, bool isCashBankShown = false);
        //IList<Ledger> GetAllAdvance(int projectId);
        IList<Record> GetLedgerBook(ProjectHead projectHead, bool isCashBankShown = false);
        IList<Record> GetAllAdvance(Project project);
        DateTime GetDateAt12Am(DateTime date);
    }
}