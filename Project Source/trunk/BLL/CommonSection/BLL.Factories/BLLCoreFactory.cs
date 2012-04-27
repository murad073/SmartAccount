using System;
using BLL.LedgerManagement;
using BLL.Messaging;
using BLL.Model.Entity;
using BLL.Model.Managers;
using BLL.Model.Repositories;
using BLL.ParameterManagement;
using BLL.ProjectManagement;
using BLL.VoucherManagement;

namespace BLL.Factories
{
    public class BLLCoreFactory
    {
        //public static IBudgetRepository BudgetRepository { get; set; }
        //public static IHeadRepository HeadRepository { get; set; }
        //public static ILedgerRepository LedgerRepository { get; set; }
        //public static IParameterRepository ParameterRepository { get; set; }
        //public static IProjectRepository ProjectRepository { get; set; }
        //public static IRecordRepository RecordRepository { get; set; }

        public static IRepository<Budget> BudgetRepository { get; set; }
        public static IRepository<Head> HeadRepository { get; set; }
        public static IRepository<Project> ProjectRepository { get; set; }
        public static IRepository<ProjectHead> ProjectHeadRepository { get; set; }
        public static IRepository<Record> RecordRepository { get; set; }
        public static IRepository<Parameter> ParameterRepository { get; set; }

        public static ILedgerManager  GetLedgerManager()
        {            
            if (RecordRepository != null && ProjectRepository != null)
            {
                LedgerManager ledgerManager = new LedgerManager(RecordRepository, ParameterRepository);
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

        public static IMassVoucherManager GetMassVoucherManager()
        {
            if (RecordRepository != null && ProjectRepository != null && HeadRepository !=null)
            {
                MassVoucherManager ledgerManager = new MassVoucherManager(RecordRepository, ProjectRepository, HeadRepository);
                ledgerManager.ManagerEvent += MessageService.Instance.ManagerEventHandler;
                //ledgerManager.LedgerEvent += LogService.Instance.ManagerEventHandler;
                return ledgerManager;
            }

            throw new ArgumentNullException("message");
        }

        public static IRecordManager GetRecordManager()
        {
            if (RecordRepository != null)
            {
                RecordManager ledgerManager = new RecordManager(RecordRepository);
                ledgerManager.ManagerEvent += MessageService.Instance.ManagerEventHandler;
                //ledgerManager.LedgerEvent += LogService.Instance.ManagerEventHandler;
                return ledgerManager;
            }

            throw new ArgumentNullException("message");
        }

        public static IHeadManager GetHeadManager()
        {
            if (HeadRepository != null)
            {
                HeadManager ledgerManager = new HeadManager(HeadRepository);
                ledgerManager.ManagerEvent += MessageService.Instance.ManagerEventHandler;
                //ledgerManager.LedgerEvent += LogService.Instance.ManagerEventHandler;
                return ledgerManager;
            }

            throw new ArgumentNullException("message");
        }

        public static IProjectManager GetProjectManager()
        {
            if (ProjectRepository!=null && HeadRepository != null && RecordRepository!=null)
            {
                ProjectManager ledgerManager = new ProjectManager(ProjectRepository, HeadRepository, RecordRepository);
                ledgerManager.ManagerEvent += MessageService.Instance.ManagerEventHandler;
                //ledgerManager.LedgerEvent += LogService.Instance.ManagerEventHandler;
                return ledgerManager;
            }

            throw new ArgumentNullException("message");
        }
    }
}
