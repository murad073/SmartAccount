using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Input;
using BLL.Factories;
using BLL.Messaging;
using BLL.Model.Entity;
using BLL.Model.Managers;
using BLL.Model.Schema;

namespace GKS.Model.ViewModels
{
    public class AddEditHeadModel : ViewModelBase
    {
        private readonly IHeadManager _headManager;
        public AddEditHeadModel()
        {
            Head = Head ?? new Head { IsActive = true };
            _headManager = BLLCoreFactory.GetHeadManager();
        }

        private Head _head;
        public Head Head
        {
            get { return _head; }
            set { _head = value; NotifyPropertyChanged("Head"); }
        }

        private string _messageText;
        public string MessageText
        {
            get { return _messageText; }
            set
            {
                _messageText = value;
                NotifyPropertyChanged("MessageText");
            }
        }

        private string _colorCode;
        public string ColorCode
        {
            get { return _colorCode; }
            set { _colorCode = value; NotifyPropertyChanged("ColorCode"); }
        }

        private bool _isActiveEnabled;
        public bool IsActiveEnabled
        {
            get { return _isActiveEnabled; }
            set
            {
                _isActiveEnabled = value;
                NotifyPropertyChanged("IsActiveEnabled");
            }
        }

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

        #region Relay Commands

        private RelayCommand _saveButtonClicked;
        public ICommand SaveButtonClicked
        {
            get
            {
                return _saveButtonClicked ?? (_saveButtonClicked = new RelayCommand(p1 => this.SaveHead(),
                                                               p2 => !string.IsNullOrWhiteSpace(Head.Name)));
            }
        }

        private RelayCommand _closeButtonClicked;
        public ICommand CloseButtonClicked
        {
            get { return _closeButtonClicked ?? (_closeButtonClicked = new RelayCommand(p1 => this.InvokeOnFinish())); }
        }

        private void SaveHead()
        {
            bool isSuccess = OperationType == OperationType.Add
                                 ? _headManager.Add(Head)
                                 : _headManager.Update(Head);

            if (isSuccess) InvokeOnFinish();
            else ShowMessage(MessageService.Instance.GetLatestMessage());
        }

        #endregion
    }

    [ValueConversion(typeof(bool), typeof(string))]
    public class RadioIsCheckedToContentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && parameter != null)
                if (string.Equals(value.ToString(), parameter.ToString(), StringComparison.OrdinalIgnoreCase))
                    return true;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is bool)
                if((bool)value)
                    return parameter;
            return null;
        }
    }
}
