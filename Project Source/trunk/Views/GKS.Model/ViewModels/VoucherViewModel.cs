using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;
using BLL.Factories;
using BLL.Messaging;
using BLL.Model.Entity;
using BLL.Model.Managers;
using BLL.Model.Repositories;

namespace GKS.Model.ViewModels
{
    public class VoucherViewModel : ViewModelBase
    {

        private readonly IProjectManager _projectManager;
        private readonly IVoucherManager _voucherManager;
        private readonly IParameterManager _parameterManager;
        public VoucherViewModel()
        {
            try
            {
                _projectManager = BLLCoreFactory.GetProjectManager();
                _voucherManager = BLLCoreFactory.GetVoucherManager();
                _parameterManager = BLLCoreFactory.GetParameterManager(); // TODO: Do we need this?

                AllProjects = _projectManager.GetProjects();
                SelectedVoucherType = VoucherTypes[0];
                VoucherStartDate = DateTime.Today;
                VoucherEndDate = DateTime.Today;
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
                NotifyPropertyChanged("SelectedProject");
            }
        }

        public KeyValuePair<string, string>[] VoucherTypes
        {
            get
            {
                return new[]
                           {
                               new KeyValuePair<string, string>("All", "All"),
                               new KeyValuePair<string, string>("DV", "Debit voucher"),
                               new KeyValuePair<string, string>("CV", "Credit voucher"),
                               new KeyValuePair<string, string>("JV", "Journal voucher"),
                               new KeyValuePair<string, string>("Contra", "Contra")
                           };
            }
        }

        private KeyValuePair<string, string> _selectedVoucherType;
        public KeyValuePair<string, string> SelectedVoucherType
        {
            get { return _selectedVoucherType; }
            set
            {
                _selectedVoucherType = value;
                NotifyPropertyChanged("SelectedVoucherType");
            }
        }

        private DateTime _voucherStartDate;
        public DateTime VoucherStartDate
        {
            get { return _voucherStartDate; }
            set
            {
                _voucherStartDate = value;
                NotifyPropertyChanged("VoucherStartDate");
            }
        }

        private DateTime _voucherEndDate;
        public DateTime VoucherEndDate
        {
            get { return _voucherEndDate; }
            set
            {
                _voucherEndDate = value;
                NotifyPropertyChanged("VoucherEndDate");
            }
        }

        private IList<Record> _voucherGridViewItems;
        public IList<Record> VoucherGridViewItems
        {
            get
            {
                return _voucherGridViewItems;
            }
            set
            {
                _voucherGridViewItems = value;
                NotifyPropertyChanged("VoucherGridViewItems");
                NotifyPropertyChanged("VoucherDataGrid");
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

        private RelayCommand _voucherViewButtonClicked;
        public ICommand VoucherViewButtonClicked
        {
            get { return _voucherViewButtonClicked ?? (_voucherViewButtonClicked = new RelayCommand(p1 => this.NotifyVoucherGrid())); }
        }

        private RelayCommand _refreshButtonClicked;
        public ICommand RefreshButtonClicked
        {
            get { return _refreshButtonClicked ?? (_refreshButtonClicked = new RelayCommand(p1 => this.Reset())); }
        }
       
        private RelayCommand _voucherDetailsButtonClicked;
        public ICommand VoucherDetailsButtonClicked
        {
            get
            {
                return _voucherDetailsButtonClicked ?? (_voucherDetailsButtonClicked = new RelayCommand(p1 => NotifyPropertyChanged("VoucherGridViewItems")));
            }
        }

        private void NotifyVoucherGrid()
        {
            //if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("VoucherGridViewItems"));

            if (!_voucherManager.Validate(SelectedProject, VoucherStartDate, VoucherEndDate))
            {
                Message latestMessage = MessageService.Instance.GetLatestMessage();
                ErrorMessage = latestMessage.MessageText;
                ColorCode = MessageService.Instance.GetColorCode(latestMessage.MessageType);
                VoucherGridViewItems = null;
                return;
            }

            ClearMessage();

            _voucherManager.VoucherStartDate = VoucherStartDate;
            _voucherManager.VoucherEndDate = VoucherEndDate;

            VoucherGridViewItems = _voucherManager.GetVouchers(SelectedProject, SelectedVoucherType.Key);
        }

        public IList<VoucherItem> VoucherDataGrid
        {
            get
            {
                if (VoucherGridViewItems == null || VoucherGridViewItems.Count == 0) return null;
                return VoucherGridViewItems.Select(v => new VoucherItem
                {
                    ProjectName = v.ProjectHead.Project.Name,
                    Date = v.Date,
                    VoucherNo = v.VoucherType + "-" + v.VoucherSerialNo,
                    HeadOfAccount = v.ProjectHead.Head.Name,                    
                    Amount = v.Debit + v.Credit,
                    //CashOrBank = (v.BankRecords.Select(br => br.Record.ID == v.ID) == null ? "Cash" : "Bank"), // TODO: It doesn't work.
                    CashOrBank = v.Tag.Contains("Cash") ? "Cash" : "Bank",
                    Narration = v.Narration
                }).ToList();

            }
        }

        private VoucherItem _selectedVoucherItem;
        public VoucherItem SelectedVoucherItem
        {
            get
            {
                return _selectedVoucherItem;
            }
            set
            {
                _selectedVoucherItem = value;
                NotifyPropertyChanged("SelectedVoucherItem");
            }
        }

        private void ClearMessage()
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

    public class VoucherItem
    {
        public string ProjectName { get; set; }
        public DateTime Date { get; set; }
        public string VoucherNo { get; set; }
        public string HeadOfAccount { get; set; }       
        public double Amount { get; set; }
        public string CashOrBank { get; set; }
        public string Narration { get; set; }
    }
}
