using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using BLL.LedgerManagement;
using BLL.Messaging;
using BLL.Model.Schema;
using BLL.ProjectManagement;
using GKS.Factory;
using BLL.Model.Repositories;

namespace GKS.Model.ViewModels
{
    //public class SelectionItem<T> : INotifyPropertyChanged
    //{
    //    #region Fields

    //    private bool _isSelected;

    //    private T _item;

    //    #endregion

    //    #region Properties

    //    public bool IsSelected
    //    {
    //        get { return _isSelected; }
    //        set
    //        {
    //            if (value == _isSelected) return;
    //            _isSelected = value;
    //            OnPropertyChanged("IsSelected");
    //            OnSelectionChanged();
    //            //OnPropertyChanged("AllHeads");
    //        }
    //    }

    //    public T Item
    //    {
    //        get { return _item; }
    //        set
    //        {
    //            if (value.Equals(_item)) return;
    //            _item = value;
    //            OnPropertyChanged("Item");
    //        }
    //    }

    //    #endregion

    //    #region Events

    //    public event PropertyChangedEventHandler PropertyChanged;

    //    public event EventHandler SelectionChanged;

    //    #endregion

    //    #region constructor

    //    public SelectionItem(T item)
    //        : this(false, item)
    //    {
    //    }

    //    public SelectionItem(bool selected, T item)
    //    {
    //        this._isSelected = selected;
    //        this._item = item;
    //    }

    //    #endregion

    //    #region Event invokers

    //    private void OnPropertyChanged(string propertyName)
    //    {
    //        PropertyChangedEventHandler changed = PropertyChanged;
    //        if (changed != null) changed(this, new PropertyChangedEventArgs(propertyName));
    //    }

    //    private void OnSelectionChanged()
    //    {
    //        EventHandler changed = SelectionChanged;
    //        if (changed != null) changed(this, EventArgs.Empty);
    //    }

    //    #endregion
    //}

    //public class SelectionList<T> :
    //ObservableCollection<SelectionItem<T>> where T : IComparable<T>
    //{
    //    #region Properties

    //    /// <summary>
    //    /// Returns the selected items in the list
    //    /// </summary>
    //    private IList<T> _selectedItems;
    //    public IList<T> SelectedItems
    //    {
    //        get
    //        {
    //            _selectedItems = this.Where(x => x.IsSelected).Select(x => x.Item);
    //            return _selectedItems;
    //        }
    //    }

    //    /// <summary>
    //    /// Returns all the items in the SelectionList
    //    /// </summary>
    //    public IList<T> AllItems
    //    {
    //        get { return this.Select(x => x.Item); }
    //    }

    //    #endregion

    //    #region ctor

    //    public SelectionList(IList<T> col)
    //        : base(toSelectionItemEnumerable(col))
    //    {

    //    }

    //    #endregion

    //    #region Public methods

    //    /// <summary>
    //    /// Adds the item to the list
    //    /// </summary>
    //    /// <param name="item"></param>
    //    public void Add(T item)
    //    {
    //        int i = 0;
    //        foreach (T existingItem in AllItems)
    //        {
    //            if (item.CompareTo(existingItem) < 0) break;
    //            i++;
    //        }
    //        Insert(i, new SelectionItem<T>(item));
    //    }

    //    /// <summary>
    //    /// Checks if the item exists in the list
    //    /// </summary>
    //    /// <param name="item"></param>
    //    /// <returns></returns>
    //    public bool Contains(T item)
    //    {
    //        return AllItems.Contains(item);
    //    }

    //    /// <summary>
    //    /// Selects all the items in the list
    //    /// </summary>
    //    public void SelectAll()
    //    {
    //        foreach (SelectionItem<T> selectionItem in this)
    //        {
    //            selectionItem.IsSelected = true;
    //        }
    //    }

    //    /// <summary>
    //    /// Unselects all the items in the list
    //    /// </summary>
    //    public void UnselectAll()
    //    {
    //        foreach (SelectionItem<T> selectionItem in this)
    //        {
    //            selectionItem.IsSelected = false;
    //        }
    //    }

    //    #endregion

    //    #region Helper methods

    //    /// <summary>
    //    /// Creates an SelectionList from any IEnumerable
    //    /// </summary>
    //    /// <param name="items"></param>
    //    /// <returns></returns>
    //    private static IList<SelectionItem<T>> toSelectionItemEnumerable(IList<T> items)
    //    {
    //        List<SelectionItem<T>> list = new List<SelectionItem<T>>();
    //        foreach (T item in items)
    //        {
    //            SelectionItem<T> selectionItem = new SelectionItem<T>(item);
    //            list.Add(selectionItem);
    //        }
    //        return list;
    //    }

    //    #endregion
    //}

    //public class RelayCommand : ICommand
    //{
    //    #region Fields

    //    readonly Action<object> _execute;
    //    readonly Predicate<object> _canExecute;

    //    #endregion // Fields

    //    #region Constructors

    //    public RelayCommand(Action<object> execute)
    //        : this(execute, null)
    //    {
    //    }

    //    public RelayCommand(Action<object> execute, Predicate<object> canExecute)
    //    {
    //        if (execute == null)
    //            throw new ArgumentNullException("execute");

    //        _execute = execute;
    //        _canExecute = canExecute;
    //    }
    //    #endregion // Constructors

    //    #region ICommand Members

    //    //[DebuggerStepThrough]
    //    public bool CanExecute(object parameter)
    //    {
    //        return _canExecute == null ? true : _canExecute(parameter);
    //    }

    //    public event EventHandler CanExecuteChanged
    //    {
    //        add { CommandManager.RequerySuggested += value; }
    //        remove { CommandManager.RequerySuggested -= value; }
    //    }

    //    public void Execute(object parameter)
    //    {
    //        _execute(parameter);
    //    }

    //    #endregion // ICommand Members
    //}

    public class GKSViewLedgerModel : INotifyPropertyChanged
    {
        //#region Fields

        //private ICommand _selectAllCommand;

        //private ICommand _unselectAllCommand;

        //private ICommand _addCommand;

        //private string _newProject;

        //#endregion

        //#region Properties

        //public SelectionList<string> Projects { get; set; }

        //public string NewProject
        //{
        //    get { return _newProject; }
        //    set
        //    {
        //        if (value == _newProject) return;
        //        _newProject = value;
        //        OnPropertyChanged("NewProject");
        //    }
        //}

        //#endregion

        //#region Commands

        //public ICommand SelectAllCommand
        //{
        //    get
        //    {
        //        if (_selectAllCommand == null)
        //        {
        //            _selectAllCommand = new RelayCommand(param => Projects.SelectAll());
        //        }
        //        return _selectAllCommand;
        //    }
        //}

        //public ICommand UnselectAllCommand
        //{
        //    get
        //    {
        //        if (_unselectAllCommand == null)
        //        {
        //            _unselectAllCommand = new RelayCommand(param => Projects.UnselectAll());
        //        }
        //        return _unselectAllCommand;
        //    }
        //}

        //public ICommand AddCommand
        //{
        //    get
        //    {
        //        if (_addCommand == null)
        //        {
        //            _addCommand = new RelayCommand(param =>
        //            {
        //                Projects.Add(NewProject);
        //                NewProject = string.Empty;
        //            },
        //            param2=>
        //                {
        //                    return true;
        //                });
        //        }
        //        return _addCommand;
        //    }
        //}

        //#endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        private readonly ProjectManager _projectManager;
        private readonly HeadManager _headManager;
        private readonly LedgerManager _ledgerManager;
        public GKSViewLedgerModel()
        {
            IProjectRepository projectRepository = GKSFactory.GetProjectRepository();
            IHeadRepository headRepository = GKSFactory.GetHeadRepository();
            IRecordRepository recordRepository = GKSFactory.GetRecordRepository();
            ILedgerRepository ledgerRepository = GKSFactory.GetLedgerRepository();
            IParameterRepository parameterRepository = GKSFactory.GetParameterRepository();

            _projectManager = new ProjectManager(projectRepository, headRepository, recordRepository);
            _headManager = new HeadManager(headRepository);
            _ledgerManager = new LedgerManager(ledgerRepository, parameterRepository);

            AllProjects = _projectManager.GetProjects();

            //IList<Project> projects = _projectManager.GetProjects();
            //Projects = new SelectionList<string>(projects.Select(p => p.Name)); -- For multi selection list.

            LedgerEndDate = DateTime.Now;
            LedgerViewButtonClicked = new ViewLedger(this, ledgerRepository);

            //ClearMessage();
            //SelectedProject = _projectManager.GetDefaultProject();
        }

        #region Event invokers

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
                return _headManager.GetHeads(SelectedProject.Id).ToList(); // TODO: We need a function like this: GetHeads(ProjectNames[]), which will return the common head.
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

        private DateTime _ledgerEndDate;
        public DateTime LedgerEndDate
        {
            get { return _ledgerEndDate; }
            set
            {
                _ledgerEndDate = value;
                if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs("FinacialYearEndDate")); }
            }
        }

        public IList<Ledger> LedgerGridViewItems
        {
            get
            {
                if (!_ledgerManager.Validate(SelectedProject, SelectedHead))
                {
                    Message latestMessage = _ledgerManager.GetLatestMessage();
                    ErrorMessage = latestMessage.MessageText;
                    ColorCode = MessageService.Instance.GetColorCode(latestMessage.MessageType);
                    return null;
                }

                ClearMessage();
                _ledgerManager.LedgerEndDate = LedgerEndDate;
                double balance = 0;
                return _ledgerManager.GetLedgerBook(SelectedProject.Id, SelectedHead.Id).Select(l => 
                    new Ledger
                        {
                            Balance = balance += l.Debit - l.Credit,
                            ChequeNo = l.ChequeNo,
                            Credit = l.Credit,
                            Date = l.Date,
                            Debit = l.Debit,
                            Particular = l.Particular,
                            Remarks = l.Remarks,
                            VoucherNo = l.VoucherNo
                        }).ToList();
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
    }

    public class ViewLedger : ICommand
    {
        private GKSViewLedgerModel _viewLedgerModel;
        private ILedgerRepository _ledgerRepository;
        public ViewLedger(GKSViewLedgerModel viewLedgerModel, ILedgerRepository ledgerRepository)
        {
            _viewLedgerModel = viewLedgerModel;
            _ledgerRepository = ledgerRepository;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _viewLedgerModel.NotifyLedgerGrid();
        }
    }
}
