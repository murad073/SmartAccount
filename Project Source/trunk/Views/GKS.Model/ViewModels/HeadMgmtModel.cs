using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using BLL.Model;
using BLL.Model.Schema;
using BLL.ProjectManagement;
using GKS.Factory;

namespace GKS.Model.ViewModels
{
    public class HeadMgmtModel : INotifyPropertyChanged
    {
        HeadManager _headManager;
        public HeadMgmtModel()
        {
            _headManager = new HeadManager(GKSFactory.GetHeadRepository());
            
            AddHeadButtonClicked = new AddnewHead(this);
            ViewHeadButtonClicked = new ViewHead(this);
        }

        public ICommand AddHeadButtonClicked { get; set; }
        public ICommand ViewHeadButtonClicked { get; set; }

        public IList<Head> Heads
        {
            get { return _headManager.GetHeads(false); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Reset()
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Heads"));
        }

        private Head _selectedGridItem;
        public Head SelectedGridItem
        {
            get { return _selectedGridItem; }
            set
            {
                _selectedGridItem = value;
            }
        }
    }

    class AddnewHead : ICommand
    {
        private HeadMgmtModel _headMgmt;
        public AddnewHead(HeadMgmtModel headMgmt)
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

    class ViewHead : ICommand
    {
        private HeadMgmtModel _headMgmt;
        public ViewHead(HeadMgmtModel headMgmt)
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
