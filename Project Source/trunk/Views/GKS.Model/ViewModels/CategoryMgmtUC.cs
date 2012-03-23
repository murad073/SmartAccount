using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using BLL.Model;
using BLL.ProjectManagement;
using GKS.Factory;

namespace GKS.Model.ViewModels
{
    public class HeadMgmtUC : INotifyPropertyChanged
    {
        HeadManager _headManager;
        public HeadMgmtUC()
        {
            _headManager = new HeadManager(GKSFactory.GetHeadRepository());
            
            AddHeadButtonClicked = new AddnewHead(this);
            ViewHeadButtonClicked = new ViewHead(this);
            OKButtonClicked = new HeadOK(this);
            CancelButtonClicked = new HeadCancel(this);
        }

        public ICommand AddHeadButtonClicked { get; set; }
        public ICommand ViewHeadButtonClicked { get; set; }
        public ICommand OKButtonClicked { get; set; }
        public ICommand CancelButtonClicked { get; set; }

        public IEnumerable<Head> Heads
        {
            get
            {
                return _headManager.GetHeads();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    class AddnewHead : ICommand
    {
        private HeadMgmtUC _headMgmt;
        public AddnewHead(HeadMgmtUC headMgmt)
        {
            _headMgmt = headMgmt;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            //NavigationService navService = NavigationService.Equals()
            //navService.Navigate(new System.Uri("AddEditHeadPage.xaml", UriKind.RelativeOrAbsolute));
        }
    }

    class ViewHead : ICommand
    {
        private HeadMgmtUC _headMgmt;
        public ViewHead(HeadMgmtUC headMgmt)
        {
            _headMgmt = headMgmt;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
        }
    }

    class HeadOK : ICommand
    {
        private HeadMgmtUC _headMgmt;
        public HeadOK(HeadMgmtUC headMgmt)
        {
            _headMgmt = headMgmt;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
        }
    }

    class HeadCancel : ICommand
    {
        private HeadMgmtUC _headMgmt;
        public HeadCancel(HeadMgmtUC headMgmt)
        {
            _headMgmt = headMgmt;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
        }
    }
}
