using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using BLL.Factories;
using BLL.Messaging;
using BLL.Model.Entity;
using BLL.Model.Managers;
using BLL.Model.Repositories;
using CodeFirst;

namespace GKS.Model.ViewModels
{
    public class LedgerViewModel : ViewModelBase
    {
        private readonly IProjectManager _projectManager;
        private readonly IHeadManager _headManager;
        private readonly ILedgerManager _ledgerManager;

        public LedgerViewModel()
        {
            //IRepository<Record> ledgerRepository = GKSFactory.GetRepository<Record>();
            try
            {
                IRepository<Record> ledgerRepository = new Repository<Record>();

                _ledgerManager = BLLCoreFactory.GetLedgerManager();
                _headManager = BLLCoreFactory.GetHeadManager();
                _projectManager = BLLCoreFactory.GetProjectManager();

                AllProjects = _projectManager.GetProjects();

                IsAllHeadsEnabled = true;
                ShowAllAdvance = false;
                LedgerEndDate = DateTime.Now;
                IsAllHeadsEnabled = true;
                ShowAllAdvance = false;
                LedgerEndDate = DateTime.Now;
            }
            catch { }
        }

        private IList<Project> _allProjects;
        public IList<Project> AllProjects
        {
            get
            {
                return _allProjects;
            }
            set
            {
                _allProjects = value;
                NotifyPropertyChanged("AllProjects");
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
                NotifyPropertyChanged("AllHeads");
                SelectedHead = null;
            }
        }

        public IList<Head> AllHeads
        {
            get
            {
                if (SelectedProject == null) return new List<Head>();
                return _headManager.GetHeads(SelectedProject);
            }
        }

        private bool _isAllHeadsEnabled;
        public bool IsAllHeadsEnabled
        {
            get { return _isAllHeadsEnabled; }
            set
            {
                _isAllHeadsEnabled = value;
                NotifyPropertyChanged("IsAllHeadsEnabled");
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
                NotifyPropertyChanged("SelectedHead");
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
                NotifyPropertyChanged("ShowAllAdvance");
                SetAllHeadsIsEnabled();
            }
        }

        private DateTime _ledgerEndDate;
        public DateTime LedgerEndDate
        {
            get { return _ledgerEndDate; }
            set
            {
                _ledgerEndDate = value;
                NotifyPropertyChanged("LedgerEndDate");
            }
        }

        private IList<Record> _ledgerGridViewItems;
        public IList<Record> LedgerGridViewItems
        {
            get
            {
                return _ledgerGridViewItems;
            }
            set
            {
                _ledgerGridViewItems = value;
                NotifyPropertyChanged("LedgerGridViewItems");
                NotifyPropertyChanged("LedgerDataGrid");
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                NotifyPropertyChanged("ErrorMessage");
            }
        }

        private string _colorCode;
        public string ColorCode
        {
            get { return _colorCode; }
            private set
            {
                _colorCode = value;
                NotifyPropertyChanged("ColorCode");
            }
        }

        public void ClearMessage()
        {
            Message message = new Message();
            ColorCode = MessageService.Instance.GetColorCode(message.MessageType);
            ErrorMessage = message.MessageText;
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

        #region Relay Commands

        private RelayCommand _ledgerViewButtonClicked;
        public ICommand LedgerViewButtonClicked
        {
            get
            {
                return _ledgerViewButtonClicked ??
                       (_ledgerViewButtonClicked = new RelayCommand(p1 => this.NotifyLedgerGrid()));
            }
        }

        private RelayCommand _refreshButtonClicked;
        public ICommand RefreshButtonClicked
        {
            get { return _refreshButtonClicked ?? (_refreshButtonClicked = new RelayCommand(p1 => this.Reset())); }
        }

        private void NotifyLedgerGrid()
        {
            if (!_ledgerManager.Validate(SelectedProject, SelectedHead, ShowAllAdvance))
            {
                Message latestMessage = MessageService.Instance.GetLatestMessage();
                ErrorMessage = latestMessage.MessageText;
                ColorCode = MessageService.Instance.GetColorCode(latestMessage.MessageType);
                return;
            }
            ClearMessage();

            _ledgerManager.LedgerEndDate = LedgerEndDate;
            if (!ShowAllAdvance)
            {
                LedgerGridViewItems = _ledgerManager.GetLedgerBook(SelectedProject, SelectedHead).ToList();
            }
            else
            {
                LedgerGridViewItems = _ledgerManager.GetAllAdvance(SelectedProject);
            }
        }

        private void Reset()
        {
            AllProjects = _projectManager.GetProjects();
        }

        #endregion

        public IList<LedgerItem> LedgerDataGrid
        {
            get
            {
                if (LedgerGridViewItems == null || LedgerGridViewItems.Count == 0) return null;
                double balance = 0;
                return LedgerGridViewItems.Select(l => GetLedgerItem(l, ref balance)).ToList();

            }
        }

        private LedgerItem GetLedgerItem(Record l, ref double balance)
        {
            bool isBankTag = !string.IsNullOrWhiteSpace(l.Tag) && l.Tag.Contains("Bank");
            bool isCashTag = !string.IsNullOrWhiteSpace(l.Tag) && l.Tag.Contains("Cash");

            string chequeNo = "";
            if (isBankTag && l.LedgerType.Equals("LedgerBook", StringComparison.OrdinalIgnoreCase))
            {
                // TODO: This is a temporary and really lame solution to find the bank record's fields.
                Record bankRecord = _ledgerManager.GetNextRecord(l.ID);
                chequeNo = bankRecord.BankBooks.Where(br => br.Record.ID == bankRecord.ID).Select(br => br.ChequeNo).SingleOrDefault();
            }
            else if (isBankTag && l.LedgerType.Equals("BankBook", StringComparison.OrdinalIgnoreCase))
            {
                chequeNo = l.BankBooks.Where(br => br.Record.ID == l.ID).Select(br => br.ChequeNo).SingleOrDefault();
            }

            return new LedgerItem
                       {
                           Date = l.Date,
                           VoucherNo = l.VoucherType + "-" + l.VoucherSerialNo,
                           Debit = l.Debit,
                           Credit = l.Credit,
                           Balance = (balance += (l.Debit - l.Credit)),
                           Particular = isCashTag ? "Cash" : "Bank",
                           Remarks = l.Narration,
                           ChequeNo = chequeNo,
                       };
        }
    }

    public class LedgerItem
    {
        public DateTime Date { get; set; }
        public string VoucherNo { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
        public double Balance { get; set; }
        public string Particular { get; set; }
        public string ChequeNo { get; set; }
        public string Remarks { get; set; }
    }

    //public class ViewableLedgerRows : List<LedgerItem> { }
}
