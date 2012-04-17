using System;
using BLL.BudgetManagement;
using BLL.LedgerManagement;
using BLL.Messaging;
using BLL.Model.Managers;
using BLL.Model.Repositories;
using BLL.ParameterManagement;
using BLL.ProjectManagement;
using BLL.VoucherManagement;

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

        public static ILedgerManager GetLedgerManager()
        {
            if (LedgerRepository != null && ProjectRepository != null)
            {
                LedgerManager ledgerManager = new LedgerManager(LedgerRepository, ParameterRepository);
                AddMessageHandler(ledgerManager);
                return ledgerManager;
            }
            throw new ArgumentNullException("LedgerRepository or ProjectRepository cannot be null.");
        }

        public static IParameterManager GetParameterManager()
        {
            if (ParameterRepository != null)
            {
                ParameterManager parameterManager = new ParameterManager(ParameterRepository);
                AddMessageHandler(parameterManager);
                return parameterManager;
            }
            throw new ArgumentNullException("ParameterRepository cannot be null.");
        }

        public static IMassVoucherManager GetMassVoucherManager()
        {
            if (RecordRepository != null && ProjectRepository != null && HeadRepository != null)
            {
                MassVoucherManager massVoucherManager = new MassVoucherManager(RecordRepository, ProjectRepository, HeadRepository);
                AddMessageHandler(massVoucherManager);
                return massVoucherManager;
            }
            throw new ArgumentNullException("RecordRepository, ProjectRepository or HeadRepository cannot be null.");
        }

        public static IRecordManager GetRecordManager()
        {
            if (RecordRepository != null)
            {
                RecordManager recordManager = new RecordManager(RecordRepository);
                AddMessageHandler(recordManager);
                return recordManager;
            }
            throw new ArgumentNullException("RecordRepository cannot be null.");
        }

        public static IHeadManager GetHeadManager()
        {
            if (HeadRepository != null)
            {
                HeadManager headManager = new HeadManager(HeadRepository);
                AddMessageHandler(headManager);
                return headManager;
            }
            throw new ArgumentNullException("HeadRepository cannot be null.");
        }

        public static IProjectManager GetProjectManager()
        {
            if (ProjectRepository != null && HeadRepository != null && RecordRepository != null)
            {
                ProjectManager projectManager = new ProjectManager(ProjectRepository, HeadRepository, RecordRepository);
                AddMessageHandler(projectManager);
                return projectManager;
            }
            throw new ArgumentNullException("ProjectRepository, HeadRepository or RecordRepository cannot be null.");
        }

        public static IBudgetManager GetBudgetManager()
        {
            if (ProjectRepository != null && BudgetRepository != null )
            {
                BudgetManager budgetManager = new BudgetManager(BudgetRepository, ProjectRepository);
                AddMessageHandler(budgetManager);
                return budgetManager;
            }
            throw new ArgumentNullException("ProjectRepository or BudgetRepository cannot be null.");
        }

        private static void AddMessageHandler(ManagerBase manager)
        {
            manager.ManagerEvent += MessageService.Instance.ManagerEventHandler;
        }
    }
}
