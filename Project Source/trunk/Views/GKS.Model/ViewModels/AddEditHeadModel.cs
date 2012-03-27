using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Input;
using BLL.Messaging;
using BLL.Model.Schema;
using BLL.ProjectManagement;
using GKS.Factory;

namespace GKS.Model.ViewModels
{
    public class AddEditHeadModel : INotifyPropertyChanged
    {
        public delegate void SimpleDelegate();
        public SimpleDelegate CloseWindow { get; set; }

        public AddEditHeadModel()
        {
            SaveButtonClicked = new SaveNewHead(this);
            CloseButtonClicked = new CloseAddHeadWindow(this);
            //Head = new Head { IsActive = true };
            IsActive = true;
        }

        private string _messageText;
        public string MessageText
        {
            get { return _messageText; }
            set
            {
                _messageText = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("MessageText"));
            }
        }

        private string _colorCode;
        public string ColorCode
        {
            get { return _colorCode; }
            set { _colorCode = value; if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ColorCode")); }
        }

        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                _isActive = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("IsActive"));
            }
        }

        public bool IsInactive
        {
            get { return !IsActive; }
        }

        private bool _isActiveEnabled;
        public bool IsActiveEnabled
        {
            get { return _isActiveEnabled; }
            set
            {
                _isActiveEnabled = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("IsActiveEnabled"));
            }
        }

        //public Head Head { get; set; }

        private string _headName;
        public string HeadName
        {
            get { return _headName; }
            set
            {
                _headName = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("HeadName"));
            }
        }

        private string _headDescription;
        public string HeadDescription
        {
            get { return _headDescription; }
            set
            {
                _headDescription = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("HeadDescription"));
            }
        }

        private HeadType _currentHeadOption;
        public HeadType CurrentHeadOption
        {
            get
            {
                return _currentHeadOption;
            }
            set
            {
                _currentHeadOption = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("CurrentHeadOption"));
            }
        }

        public ICommand SaveButtonClicked { get; set; }
        public ICommand CloseButtonClicked { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private OperationType _operationType;
        public OperationType OperationType
        {
            get { return _operationType; }
            set
            {
                _operationType = value;
                IsActiveEnabled = value == OperationType.Update;
            }
        }

        public void ShowMessage(Message message)
        {
            MessageText = message.MessageText;
            ColorCode = MessageService.Instance.GetColorCode(message.MessageType);
        }

        public Head GetHead()
        {
            return new Head
            {
                Description = HeadDescription,
                IsActive = IsActive,
                Name = HeadName,
                Type = CurrentHeadOption
            };
        }
    }



    public class SaveNewHead : ICommand
    {
        AddEditHeadModel _addEditHeadModel;
        HeadManager _headManager;
        public SaveNewHead(AddEditHeadModel addEditHeadModel)
        {
            _addEditHeadModel = addEditHeadModel;
            _headManager = new HeadManager(GKSFactory.GetHeadRepository());
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (!string.IsNullOrWhiteSpace(_addEditHeadModel.HeadName))
            {
                Head head = _addEditHeadModel.GetHead();

                OperationType saveType = _addEditHeadModel.OperationType;

                bool isSuccess = saveType == OperationType.Add
                                     ? _headManager.Add(head)
                                     : _headManager.Update(head);

                if (isSuccess && _addEditHeadModel.CloseButtonClicked.CanExecute(this))
                    _addEditHeadModel.CloseButtonClicked.Execute(this);
                else
                    _addEditHeadModel.ShowMessage(_headManager.GetLatestMessage());
            }
        }
    }

    public class CloseAddHeadWindow : ICommand
    {
        AddEditHeadModel _addEditHeadModel;
        public CloseAddHeadWindow(AddEditHeadModel addEditHeadModel)
        {
            _addEditHeadModel = addEditHeadModel;
        }

        public bool CanExecute(object parameter)
        {
            if (_addEditHeadModel.CloseWindow != null)
                return true;
            return false;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _addEditHeadModel.CloseWindow();
        }
    }

    public class EnumMatchToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
                              object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return false;

            string checkValue = value.ToString();
            string targetValue = parameter.ToString();
            return checkValue.Equals(targetValue,
                     StringComparison.InvariantCultureIgnoreCase);
        }

        public object ConvertBack(object value, Type targetType,
                                  object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return null;

            bool useValue = (bool)value;
            string targetValue = parameter.ToString();
            if (useValue)
                return Enum.Parse(targetType, targetValue);

            return null;
        }
    }
}
