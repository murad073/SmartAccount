using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using BLL.Messaging;
using BLL.Model.Schema;
using BLL.ProjectManagement;
using BLL.Utils;
using BLL.VoucherManagement;
using GKS.Factory;
using System.Windows.Data;
using BLL.Model.Repositories;


namespace GKS.Model.ViewModels
{
    public class VoucherPost : INotifyPropertyChanged
    {
        private readonly ProjectManager _projectManager;
        private readonly HeadManager _headManager;
        private readonly MassVoucherManager _massVoucherManager;

        public VoucherPost()
        {
            IProjectRepository projectRepository = GKSFactory.GetProjectRepository();
            IHeadRepository headRepository = GKSFactory.GetHeadRepository();
            IRecordRepository recordRepository = GKSFactory.GetRecordRepository();

            _projectManager = new ProjectManager(projectRepository, headRepository, recordRepository);
            _headManager = new HeadManager(headRepository);
            _massVoucherManager = new MassVoucherManager(recordRepository, projectRepository, headRepository);

            InputFirstPartEnabled = true;
            InputSecondPartEnabled = true;

            AllProjects = new CollectionView(_projectManager.GetProjects(false));
            SelectedVoucherType = "DV";

            VoucherDate = DateTime.Now;
            ChequeDate = DateTime.Now;

            TemporaryRecords = new List<Record>();

            TempButtonClicked = new CreateTempVoucher(this);
            PostButtonClicked = new SubmitVoucherForSave(this);
            ClearButtonClicked = new ClearVoucher(this);
        }

        private bool _isInputFirstPartEnabled;
        public bool InputFirstPartEnabled
        {
            get { return _isInputFirstPartEnabled; }
            set
            {
                _isInputFirstPartEnabled = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("InputFirstPartEnabled"));
            }
        }

        private bool _isInputSecondPartEnabled;
        public bool InputSecondPartEnabled
        {
            get { return _isInputSecondPartEnabled; }
            set
            {
                _isInputSecondPartEnabled = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("InputSecondPartEnabled"));
            }
        }

        #region Project Head section

        private CollectionView _allProjects;
        public CollectionView AllProjects
        {
            get { return _allProjects; }
            set
            {
                _allProjects = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("AllProjects"));
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
                if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("SelectedProject")); }
            }
        }

        private IList<Head> _allHeads;
        public IList<Head> AllHeads
        {
            get { return _allHeads; }
            set
            {
                _allHeads = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("AllHeads"));
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
            get { return _selectedHead; }
            set
            {
                _selectedHead = value;
                SetFixedAssetOrAdvanceGroupboxIsEnabled();
                if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("SelectedHead")); }
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
                VoucherTypeChanged(); // All small event moved to inside the function
                if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("SelectedVoucherType")); }
            }
        }

        private int _voucherSerialNo;
        public int VoucherSerialNo
        {
            get { return _voucherSerialNo; }
            set
            {
                _voucherSerialNo = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("VoucherSerialNo"));
                }
            }
        }

        private DateTime _voucherDate;
        public DateTime VoucherDate
        {
            get { return _voucherDate; }
            set
            {
                _voucherDate = value;
                if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("VoucherDate")); }
            }
        }

        private bool _isJVStartedChecked;
        public bool IsJVStartedChecked
        {
            get { return _isJVStartedChecked; }
            set
            {
                _isJVStartedChecked = value;
                SetPostButtonIsEnabled();
                SetInputSecondPartIsEnabled();
                SetJVBalanceZeroMessage();
                //SetTemporaryButtonIsEnabled();
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("IsJVStartedChecked"));
                }
            }
        }

        private bool _isMultiJVCheckboxVisible;
        public bool IsMultiJVCheckboxVisible
        {
            get { return _isMultiJVCheckboxVisible; }
            set
            {
                _isMultiJVCheckboxVisible = value;
                SetJVDebitCreditIsEnabled();
                SetTemporaryButtonIsEnabled();
                SetJVStartedIsChecked();
                if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("IsMultiJVCheckboxVisible")); }
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
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Amount"));
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
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("SelectedJVDebitOrCredit"));
            }
        }

        private bool _isJVDebitOrCreditEnabled;
        public bool IsJVDebitOrCreditEnabled
        {
            get { return _isJVDebitOrCreditEnabled; }
            set
            {
                _isJVDebitOrCreditEnabled = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("IsJVDebitOrCreditEnabled"));
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
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("SelectedContraType"));
            }
        }

        private bool _isContraTypesEnabled;
        public bool IsContraTypesEnabled
        {
            get { return _isContraTypesEnabled; }
            set
            {
                _isContraTypesEnabled = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("IsContraTypesEnabled"));
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
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("IsChequeGroupboxEnabled"));
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

                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("IsPaymentInCheque"));
            }
        }

        private string _chequeNo;
        public string ChequeNo
        {
            get { return _chequeNo; }
            set
            {
                _chequeNo = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ChequeNo"));
            }
        }

        private DateTime _chequeDate;
        public DateTime ChequeDate
        {
            get { return _chequeDate; }
            set
            {
                _chequeDate = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ChequeDate"));
            }
        }

        private string _bankName;
        public string BankName
        {
            get { return _bankName; }
            set
            {
                _bankName = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("BankName"));
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
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("IsFixedAssetOrAdvanceGroupboxEnabled"));
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

                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("IsFixedAsset"));
            }
        }

        private bool _isAdvance;
        public bool IsAdvance
        {
            get { return _isAdvance; }
            set
            {
                _isAdvance = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("IsAdvance"));
            }
        }

        private string _fixedAssetParticulars;
        public string FixedAssetParticulars
        {
            get { return _fixedAssetParticulars; }
            set
            {
                _fixedAssetParticulars = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("FixedAssetParticulars"));
            }
        }

        private double _fixedAssetDepreciationRate;
        public double FixedAssetDepreciationRate
        {
            get { return _fixedAssetDepreciationRate; }
            set
            {
                _fixedAssetDepreciationRate = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("FixedAssetDepreciationRate"));
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
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Narration"));
            }
        }

        private string _takaInWords;
        public string TakaInWords
        {
            get { return _takaInWords; }
            set
            {
                _takaInWords = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("TakaInWords"));
            }
        }

        private List<Record> _temporaryRecords;
        public List<Record> TemporaryRecords
        {
            get { return _temporaryRecords; }
            set
            {
                _temporaryRecords = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("TemporaryRecords"));
            }
        }

        private bool _isTemporaryButtonEnabled;
        public bool IsTemporaryButtonEnabled
        {
            get { return _isTemporaryButtonEnabled; }
            set
            {
                _isTemporaryButtonEnabled = value;
                //SetPostButtonIsEnabled();
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("IsTemporaryButtonEnabled"));
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
                SetTemporaryButtonIsEnabled();
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("IsPostButtonEnabled"));
            }
        }

        private string _notificationMessage;
        public string NotificationMessage
        {
            get { return _notificationMessage; }
            private set
            {
                _notificationMessage = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("NotificationMessage"));
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

        #endregion


        #region view events

        private void VoucherTypeChanged()
        {
            SetAllHeadsIsEnabled();
            SetChequeGroupboxIsEnabled();
            SetFixedAssetOrAdvanceGroupboxIsEnabled();
            SetMultiJVCheckboxIsVisible();
            SetJVDebitCreditIsEnabled();
            SetJVStartedIsChecked();
            SetContraTypesIsEnabled();
            SetVoucherSerialNo();
            SetTemporaryButtonIsEnabled();
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
                //return _headManager.IsCapitalHead(SelectedHead.Id);
                return SelectedHead.Type == HeadType.Capital;
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
            // TODO: JV started unchecked does not work with JC Started checkbox disabled
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
                SelectedContraType = null; // TODO: The selected contra type should be cleared here.
            }
        }

        private void SetTemporaryButtonIsEnabled()
        {
            //bool isTemporaryButtionEnabled = true;
            //int count = TemporaryRecords == null ? 0 : TemporaryRecords.Count;
            //if (count > 0)
            //{
            //    if (SelectedVoucherType != "JV") isTemporaryButtionEnabled = false;
            //    if (SelectedVoucherType == "JV" && !IsJVStartedChecked) isTemporaryButtionEnabled = false;
            //}

            //IsTemporaryButtonEnabled = isTemporaryButtionEnabled;

            bool isTemporaryButtonEnabled;
            if (SelectedVoucherType == "JV") isTemporaryButtonEnabled = IsJVStartedChecked;
            else isTemporaryButtonEnabled = !IsPostButtonEnabled;
            IsTemporaryButtonEnabled = isTemporaryButtonEnabled;
        }

        public void SetPostButtonIsEnabled()
        {
            // -- IsPostButtonEnabled = !IsTemporaryButtonEnabled;
            bool isPostButtionEnabled = false;
            int count = TemporaryRecords == null ? 0 : TemporaryRecords.Count;
            if (count > 0)
            {
                if (SelectedVoucherType != "JV") isPostButtionEnabled = true;
                if (SelectedVoucherType == "JV" && !IsJVStartedChecked) isPostButtionEnabled = true;
            }

            IsPostButtonEnabled = isPostButtionEnabled;
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

        private void SetJVBalanceZeroMessage()
        {
            //todo: need to move the logic in BLL
            if (SelectedVoucherType == "JV" && !IsJVStartedChecked && TemporaryRecords.Count > 0)
            {
                if (TempGridItems.Last().Balance != 0)
                {
                    ShowMessage(MessageService.Instance.Get(ErrorMessage.VoucherBalanceIsNotZero.ToString(),
                                                            MessageType.Error));
                }
            }
        }

        private bool ValidateAll()
        {
            return true;
        }

        private void SetAllHeads()
        {
            if (SelectedProject != null)
                AllHeads = _headManager.GetHeads(SelectedProject.Id, false, false).ToList();
            else
            {
                AllHeads = null;
                SelectedHead = null;
            }
        }

        private void SetVoucherSerialNo()
        {
            if (SelectedProject != null)
                VoucherSerialNo = _massVoucherManager.GetNewVoucherNo(SelectedVoucherType, SelectedProject.Name);
            else
                VoucherSerialNo = 0;
            // TODO: new change -> make the function work perfectly
        }

        private void AmountChanged()
        {
            if (Amount > 0)
                TakaInWords = Utilities.NumberToTextInLacCrore(((int)Amount).ToString()) + " Only";
            else TakaInWords = "";
        }

        public void ShowMessage(Message message)
        {
            ColorCode = MessageService.Instance.GetColorCode(message.MessageType);
            NotificationMessage = message.MessageText;
        }

        public void ClearMessage()
        {
            Message message = new Message();
            ColorCode = MessageService.Instance.GetColorCode(message.MessageType);
            NotificationMessage = message.MessageText;
        }


        #endregion


        #region Command Operation Region

        public MassVoucher GetCurrentVoucher() // TODO: new change -> function return type should be MassVoucher ...
        {
            MassVoucher massVoucher = new MassVoucher
                                          {
                                              Amount = Amount,
                                              ProjectName = SelectedProject == null ? "" : SelectedProject.Name,
                                              HeadName = SelectedHead == null ? "" : SelectedHead.Name,
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
                                              Tag = IsAdvance ? "Advance" : "",
                                              Narration = Narration
                                          };

            return massVoucher;
        }

        public void AddTemporaryRecords(IList<Record> records)
        {
            TemporaryRecords.AddRange(records);
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("TemporaryRecords"));
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("TempGridItems"));
        }

        public void ClearTemporaryRecords()
        {
            TemporaryRecords.Clear();
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("TemporaryRecords"));
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("TempGridItems"));
        }

        public IList<ViewableGridRow> TempGridItems
        {
            get
            {
                if (TemporaryRecords == null || TemporaryRecords.Count == 0) return null;
                double balance = 0;
                return TemporaryRecords.Select(tv => new ViewableGridRow
                                                         {
                                                             Balance = (balance += tv.Balance),
                                                             Credit = tv.Credit,
                                                             Debit = tv.Debit,
                                                             Date = tv.Date,
                                                             Head = tv.HeadName,
                                                             VoucherNo = tv.VoucherNo
                                                         }).ToList();

            }
        }

        //private static bool IsDebitVoucher(MassVoucher v)
        //{
        //    // TODO: make the function work well
        //    // Why do we need this? -JS
        //    return false;
        //    //string type = v.VoucherType;
        //    //return (type == "DV") || (type == "JV" && v.JournalDebitOrCredit == "Debit") ||
        //    //       (type == "Contra" && v.ContraType == "Cash to bank"); //TODO: is the debit check is okay, sobuj will verify that.
        //}

        public void Reset(bool removeMessage = true)
        {
            AllProjects = new CollectionView(_projectManager.GetProjects(false));
            SelectedProject = null;

            ClearTemporaryRecords();

            VoucherTypeChanged();

            VoucherDate = DateTime.Now;
            Amount = 0;
            IsPaymentInCheque = false;
            SelectedJVDebitOrCredit = null;
            SelectedContraType = null;
            Narration = "";
            if (removeMessage) ClearMessage();

            IsPostButtonEnabled = false;
            InputFirstPartEnabled = true;
            InputSecondPartEnabled = true;
            IsJVStartedChecked = false;
        }

        public void TemporaryVoucherReset()
        {
            VoucherTypeChanged();
            VoucherDate = DateTime.Now;
            SelectedContraType = null;
        }

        #endregion

        public ICommand TempButtonClicked { get; set; }
        public ICommand PostButtonClicked { get; set; }
        public ICommand ClearButtonClicked { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

    }

    public class ViewableGridRow
    {
        public string Head { get; set; }
        public DateTime Date { get; set; }
        public string VoucherNo { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
        public double Balance { get; set; }
    }

    public class CreateTempVoucher : ICommand
    {
        private readonly VoucherPost _voucherPost;
        public CreateTempVoucher(VoucherPost voucherPost)
        {
            _voucherPost = voucherPost;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            MassVoucherManager massVoucherManager = new MassVoucherManager(GKSFactory.GetRecordRepository(), GKSFactory.GetProjectRepository(), GKSFactory.GetHeadRepository());
            MassVoucher massVoucher = _voucherPost.GetCurrentVoucher(); // TODO: This line will give null exception, if temp button clicked without proper entry.

            if (massVoucher == null)
                return;

            bool isAdded = massVoucherManager.Set(massVoucher); // TODO: Write validation code in Set.
            Message latestMessage = massVoucherManager.GetMessage();

            if (isAdded)
            {
                IList<Record> records = massVoucherManager.GetEntryableRecords(); // TODO: This will give exception after calling GetJournalVoucher, if the Journal voucher type (Decit/Credit) is null.
                _voucherPost.AddTemporaryRecords(records);
                _voucherPost.ClearMessage();
                _voucherPost.SetPostButtonIsEnabled();
            }

            _voucherPost.ShowMessage(latestMessage);
        }
    }

    public class SubmitVoucherForSave : ICommand
    {
        VoucherPost _voucherPost;
        public SubmitVoucherForSave(VoucherPost voucherPost)
        {
            _voucherPost = voucherPost;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            RecordManager recordManager = new RecordManager(GKSFactory.GetRecordRepository(),
                                                            _voucherPost.TemporaryRecords);
            bool isSuccess = recordManager.Save();
            Message message = recordManager.GetLatestMessage();
            _voucherPost.ShowMessage(message);
            if(isSuccess) _voucherPost.Reset(false);
        }
    }

    public class ClearVoucher : ICommand
    {
        private readonly VoucherPost _voucherPost;
        public ClearVoucher(VoucherPost voucherPost)
        {
            _voucherPost = voucherPost;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _voucherPost.Reset();
        }
    }

}
