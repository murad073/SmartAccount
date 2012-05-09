using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Input;
using BLL.Factories;
using BLL.Messaging;
using BLL.Model.Entity;
using BLL.Model.Managers;

namespace GKS.Model.ViewModels
{
    public class AddEditProjectModel : ViewModelBase
    {
        private readonly IProjectManager _projectManger;

        public AddEditProjectModel()
        {
            _projectManger = BLLCoreFactory.GetProjectManager();
            Project = Project ?? new Project { IsActive = true, CreateDate = DateTime.Now };
        }

        private Project _project;
        public Project Project
        {
            get { return _project; }
            set { _project = value; NotifyPropertyChanged("Project"); }
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
            set
            {
                _colorCode = value;
                NotifyPropertyChanged("ColorCode");
            }
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
                return _saveButtonClicked ?? (_saveButtonClicked =
                        new RelayCommand(p1 => this.SaveProject(), p2 => !string.IsNullOrWhiteSpace(Project.Name)));
            }
        }

        private RelayCommand _cancelButtonClicked;
        public ICommand CancelButtonClicked
        {
            get { return _cancelButtonClicked ?? (_cancelButtonClicked = new RelayCommand(p1 => this.InvokeOnFinish() )); }
        }

        private void SaveProject()
        {
            bool isSuccess = OperationType == OperationType.Add
                                 ? _projectManger.Add(Project)
                                 : _projectManger.Update(Project);

            if (isSuccess) InvokeOnFinish();
            else ShowMessage(MessageService.Instance.GetLatestMessage());
        }

        #endregion
    }
}

