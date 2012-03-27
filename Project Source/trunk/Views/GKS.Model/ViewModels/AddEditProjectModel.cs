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
    public class AddEditProjectModel : INotifyPropertyChanged
    {
        public delegate void SimpleDelegate();
        public SimpleDelegate CloseWindow { get; set; }

        public AddEditProjectModel()
        {
            CreateDate = DateTime.Now;
            IsActive = true;
            SaveButtonClicked = new SaveProject(this);
            CancelButtonClicked = new CancelProject(this);
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
            set
            {
                _colorCode = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ColorCode"));
            }
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

        //public Project Project { get; set; }

        private string _projectNameText;
        public string ProjectNameText
        {
            get
            {
                return _projectNameText;
            }
            set
            {
                _projectNameText = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ProjectNameText"));
            }
        }

        private string _descriptionText;
        public string DescriptionText
        {
            get { return _descriptionText; }
            set
            {
                _descriptionText = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("DescriptionText"));
            }
        }

        private DateTime _createDate;
        public DateTime CreateDate
        {
            get { return _createDate; }
            set
            {
                _createDate = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("CreateDate"));
            }
        }

        public ICommand SaveButtonClicked { get; set; }
        public ICommand CancelButtonClicked { get; set; }

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

        public Project GetProject()
        {
            return new Project
                       {
                           CreateDate = CreateDate,
                           Description = DescriptionText,
                           IsActive = IsActive,
                           Name = ProjectNameText
                       };
        }
    }

    public class SaveProject : ICommand
    {
        AddEditProjectModel _projectModel;
        ProjectManager _projectManger;

        public SaveProject(AddEditProjectModel projectModel)
        {
            _projectModel = projectModel;
            _projectManger = new ProjectManager(GKSFactory.GetProjectRepository(), GKSFactory.GetHeadRepository(), GKSFactory.GetRecordRepository());
        }

        public bool CanExecute(object parameter)
        {
            return true;
            //return !string.IsNullOrWhiteSpace(_projectModel.ProjectNameText);
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (!string.IsNullOrWhiteSpace(_projectModel.ProjectNameText))
            {
                Project project = _projectModel.GetProject();

                OperationType saveType = _projectModel.OperationType;

                bool isSuccess = saveType == OperationType.Add
                                     ? _projectManger.Add(project)
                                     : _projectManger.Update(project);

                if (isSuccess && _projectModel.CancelButtonClicked.CanExecute(this))
                    _projectModel.CancelButtonClicked.Execute(this);
                else
                    _projectModel.ShowMessage(_projectManger.GetLatestMessage());
            }
        }
    }

    public class CancelProject : ICommand
    {
        AddEditProjectModel _projectModel;
        public CancelProject(AddEditProjectModel projectModel)
        {
            _projectModel = projectModel;
        }

        public bool CanExecute(object parameter)
        {
            if (_projectModel.CloseWindow != null)
                return true;
            return false;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _projectModel.CloseWindow();
        }
    }

    public enum ProjectStatus
    {
        Active,
        Inactive
    }

    public enum OperationType
    {
        Add,
        Update
    }

    public class EnumConverter : IValueConverter
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
