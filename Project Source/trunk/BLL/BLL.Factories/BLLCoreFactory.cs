using System;
using BLL.LedgerManagement;
using BLL.Messaging;
using BLL.Model.Managers;
using BLL.Model.Repositories;
using BLL.ParameterManagement;

namespace BLL.Factories
{
    public class BLLCoreFactory
    {
        public static IBudgetRepository BudgetRepository { get; set; }
        public static IHeadRepository HeadRepository { get; set; }
        public static ILedgerRepository LedgerRepository { get; set; }
        public static IParameterRepository ParameterRepository { get; set; }
        public static IProjectRepository ProjectRepository { get; set; }
        public static IRecordRepository RecordRepository { get; set; }

        public static ILedgerManager  GetLedgerManager()
        {
            if (LedgerRepository != null && ProjectRepository != null)
            {
                LedgerManager ledgerManager = new LedgerManager(LedgerRepository, ParameterRepository);
                ledgerManager.ManagerEvent += MessageService.Instance.ManagerEventHandler;
                //ledgerManager.LedgerEvent += LogService.Instance.ManagerEventHandler;
                return ledgerManager;
            }

            throw new ArgumentNullException("message");
        }

        public static IParameterManager GetParameterManager()
        {
            if (ParameterRepository != null)
            {
                ParameterManager parameterManager = new ParameterManager(ParameterRepository);
                //parameterManager.ManagerEvent += MessageService.Instance.ManagerEventHandler;
                //ledgerManager.LedgerEvent += LogService.Instance.ManagerEventHandler;
                return parameterManager;
            }

            throw new ArgumentNullException("message");
        }

    }
}
