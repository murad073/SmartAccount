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
        private readonly ILedgerManager _ledgerManager;
        public VoucherViewModel()
        {
            try
            {
                _projectManager = BLLCoreFactory.GetProjectManager();
                _ledgerManager = BLLCoreFactory.GetLedgerManager();

                AllProjects = _projectManager.GetProjects();
                SelectedVoucherType = VoucherTypes[0];
                //VoucherStartDate = _parameterManager.GetFinancialYearStartDate(); // TODO: Should be first day of current finanical year.
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

        public IList<Record> VoucherGridViewItems
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
                //_ledgerManager.GetLedgerBook(SelectedProject.Id, )

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

                //TODO: murad will do that - > new class and do all union
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

        private RelayCommand _voucherDetailsButtonClicked;
        public ICommand VoucherDetailsButtonClicked
        {
            get
            {
                return _voucherDetailsButtonClicked ?? (_voucherDetailsButtonClicked = new RelayCommand(p1 => NotifyPropertyChanged("VoucherGridViewItems")));
            }
        }

        private RelayCommand _refreshClicked;
        public ICommand RefreshClicked
        {
            get
            {
                return _refreshClicked ?? (_refreshClicked = new RelayCommand(p1 => this.Reset()));
            }
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

    public class VoucherItem
    {
        public string Date { get; set; }
        public string VoucherNo { get; set; }
        public string HeadOfAccount { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
        public string CashOrBank { get; set; }
    }
}
