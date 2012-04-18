using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using BLL.Factories;
//using BLL.LedgerManagement;
using BLL.Messaging;
using BLL.Model.Managers;
using BLL.Model.Schema;
//using BLL.ProjectManagement;
using GKS.Factory;
using BLL.Model.Repositories;

namespace GKS.Model.ViewModels
{

    public class LedgerViewModel : INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion


        private readonly IProjectManager _projectManager;
        private readonly IHeadManager _headManager;
        private readonly ILedgerManager _ledgerManager;
        public LedgerViewModel()
        {
            //IProjectRepository projectRepository = GKSFactory.GetProjectRepository();
            //IHeadRepository headRepository = GKSFactory.GetHeadRepository();
            //IRecordRepository recordRepository = GKSFactory.GetRecordRepository();
            ILedgerRepository ledgerRepository = GKSFactory.GetLedgerRepository();
            //IParameterRepository parameterRepository = GKSFactory.GetParameterRepository();

            //_projectManager = new ProjectManager(projectRepository, headRepository, recordRepository);
            //_headManager = new HeadManager(headRepository);
            //_ledgerManager =  new LedgerManager(ledgerRepository, parameterRepository);

            _ledgerManager = BLLCoreFactory.GetLedgerManager();
            _headManager = BLLCoreFactory.GetHeadManager();
            _projectManager = BLLCoreFactory.GetProjectManager();

            AllProjects = _projectManager.GetProjects();

            //IList<Project> projects = _projectManager.GetProjects();
            //Projects = new SelectionList<string>(projects.Select(p => p.Name)); -- For multi selection list.

            IsAllHeadsEnabled = true;
            ShowAllAdvance = false;
            LedgerEndDate = DateTime.Now;
            LedgerViewButtonClicked = new ViewLedger(this, ledgerRepository);

            //ClearMessage();
            //SelectedProject = _projectManager.GetDefaultProject();
        }

        #region Event invokers
        // Needed when we have multi-select list-box.
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler changed = PropertyChanged;
            if (changed != null) changed(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

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
                    PropertyChanged(this, new PropertyChangedEventArgs("AllHeads"));
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectedHead"));
                }
            }
        }

        public IList<Head> AllHeads
        {
            get
            {
                if (SelectedProject == null || SelectedProject.Id <= 0) return null;
                return _headManager.GetHeads(SelectedProject.Id).ToList();
            }
        }

        private bool _isAllHeadsEnabled;
        public bool IsAllHeadsEnabled
        {
            get { return _isAllHeadsEnabled; }
            set
            {
                _isAllHeadsEnabled = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("IsAllHeadsEnabled"));
            }
        }

        private Head _selectedHead;
        public Head SelectedHead
        {
            get
            {
                return _selectedHead;
            }
            set
            {
                _selectedHead = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("SelectedHead"));
            }
        }

        private bool _showCashOrBankTransaction;
        public bool ShowCashOrBankTransaction
        {
            get { return _showCashOrBankTransaction; }
            set
            {
                _showCashOrBankTransaction = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ShowCashOrBankTransaction"));
                }
            }
        }

        private bool _showAllAdvance;
        public bool ShowAllAdvance
        {
            get
            {
                return _showAllAdvance;
            }
            set
            {
                _showAllAdvance = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ShowAllAdvance"));
                    SetAllHeadsIsEnabled();
                }
            }
        }

        private DateTime _ledgerEndDate;
        public DateTime LedgerEndDate
        {
            get { return _ledgerEndDate; }
            set
            {
                _ledgerEndDate = value;
                if (PropertyChanged != null)
                {
                    // TODO: Why is it here?
                    PropertyChanged(this, new PropertyChangedEventArgs("FinacialYearEndDate"));
                }
            }
        }

        public IList<Ledger> LedgerGridViewItems
        {
            get
            {
                if (!_ledgerManager.Validate(SelectedProject, SelectedHead, ShowAllAdvance))
                {
                    //Message latestMessage = _ledgerManager.GetLatestMessage();
                    Message latestMessage = MessageService.Instance.GetLatestMessage();
                    ErrorMessage = latestMessage.MessageText;
                    ColorCode = MessageService.Instance.GetColorCode(latestMessage.MessageType);
                    return null;
                }

                ClearMessage();
                _ledgerManager.LedgerEndDate = LedgerEndDate;
                double balance = 0;
                if (!ShowAllAdvance)
                {
                    return _ledgerManager.GetLedgerBook(SelectedProject.Id, SelectedHead.Id).Select(l =>
                    new Ledger
                    {
                        VoucherNo = l.VoucherNo,
                        Date = l.Date,
                        ChequeNo = l.ChequeNo,
                        Debit = l.Debit,
                        Credit = l.Credit,
                        Balance = balance += l.Debit - l.Credit,
                        Particular = l.Particular,
                        Remarks = l.Remarks
                    }).ToList();
                }
                else
                {
                    return _ledgerManager.GetAllAdvance(SelectedProject.Id);
                }
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

        public void ClearMessage()
        {
            Message message = new Message();
            ColorCode = MessageService.Instance.GetColorCode(message.MessageType);
            ErrorMessage = message.MessageText;
        }

        public ICommand LedgerViewButtonClicked { get; set; }

        public void NotifyLedgerGrid()
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("LedgerGridViewItems"));
        }

        public void Reset()
        {
            AllProjects = _projectManager.GetProjects();
        }

        private void SetAllHeadsIsEnabled()
        {
            if (ShowAllAdvance)
            {
                SelectedHead = null;
                IsAllHeadsEnabled = false;
            }
            else IsAllHeadsEnabled = true;
        }
    }

    public class ViewLedger : ICommand
    {
        private LedgerViewModel _ledgerViewModel;
        private ILedgerRepository _ledgerRepository;
        public ViewLedger(LedgerViewModel ledgerViewModel, ILedgerRepository ledgerRepository)
        {
            _ledgerViewModel = ledgerViewModel;
            _ledgerRepository = ledgerRepository;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _ledgerViewModel.NotifyLedgerGrid();
        }
    }
}
