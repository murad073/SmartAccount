using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using BLL.Factories;
using BLL.Messaging;
using BLL.Model.Entity;
using BLL.Model.Managers;
using BLL.Model.Schema;
using BLL.Utils;
using System.Windows.Data;


namespace GKS.Model.ViewModels
{
    public class VoucherPost : ViewModelBase
    {
        private readonly IProjectManager _projectManager;
        private readonly IHeadManager _headManager;
        private readonly IMassVoucherManager _massVoucherManager;
        private readonly IParameterManager _parameterManager;

        public VoucherPost()
        {
            //try
            //{
                _massVoucherManager = BLLCoreFactory.GetMassVoucherManager();
                _projectManager = BLLCoreFactory.GetProjectManager();
                _headManager = BLLCoreFactory.GetHeadManager();
                _parameterManager = BLLCoreFactory.GetParameterManager();

                InputFirstPartEnabled = true;
                InputSecondPartEnabled = true;
                AllProjects = _projectManager.GetProjects(false);

                SelectedVoucherType = "DV";

                VoucherDate = DateTime.Now;
                ChequeDate = DateTime.Now;

                TemporaryRecords = new List<Record>();
                _isJVBalanced = true;
            //}
            //catch { }
        }

        private bool _isInputFirstPartEnabled;
        public bool InputFirstPartEnabled
        {
            get { return _isInputFirstPartEnabled; }
            set
            {
                _isInputFirstPartEnabled = value;
                NotifyPropertyChanged("InputFirstPartEnabled");
            }
        }

        private bool _isInputSecondPartEnabled;
        public bool InputSecondPartEnabled
        {
            get { return _isInputSecondPartEnabled; }
            set
            {
                _isInputSecondPartEnabled = value;
                NotifyPropertyChanged("InputSecondPartEnabled");
            }
        }

        #region Project Head section

        private IList<Project> _allProjects;
        public IList<Project> AllProjects
        {
            get { return _allProjects; }
            set
            {
                _allProjects = value;
                NotifyPropertyChanged("AllProjects");
            }
        }

        private Project _selectedProject;
        public Project SelectedProject
        {
            get { return _selectedProject; }
            set
            {
                _selectedProject = value;
                SetVoucherSerialNo();
                SetAllHeads();
                NotifyPropertyChanged("SelectedProject");
            }
        }

        private IList<Head> _allHeads;
        public IList<Head> AllHeads
        {
            get { return _allHeads; }
            set
            {
                _allHeads = value;
                NotifyPropertyChanged("AllHeads");
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
            get { return _selectedHead; }
            set
            {
                _selectedHead = value;
                SetFixedAssetOrAdvanceGroupboxIsEnabled();
                NotifyPropertyChanged("SelectedHead");
            }
        }

        public string[] VoucherTypes
        {
            get
            {
                return new[] { "DV", "CV", "JV", "Contra" };
            }
        }

        private string _selectedVoucherType;
        public string SelectedVoucherType
        {
            get { return _selectedVoucherType; }
            set
            {
                _selectedVoucherType = value;
                VoucherTypeChanged();
                NotifyPropertyChanged("SelectedVoucherType");
            }
        }

        private int _voucherSerialNo;
        public int VoucherSerialNo
        {
            get { return _voucherSerialNo; }
            set
            {
                _voucherSerialNo = value;
                NotifyPropertyChanged("VoucherSerialNo");
            }
        }

        private DateTime _voucherDate;
        public DateTime VoucherDate
        {
            get { return _voucherDate; }
            set
            {
                _voucherDate = value;
                NotifyPropertyChanged("VoucherDate");
            }
        }

        private bool _isJVStartedChecked;
        public bool IsJVStartedChecked
        {
            get { return _isJVStartedChecked; }
            set
            {
                _isJVStartedChecked = value;
                // The following function calls should not change order.
                if (value == false)
                {
                    if (TemporaryRecords != null && TemporaryRecords.Count != 0)
                    {
                        // We want the latest narration for all the records of a JV.
                        for (int count = 0; count < TemporaryRecords.Count; count++)
                            TemporaryRecords[count].Narration = Narration;
                    }
                    SetJVBalanceZeroMessage();
                }
                
                SetInputSecondPartIsEnabled();
                SetPostButtonIsEnabled();
                //SetCreateVoucherButtonIsEnabled();
                NotifyPropertyChanged("IsJVStartedChecked");
            }
        }

        private bool _isMultiJVCheckboxVisible;
        public bool IsMultiJVCheckboxVisible
        {
            get { return _isMultiJVCheckboxVisible; }
            set
            {
                _isMultiJVCheckboxVisible = value;
                // The following function calls should not change order.
                SetJVDebitCreditIsEnabled();
                SetCreateVoucherButtonIsEnabled();
                SetJVStartedIsChecked();
                NotifyPropertyChanged("IsMultiJVCheckboxVisible");
            }
        }

        #endregion


        #region Amount Information

        private double _amount;
        public double Amount
        {
            get { return _amount; }
            set
            {
                _amount = value < 0 ? 0 : value;
                AmountChanged();
                NotifyPropertyChanged("Amount");
            }
        }

        public string[] JVDebitCreditItems
        {
            get
            {
                return new[] { "Debit", "Credit" };
            }
        }

        private string _selectedJVDebitOrCredit;
        public string SelectedJVDebitOrCredit
        {
            get { return _selectedJVDebitOrCredit; }
            set
            {
                _selectedJVDebitOrCredit = value;
                SetFixedAssetOrAdvanceGroupboxIsEnabled();
                NotifyPropertyChanged("SelectedJVDebitOrCredit");
            }
        }

        private bool _isJVDebitOrCreditEnabled;
        public bool IsJVDebitOrCreditEnabled
        {
            get { return _isJVDebitOrCreditEnabled; }
            set
            {
                _isJVDebitOrCreditEnabled = value;
                NotifyPropertyChanged("IsJVDebitOrCreditEnabled");
            }
        }


        public string[] ContraTypes
        {
            get
            {
                return new[] { "Bank to cash", "Cash to bank" };
            }
        }

        private string _selectedContraType;
        public string SelectedContraType
        {
            get { return _selectedContraType; }
            set
            {
                _selectedContraType = value;
                SetPaymentInChequeIsChecked();
                SetChequeGroupboxIsEnabled();
                NotifyPropertyChanged("SelectedContraType");
            }
        }

        private bool _isContraTypesEnabled;
        public bool IsContraTypesEnabled
        {
            get { return _isContraTypesEnabled; }
            set
            {
                _isContraTypesEnabled = value;
                NotifyPropertyChanged("IsContraTypesEnabled");
            }
        }

        #endregion


        #region Cheque Information Section

        private bool _isChequeGroupboxEnabled;
        public bool IsChequeGroupboxEnabled
        {
            get { return _isChequeGroupboxEnabled; }
            set
            {
                _isChequeGroupboxEnabled = value;
                NotifyPropertyChanged("IsChequeGroupboxEnabled");
            }
        }

        private bool _isPaymentInCheque;
        public bool IsPaymentInCheque
        {
            get { return _isPaymentInCheque; }
            set
            {
                _isPaymentInCheque = value;
                if (!value)
                {
                    ChequeNo = "";
                    ChequeDate = DateTime.Now;
                    BankName = "";
                }
                NotifyPropertyChanged("IsPaymentInCheque");
            }
        }

        private string _chequeNo;
        public string ChequeNo
        {
            get { return _chequeNo; }
            set
            {
                _chequeNo = value;
                NotifyPropertyChanged("ChequeNo");
            }
        }

        private DateTime _chequeDate;
        public DateTime ChequeDate
        {
            get { return _chequeDate; }
            set
            {
                _chequeDate = value;
                NotifyPropertyChanged("ChequeDate");
            }
        }

        private string _bankName;
        public string BankName
        {
            get { return _bankName; }
            set
            {
                _bankName = value;
                NotifyPropertyChanged("BankName");
            }
        }

        #endregion


        #region Fixed asset section

        private bool _isFixedAssetOrAdvanceGroupboxEnabled;
        public bool IsFixedAssetOrAdvanceGroupboxEnabled
        {
            get { return _isFixedAssetOrAdvanceGroupboxEnabled; }
            set
            {
                _isFixedAssetOrAdvanceGroupboxEnabled = value;
                if (!value) IsFixedAsset = false;
                NotifyPropertyChanged("IsFixedAssetOrAdvanceGroupboxEnabled");
            }
        }

        private bool _isFixedAsset;
        public bool IsFixedAsset
        {
            get { return _isFixedAsset; }
            set
            {
                _isFixedAsset = value;
                if (value == false)
                {
                    FixedAssetParticulars = string.Empty;
                    FixedAssetDepreciationRate = 0;
                }
                NotifyPropertyChanged("IsFixedAsset");
            }
        }

        private bool _isAdvance;
        public bool IsAdvance
        {
            get { return _isAdvance; }
            set
            {
                _isAdvance = value;
                NotifyPropertyChanged("IsAdvance");
            }
        }

        private string _fixedAssetParticulars;
        public string FixedAssetParticulars
        {
            get { return _fixedAssetParticulars; }
            set
            {
                _fixedAssetParticulars = value;
                NotifyPropertyChanged("FixedAssetParticulars");
            }
        }

        private double _fixedAssetDepreciationRate;
        public double FixedAssetDepreciationRate
        {
            get { return _fixedAssetDepreciationRate; }
            set
            {
                _fixedAssetDepreciationRate = value;
                NotifyPropertyChanged("FixedAssetDepreciationRate");
            }
        }

        #endregion


        #region End part

        private string _narration;
        public string Narration
        {
            get { return _narration; }
            set
            {
                _narration = value;
                NotifyPropertyChanged("Narration");
            }
        }

        private string _takaInWords;
        public string TakaInWords
        {
            get { return _takaInWords; }
            set
            {
                _takaInWords = value;
                NotifyPropertyChanged("TakaInWords");
            }
        }

        private List<Record> _temporaryRecords;
        private List<Record> TemporaryRecords
        {
            get { return _temporaryRecords ?? (_temporaryRecords = new List<Record>()); }
            set
            {
                _temporaryRecords = value;
                NotifyPropertyChanged("TemporaryRecords");
            }
        }

        private bool _isCreateVoucherButtonEnabled;
        public bool IsCreateVoucherButtonEnabled
        {
            get { return _isCreateVoucherButtonEnabled; }
            set
            {
                _isCreateVoucherButtonEnabled = value;
                //SetPostButtonIsEnabled();
                NotifyPropertyChanged("IsCreateVoucherButtonEnabled");
            }
        }

        private bool _isPostButtonEnabled;
        public bool IsPostButtonEnabled
        {
            get { return _isPostButtonEnabled; }
            set
            {
                _isPostButtonEnabled = value;
                SetInputFirstPartIsEnabled();
                SetInputSecondPartIsEnabled();
                SetCreateVoucherButtonIsEnabled();
                NotifyPropertyChanged("IsPostButtonEnabled");
            }
        }

        private string _notificationMessage;
        public string NotificationMessage
        {
            get { return _notificationMessage; }
            private set
            {
                _notificationMessage = value;
                NotifyPropertyChanged("NotificationMessage");
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

        #endregion


        #region view events

        private void VoucherTypeChanged()
        {
            // The following function calls should not change order.
            SetAllHeadsIsEnabled();
            SetChequeGroupboxIsEnabled();
            SetFixedAssetOrAdvanceGroupboxIsEnabled();
            SetMultiJVCheckboxIsVisible();
            SetJVDebitCreditIsEnabled();
            SetJVStartedIsChecked();
            SetContraTypesIsEnabled();
            SetVoucherSerialNo();
            SetCreateVoucherButtonIsEnabled();
        }

        private void SetAllHeadsIsEnabled()
        {
            if (SelectedVoucherType == "Contra")
            {
                SelectedHead = null;
                IsAllHeadsEnabled = false;
            }
            else IsAllHeadsEnabled = true;
        }

        private void SetChequeGroupboxIsEnabled()
        {
            if (SelectedVoucherType == "Contra" && SelectedContraType == "Bank to cash") IsChequeGroupboxEnabled = true;
            else if (new[] { "DV", "CV" }.Contains(SelectedVoucherType)) IsChequeGroupboxEnabled = true;
            else IsChequeGroupboxEnabled = false;
        }

        private void SetPaymentInChequeIsChecked()
        {
            if (string.IsNullOrWhiteSpace(SelectedContraType) || SelectedContraType == "Cash to bank") IsPaymentInCheque = false;
        }

        private void SetFixedAssetOrAdvanceGroupboxIsEnabled()
        {
            if (!IsSelectedHeadCapital()) IsFixedAssetOrAdvanceGroupboxEnabled = false;
            else if (new[] { "CV", "Contra" }.Contains(SelectedVoucherType)) IsFixedAssetOrAdvanceGroupboxEnabled = false;
            else if (SelectedVoucherType == "JV" && SelectedJVDebitOrCredit == "Credit") IsFixedAssetOrAdvanceGroupboxEnabled = false;
            else IsFixedAssetOrAdvanceGroupboxEnabled = true;

            if (!IsFixedAssetOrAdvanceGroupboxEnabled)
            {
                IsAdvance = false;
                IsFixedAsset = false;
            }
        }

        private bool IsSelectedHeadCapital()
        {
            if (SelectedHead != null)
                return SelectedHead.HeadType == HeadType.Capital.ToString();

            return false;
        }

        private void SetMultiJVCheckboxIsVisible()
        {
            if (SelectedVoucherType == "JV") IsMultiJVCheckboxVisible = true;
            else IsMultiJVCheckboxVisible = false;
        }

        private void SetJVStartedIsChecked()
        {
            bool isJVStartedChecked = true;
            if (!IsMultiJVCheckboxVisible) isJVStartedChecked = false;
            else if (SelectedVoucherType != "JV") isJVStartedChecked = false;

            IsJVStartedChecked = isJVStartedChecked;
            // TODO: (sobuj) JV started unchecked does not work with JC Started checkbox disabled
        }

        private void SetJVDebitCreditIsEnabled()
        {
            if (SelectedVoucherType == "JV" && IsMultiJVCheckboxVisible) IsJVDebitOrCreditEnabled = true;
            else
            {
                IsJVDebitOrCreditEnabled = false;
                SelectedJVDebitOrCredit = null;
            }
        }

        private void SetContraTypesIsEnabled()
        {
            if (SelectedVoucherType == "Contra") IsContraTypesEnabled = true;
            else
            {
                IsContraTypesEnabled = false;
                SelectedContraType = null;
            }
        }

        private void SetCreateVoucherButtonIsEnabled()
        {
            bool isCreateVoucherButtonEnabled = true;
            if (SelectedVoucherType == "JV") isCreateVoucherButtonEnabled = IsJVStartedChecked;
            else isCreateVoucherButtonEnabled = !IsPostButtonEnabled;
            IsCreateVoucherButtonEnabled = isCreateVoucherButtonEnabled;
        }
        
        private void SetPostButtonIsEnabled()
        {
            bool isPostButtonEnabled = false;
            int count = TemporaryRecords == null ? 0 : TemporaryRecords.Count;
            if (count > 0)
            {
                if (SelectedVoucherType != "JV") isPostButtonEnabled = true;
                if (SelectedVoucherType == "JV" && !IsJVStartedChecked && _isJVBalanced) isPostButtonEnabled = true;
            }

            IsPostButtonEnabled = isPostButtonEnabled;
        }

        private void SetInputFirstPartIsEnabled()
        {
            InputFirstPartEnabled = !IsPostButtonEnabled;
        }

        private void SetInputSecondPartIsEnabled()
        {
            bool inputSecondPartEnabled;
            if (SelectedVoucherType == "JV") inputSecondPartEnabled = IsJVStartedChecked;
            else inputSecondPartEnabled = !IsPostButtonEnabled;
            InputSecondPartEnabled = inputSecondPartEnabled;
        }

        private bool _isJVBalanced;
        private void SetJVBalanceZeroMessage()
        {
            if (SelectedVoucherType == "JV" && !IsJVStartedChecked && TemporaryRecords.Count > 0)
            {
                if (TempRecordsGridItems.Last().Balance != 0)
                {
                    _isJVBalanced = false;
                    ShowMessage(MessageService.Instance.Get("VoucherBalanceIsNotZero", MessageType.Error));
                }
                else
                    _isJVBalanced = true;
            }
        }

        private void SetAllHeads()
        {
            if (SelectedProject != null)
                AllHeads = _headManager.GetHeads(SelectedProject, false, false).ToList();
            else
            {
                AllHeads = null;
                
            }
        }

        private void SetVoucherSerialNo()
        {
            if (SelectedProject != null)
                VoucherSerialNo = _massVoucherManager.GetNewVoucherNo(SelectedVoucherType, SelectedProject);
            else
                VoucherSerialNo = 0;
        }

        private void AmountChanged()
        {
            if (Amount > 0)
                TakaInWords = MoneyToTextUtils.NumberToTextInLacCrore(((int)Amount).ToString()) + "Only";
            else TakaInWords = "";
        }

        private void ShowMessage(Message message)
        {
            ColorCode = MessageService.Instance.GetColorCode(message.MessageType);
            NotificationMessage = message.MessageText;
        }

        private void ClearMessage()
        {
            Message message = new Message();
            ColorCode = MessageService.Instance.GetColorCode(message.MessageType);
            NotificationMessage = message.MessageText;
        }


        #endregion

        #region Command Operation Region

        private string  GetTag()
        {
            string tag = "";
            if (IsAdvance)
                tag = "Advance";

            if (tag != "")
                tag += ",";

            if (IsPaymentInCheque)
                tag += "Bank";
            else
                tag += "Cash";

            return tag;
        }

        private MassVoucher GetCurrentVoucher()
        {
            MassVoucher massVoucher = new MassVoucher
                                          {
                                              Amount = Amount,
                                              Project = SelectedProject,
                                              Head = SelectedHead,
                                              VoucherType = SelectedVoucherType,
                                              VoucherSerialNo = VoucherSerialNo,
                                              VoucherDate = VoucherDate,
                                              JVDebitOrCredit = SelectedJVDebitOrCredit,
                                              ContraType = SelectedContraType,
                                              IsCheque = IsPaymentInCheque,
                                              IsFixedAsset = IsFixedAsset,
                                              ChequeNo = ChequeNo,
                                              ChequeDate = ChequeDate,
                                              BankName = BankName,
                                              FixedAssetName = FixedAssetParticulars,
                                              FixedAssetDepreciationRate = FixedAssetDepreciationRate,
                                              Tag = GetTag(),
                                              Narration = Narration,
                                              FinancialYear = _parameterManager.GetCurrentFinancialYear()
                                          };

            return massVoucher;
        }

        private void AddTemporaryRecords(IList<Record> records)
        {
            TemporaryRecords.AddRange(records);
            NotifyPropertyChanged("TemporaryRecords");
            NotifyPropertyChanged("TempRecordsGridItems");
        }

        private void ClearTemporaryRecords()
        {
            TemporaryRecords.Clear();
            NotifyPropertyChanged("TemporaryRecords");
            NotifyPropertyChanged("TempRecordsGridItems");
        }

        public IList<ViewableGridRow> TempRecordsGridItems
        {
            get
            {
                if (TemporaryRecords == null || TemporaryRecords.Count == 0) return null;
                double balance = 0;
                return TemporaryRecords.Select(tr => new ViewableGridRow
                                                         {
                                                             Balance = (balance += (tr.Debit - tr.Credit)),
                                                             Credit = tr.Credit,
                                                             Debit = tr.Debit,
                                                             Date = tr.Date,
                                                             Head = tr.HeadName(),
                                                             VoucherNo = tr.VoucherType + "-" + tr.VoucherSerialNo
                                                         }).ToList();
            }
        }

        public void Reset(bool removeProjectAndMessage = true)
        {
            AllProjects = _projectManager.GetProjects(false);
            
            if (removeProjectAndMessage) SelectedProject = null;
            SelectedHead = null;

            ClearTemporaryRecords();

            VoucherTypeChanged();

            VoucherDate = DateTime.Now;
            Amount = 0;
            IsPaymentInCheque = false;
            SelectedJVDebitOrCredit = null;
            SelectedContraType = null;
            Narration = "";
            if (removeProjectAndMessage) ClearMessage();

            IsPostButtonEnabled = false;
            InputFirstPartEnabled = true;
            InputSecondPartEnabled = true;
            IsJVStartedChecked = false;
        }

        //public void TemporaryVoucherReset()
        //{
        //    VoucherTypeChanged();
        //    VoucherDate = DateTime.Now;
        //    SelectedContraType = null;
        //}

        private void CreateVoucher()
        {
            IMassVoucherManager massVoucherManager = BLLCoreFactory.GetMassVoucherManager();
            MassVoucher massVoucher = GetCurrentVoucher();

            if (massVoucher == null)
                return;

            bool isAdded = massVoucherManager.Set(massVoucher);
            Message latestMessage = MessageService.Instance.GetLatestMessage();

            if (isAdded)
            {
                IList<Record> records = massVoucherManager.GetEntryableRecords();
                AddTemporaryRecords(records);
                ClearMessage();
                SetPostButtonIsEnabled();
            }

            ShowMessage(latestMessage);
        }

        private void PostVoucher()
        {
            IRecordManager recordManager = BLLCoreFactory.GetRecordManager();
            recordManager.SetRecords(TemporaryRecords);
            bool isSuccess = recordManager.Save();
            Message message = MessageService.Instance.GetLatestMessage();
            ShowMessage(message);
            if (isSuccess) Reset(false);
        }

        #endregion

        private RelayCommand _clearButtonClicked;
        public ICommand ClearButtonClicked
        {
            get
            {
                return _clearButtonClicked ??
                       (_clearButtonClicked = new RelayCommand(p1 => this.Reset()));
            }
        }

        private RelayCommand _createVoucherButtonClicked;
        public ICommand CreateVoucherButtonClicked
        {
            get
            {
                return _createVoucherButtonClicked ?? (_createVoucherButtonClicked = new RelayCommand(p1 => this.CreateVoucher()));
            }
        }

        private RelayCommand _postButtonClicked;
        public ICommand PostButtonClicked
        {
            get
            {
                return _postButtonClicked ?? (_postButtonClicked = new RelayCommand(p1 => this.PostVoucher()));
            }
        }
    }

    //public class ViewableGridRows : List<ViewableGridRow> { }
}
