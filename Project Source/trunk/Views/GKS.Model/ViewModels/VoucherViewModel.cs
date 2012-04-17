using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;
//using BLL.LedgerManagement;
using BLL.Factories;
using BLL.Messaging;
using BLL.Model.Managers;
using BLL.Model.Schema;
//using BLL.ProjectManagement;
using GKS.Factory;
using BLL.Model.Repositories;
//using BLL.ParameterManagement;

namespace GKS.Model.ViewModels
{
    public class VoucherViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IProjectManager _projectManager;
        private readonly IHeadManager _headManager;
        private readonly ILedgerManager _ledgerManager;
        private readonly IParameterManager _parameterManager;
        public VoucherViewModel()
        {
            //IProjectRepository projectRepository = GKSFactory.GetProjectRepository();
            //IHeadRepository headRepository = GKSFactory.GetHeadRepository();
            //IRecordRepository recordRepository = GKSFactory.GetRecordRepository();
            //ILedgerRepository ledgerRepository = GKSFactory.GetLedgerRepository();
            //IParameterRepository parameterRepository = GKSFactory.GetParameterRepository();

            _projectManager = BLLCoreFactory.GetProjectManager();
            _headManager = BLLCoreFactory.GetHeadManager();
            _ledgerManager = BLLCoreFactory.GetLedgerManager();
            _parameterManager = BLLCoreFactory.GetParameterManager();

            AllProjects = _projectManager.GetProjects();
            SelectedVoucherType = "Debit voucher";
            VoucherStartDate = _parameterManager.GetFinancialYearStartDate(); // TODO: Should be first day of current finanical year.
            VoucherEndDate = DateTime.Today;

            VoucherDetailsButtonClicked = new ViewVoucherDetails(this);
        }

        public IList<Project> _allProjects;
        public IList<Project> AllProjects
        {
            get
            {
                return _allProjects;
            }
            set
            {
                _allProjects = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("AllProjects"));
            }
        }

        private Project _selectedProject;
        public Project SelectedProject
        {
            get
            {
                return _selectedProject;
            }
            set
            {
                _selectedProject = value;

                if (PropertyChanged != null)
                {
                    //PropertyChanged(this, new PropertyChangedEventArgs("AllHeads"));
                    //PropertyChanged(this, new PropertyChangedEventArgs("SelectedHead"));
                }
            }
        }

        public string[] VoucherTypes
        {
            get
            {
                return new[] { "Debit voucher", "Credit voucher", "Journal voucher", "Contra" };
            }
        }

        private string _selectedVoucherType;
        public string SelectedVoucherType
        {
            get { return _selectedVoucherType; }
            set
            {
                _selectedVoucherType = value;
                //VoucherTypeChanged(); // All small event moved to inside the function
                if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("SelectedVoucherType")); }
            }
        }

        private DateTime _voucherStartDate;
        public DateTime VoucherStartDate
        {
            get { return _voucherStartDate; }
            set
            {
                _voucherStartDate = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("FinacialYearEndDate"));
            }
        }

        private DateTime _voucherEndDate;
        public DateTime VoucherEndDate
        {
            get { return _voucherEndDate; }
            set
            {
                _voucherEndDate = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("FinacialYearEndDate"));
            }
        }

        public IList<Ledger> VoucherGridViewItems
        {
            get
            {
                //if (!_ledgerManager.Validate(SelectedProject, SelectedHead, ShowAllAdvance))
                //{
                //    Message latestMessage = _ledgerManager.GetLatestMessage();
                //    ErrorMessage = latestMessage.MessageText;
                //    ColorCode = MessageService.Instance.GetColorCode(latestMessage.MessageType);
                //    return null;
                //}

                ClearMessage();
                _ledgerManager.LedgerEndDate = VoucherEndDate;
                //double balance = 0;
                //return _ledgerManager.GetLedgerBook(SelectedProject.Id, SelectedHead.Id).Select(l =>
                //new Ledger
                //{
                //    VoucherNo = l.VoucherNo,
                //    Date = l.Date,
                //    ChequeNo = l.ChequeNo,
                //    Debit = l.Debit,
                //    Credit = l.Credit,
                //    Balance = balance += l.Debit - l.Credit,
                //    Particular = l.Particular,
                //    Remarks = l.Remarks
                //}).ToList();
                return null;
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ErrorMessage"));
            }
        }

        private string _colorCode;
        public string ColorCode
        {
            get { return _colorCode; }
            private set
            {
                _colorCode = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ColorCode"));
            }
        }

        public ICommand VoucherDetailsButtonClicked { get; set; }

        public void NotifyVoucherGrid()
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("VoucherGridViewItems"));
        }

        public void ClearMessage()
        {
            Message message = new Message();
            ColorCode = MessageService.Instance.GetColorCode(message.MessageType);
            ErrorMessage = message.MessageText;
        }

        public void Reset()
        {
            AllProjects = _projectManager.GetProjects();
        }
    }

    public class ViewVoucherDetails : ICommand
    {
        private VoucherViewModel _viewVoucherModel;
        public ViewVoucherDetails(VoucherViewModel viewVoucherModel)
        {
            _viewVoucherModel = viewVoucherModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _viewVoucherModel.NotifyVoucherGrid();
        }
    }
}
