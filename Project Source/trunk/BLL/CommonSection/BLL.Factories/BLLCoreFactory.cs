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
        public static IRepository<Budget> BudgetRepository { get; set; }
        public static IRepository<Head> HeadRepository { get; set; }
        public static IRepository<Project> ProjectRepository { get; set; }
        public static IRepository<ProjectHead> ProjectHeadRepository { get; set; }
        public static IRepository<Record> RecordRepository { get; set; }
        public static IRepository<Parameter> ParameterRepository { get; set; }
        public static IRepository<FixedAsset> FixedAssetRepository { get; set; }
        public static IRepository<BankRecord> BankRecordRepository { get; set; }

        public static ILedgerManager GetLedgerManager()
        {
            if (RecordRepository != null && ProjectRepository != null)
            {
                LedgerManager ledgerManager = new LedgerManager(RecordRepository, GetParameterManager());
                ledgerManager.ManagerEvent += MessageService.Instance.ManagerEventHandler;
                //ledgerManager.LedgerEvent += LogService.Instance.ManagerEventHandler;
                return ledgerManager;
            }

            throw new ArgumentNullException("message");
        }

        public static IVoucherManager GetVoucherManager()
        {
            if (RecordRepository != null && ProjectRepository != null)
            {
                VoucherManager voucherManager = new VoucherManager(RecordRepository);
                voucherManager.ManagerEvent += MessageService.Instance.ManagerEventHandler;
                //ledgerManager.LedgerEvent += LogService.Instance.ManagerEventHandler;
                return voucherManager;
            }

            throw new ArgumentNullException("message");
        }

        public static IParameterManager GetParameterManager()
        {
            if (ParameterRepository != null)
            {
                ParameterManager parameterManager = new ParameterManager(ParameterRepository);
                //parameterManager.ManagerEvent += MessageService.Instance.ManagerEventHandler;
                //parameterManager.LedgerEvent += LogService.Instance.ManagerEventHandler;
                return parameterManager;
            }

            throw new ArgumentNullException("message");
        }

        public static IMassVoucherManager GetMassVoucherManager()
        {
            if (RecordRepository != null && ProjectRepository != null && HeadRepository != null)
            {
                MassVoucherManager massVoucherManager = new MassVoucherManager(RecordRepository, ProjectRepository, HeadRepository, ProjectHeadRepository, FixedAssetRepository, BankRecordRepository);
                massVoucherManager.ManagerEvent += MessageService.Instance.ManagerEventHandler;
                //massVoucherManager.LedgerEvent += LogService.Instance.ManagerEventHandler;
                return massVoucherManager;
            }

            throw new ArgumentNullException("message");
        }

        public static IRecordManager GetRecordManager()
        {
            if (RecordRepository != null)
            {
                RecordManager recordManager = new RecordManager(RecordRepository);
                recordManager.ManagerEvent += MessageService.Instance.ManagerEventHandler;
                //recordManager.LedgerEvent += LogService.Instance.ManagerEventHandler;
                return recordManager;
            }

            throw new ArgumentNullException("message");
        }

        public static IHeadManager GetHeadManager()
        {
            if (ProjectHeadRepository != null && HeadRepository != null)
            {
                HeadManager headManager = new HeadManager(ProjectHeadRepository, HeadRepository);
                headManager.ManagerEvent += MessageService.Instance.ManagerEventHandler;
                //headManager.LedgerEvent += LogService.Instance.ManagerEventHandler;
                return headManager;
            }

            throw new ArgumentNullException("message");
        }

        public static IProjectManager GetProjectManager()
        {
            if (ProjectRepository != null && HeadRepository != null && ProjectHeadRepository != null && RecordRepository != null)
            {
                ProjectManager projectManager = new ProjectManager(ProjectRepository, HeadRepository, ProjectHeadRepository, RecordRepository);
                projectManager.ManagerEvent += MessageService.Instance.ManagerEventHandler;
                //projectManager.LedgerEvent += LogService.Instance.ManagerEventHandler;
                return projectManager;
            }

            throw new ArgumentNullException("message");
        }
    }
}
