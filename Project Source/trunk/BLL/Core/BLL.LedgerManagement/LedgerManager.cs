using System.Collections.Generic;
using BLL.Model.Entity;
using BLL.Model.Managers;
using BLL.Model.Repositories;
using BLL.Model;
using System;
using System.Linq;
using BLL.ParameterManagement;

namespace BLL.LedgerManagement
{
    public class LedgerManager : ManagerBase, ILedgerManager
    {
        private readonly IRepository<Record> _recordRepository;
        private readonly IParameterManager _parameterManager;
        public LedgerManager(IRepository<Record> recordRepository, IParameterManager parameterManager)
        {
            _recordRepository = recordRepository;
            _parameterManager = parameterManager;
            LedgerEndDate = DateTime.Now;
        }

        public override string ModuleName
        {
            get { return "Ledger"; }
        }

        public DateTime LedgerEndDate { get; set; }

        public bool Validate(Project project, Head head, bool showAllAdvance)
        {
            if (project == null)
            {
                InvokeManagerEvent(EventType.Error, "NoProjectSelected");
                return false;
            }

            if (!showAllAdvance && head == null)
            {
                InvokeManagerEvent(EventType.Error, "NoHeadSelected");
                return false;
            }
            return true;
        }

        public IList<Record> GetLedgerBook(Project project, Head head, bool isCashBankShown = false)
        {
            DateTime financialYearStartDate = _parameterManager.GetFinancialYearStartDate();

            IList<Record> records = _recordRepository.Get(r => r.ProjectHead.Project.ID == project.ID).ToList(); // TODO: This line returns 0 debit/credit for LedgerType == LedgerBook.
            if (head.Name.Equals("Cash Book", StringComparison.OrdinalIgnoreCase))
                records = records.Where(r => r.LedgerType.Equals("CashBook", StringComparison.OrdinalIgnoreCase)).ToList();
            else if (head.Name.Equals("Bank Book", StringComparison.OrdinalIgnoreCase))
                records = records.Where(r => r.LedgerType.Equals("BankBook", StringComparison.OrdinalIgnoreCase)).ToList();
            else
                records =  records.Where(r => r.ProjectHead.Head.ID == head.ID && r.LedgerType.Equals("LedgerBook", StringComparison.OrdinalIgnoreCase)).ToList();

            records = records.OrderBy(l => l.Date).ToList();
            records = records.Where(l => GetDateAt12Am(l.Date) >= financialYearStartDate && GetDateAt12Am(l.Date) <= LedgerEndDate).ToList();

            return records;
        }

        public Record GetNextRecord(int id)
        {
            return _recordRepository.Get(r => r.ID == id+1).SingleOrDefault();
        }

        public IList<Record> GetAllAdvance(Project project)
        {
            //return _ledgerRepository.GetLedger(projectId);
            //int[] projectHeadIds = db.ProjectHeads.Where(ph => ph.ProjectID == projectId).Select(ph => ph.ID).ToArray();
            //double balance = 0;
            IList<Record> records = project.ProjectHeads.SelectMany(ph => ph.Records).ToList();
            if (records.Count == 0)
                return null;

            return records.Where(
                    r => r.Tag.Contains("Advance")  && r.LedgerType == "LedgerBook").ToList();
            //TODO: add date filter
        }

        public static DateTime GetDateAt12Am(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day);
        }

        //public IList<Ledger> GetAll(int projectId, string voucherType, DateTime startDate, DateTime endDate)
        //{
        //    IList<Ledger> all = _ledgerRepository.GetLedger(projectId);
        //    return all.Where(l=>l.)
        //}
        //TODO: will complete after code first approach
    }
}

