using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.Model.Managers;
using BLL.Factories;
using BLL.Model.Entity;
using System.Windows.Input;
using BLL.Messaging;

namespace GKS.Model.ViewModels
{
    public class DepreciationRateSetupModel : ViewModelBase
    {
        private readonly IProjectManager _projectManager;
        private readonly IHeadManager _headManager;
        private readonly IDepreciationRateManager _depreciationRateManager;

        public DepreciationRateSetupModel()
        {
            try
            {
                _projectManager = BLLCoreFactory.GetProjectManager();
                _headManager = BLLCoreFactory.GetHeadManager();
                _depreciationRateManager = BLLCoreFactory.GetDepreciationRateManager();

                NotifyDepreciationRateDataGrid();

                AllProjects = _projectManager.GetProjects();
                DepreciationRateEdit = 0;
            }
            catch
            { }
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
                NotifyPropertyChanged("AllHeads");
                SelectedHead = null;
            }
        }

        public IList<Head> AllHeads
        {
            get
            {
                if (SelectedProject == null) return new List<Head>();
                return _headManager.GetCapitalHeads(SelectedProject);
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

        private double _depreciationRateEdit;
        public double DepreciationRateEdit
        {
            get
            {
                return _depreciationRateEdit;
            }
            set
            {
                _depreciationRateEdit = value;
                NotifyPropertyChanged("DepreciationRateEdit");
            }
        }

        private IList<DepreciationRate> _depreciationRateDataGridItems;
        private IList<DepreciationRate> DepreciationRateDataGridItems
        {
            get
            {
                return _depreciationRateDataGridItems;
            }
            set
            {
                _depreciationRateDataGridItems = value;
                NotifyPropertyChanged("DepreciationRateDataGridItems");
                NotifyPropertyChanged("DepreciationRateDataGrid");
            }
        }

        private IList<DepreciationRateGridRow> _depreciationRateDataGrid;
        public IList<DepreciationRateGridRow> DepreciationRateDataGrid
        {
            get
            {
                if (DepreciationRateDataGridItems == null || DepreciationRateDataGridItems.Count == 0) return null;

                return DepreciationRateDataGridItems.Select(dr => GetDepreciationRateGridRow(dr)).ToList();
            }
        }

        private DepreciationRateGridRow GetDepreciationRateGridRow(DepreciationRate depreciationRate)
        {
            return new DepreciationRateGridRow
            {
                HeadName = depreciationRate.ProjectHead.Head.Name,
                DepreciationRate = depreciationRate.Rate,
            };
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
            private set
            {
                _colorCode = value;
                NotifyPropertyChanged("ColorCode");
            }
        }

        private RelayCommand _saveButtonClicked;
        public ICommand SaveButtonClicked
        {
            get { return _saveButtonClicked ?? (_saveButtonClicked = new RelayCommand(p1 => this.SaveDepreciationRate())); }
        }

        private RelayCommand _oKButtonClicked;
        public ICommand OKButtonClicked
        {
            get { return _oKButtonClicked ?? (_oKButtonClicked = new RelayCommand(p1 => this.InvokeOnFinish())); }
        }

        private void SaveDepreciationRate()
        {
            if (_depreciationRateManager.Set(SelectedProject, SelectedHead, DepreciationRateEdit))
                NotifyDepreciationRateDataGrid();
            ShowMessage(MessageService.Instance.GetLatestMessage());
        }

        private void ShowMessage(Message message)
        {
            MessageText = message.MessageText;
            ColorCode = MessageService.Instance.GetColorCode(message.MessageType);
        }

        private void NotifyDepreciationRateDataGrid()
        {
            DepreciationRateDataGridItems = _depreciationRateManager.GetDepreciationRates();
        }
    }

    public class DepreciationRateGridRow
    {
        public string HeadName { get; set; }
        public double DepreciationRate { get; set; }
    }
}
