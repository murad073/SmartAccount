using System.Collections.Generic;
using BLL.Messaging;
using BLL.Model.Repositories;
using BLL.Model.Schema;
using System;
using BLL.ParameterManagement;
using System.Linq;

namespace BLL.LedgerManagement
{
    public class LedgerManager
    {
        private readonly ILedgerRepository _ledgerRepository;
        private readonly ParameterManager _parameterManager;
        private readonly Message _message;
        public LedgerManager(ILedgerRepository ledgerRepository, IParameterRepository parameterRepository)
        {
            _ledgerRepository = ledgerRepository;
            _parameterManager = new ParameterManager(parameterRepository);
            LedgerEndDate = DateTime.Now;
            _message = new Message();
        }

        public DateTime LedgerEndDate { get; set; }

        public bool Validate(Project project, Head head, bool showAllAdvance)
        {
            if (project == null)
            {
                _message.MessageText = "Project is not selected.";
                _message.MessageType = MessageType.Error;
                return false;
            }

            if (!showAllAdvance && head == null)
            {
                _message.MessageText = "Account head is not selected.";
                _message.MessageType = MessageType.Error;
                return false;
            }

            return true;
        }

        public Message GetLatestMessage()
        {
            return _message;
        }

        public IList<Ledger> GetLedgerBook(int projectId, int headId, bool isCashBankShown = false)
        {
            DateTime financialYearStartDate = _parameterManager.GetFinancialYearStartDate();

            return
                _ledgerRepository.GetLedger(projectId, headId).OrderBy(l => l.Date).Where(
                    l => GetDateAt12AM(l.Date) >= financialYearStartDate && GetDateAt12AM(l.Date) <= LedgerEndDate).
                    ToList();
        }

        public IList<Ledger> GetAllAdvance(int projectId)
        {
            return _ledgerRepository.GetLedger(projectId);
        }

        private DateTime GetDateAt12AM(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day);
        }
    }
}
