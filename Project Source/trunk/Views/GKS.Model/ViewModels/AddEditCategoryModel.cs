using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Input;
using BLL.Model;
using BLL.ProjectManagement;
using GKS.Factory;

namespace GKS.Model.ViewModels
{
    public class AddEditHeadModel : INotifyPropertyChanged
    {
        public AddEditHeadModel()
        {
            SaveButtonClicked = new SaveNewHead(this);
            CancelButtonClicked = new CancelAddHeadPage(this);
            Head = new Head();
            //CurrentHeadOption = HeadType.Revenue;
        }

        public Head Head { get; set; }

        public string HeadName { get { return Head.Name; } set { Head.Name = value; } }

        public HeadType CurrentHeadOption
        {
            get
            {
                return Head.Type; 
            }
            set
            {
                Head.Type = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("CurrentHeadOption"));
            }
        }

        public ICommand SaveButtonClicked { get; set; }
        public ICommand CancelButtonClicked { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
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
            _headManager.AddOrUpdateHead(_addEditHeadModel.Head);
            //_addEditHeadModel.CurrentHeadOption = HeadType.Capital;
        }
    }

    public class CancelAddHeadPage : ICommand
    {
        AddEditHeadModel _addEditHeadModel;
        public CancelAddHeadPage(AddEditHeadModel addEditHeadModel)
        {
            _addEditHeadModel = addEditHeadModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            //_addEditHeadModel.CurrentHeadOption = HeadType.FixedAsset;
        }
    }

}
