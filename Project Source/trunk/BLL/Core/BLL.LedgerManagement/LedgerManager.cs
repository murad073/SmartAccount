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

            IList<Record> records = _recordRepository.Get(r => r.ProjectHead.Project.ID == project.ID && r.ProjectHead.Head.ID == head.ID).ToList();
            records = records.OrderBy(l => l.Date).ToList();
            records = records.Where(l => GetDateAt12Am(l.Date) >= financialYearStartDate && GetDateAt12Am(l.Date) <= LedgerEndDate).ToList();

            return records;
            //return _recordRepository.Get(r => r.ProjectHead.Project.ID == project.ID && r.ProjectHead.Head.ID == head.ID).OrderBy(l => l.Date).ToList().Where(
            //        l => GetDateAt12Am(l.Date) >= financialYearStartDate && GetDateAt12Am(l.Date) <= LedgerEndDate).
            //        ToList();

            //return
            //    _ledgerRepository.GetLedger(projectId, headId) .OrderBy(l => l.Date).Where(
            //        l => GetDateAt12Am(l.Date) >= financialYearStartDate && GetDateAt12Am(l.Date) <= LedgerEndDate).
            //        ToList();
        }

        public IList<Record> GetAllAdvance(Project project)
        {
            //return _ledgerRepository.GetLedger(projectId);
            //int[] projectHeadIds = db.ProjectHeads.Where(ph => ph.ProjectID == projectId).Select(ph => ph.ID).ToArray();
            //double balance = 0;
            return project.ProjectHeads.SelectMany(ph => ph.Records).Where(
                    r => r.Tag == "Advance" && r.LedgerType == "LedgerBook").ToList();
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

