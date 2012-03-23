using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Controls;

namespace MVVMTest
{
    public class ViewModel: IMainWindow, ICommand
    {
        public ViewModel(string nickName)
        {
            Name = nickName;
        }
        public ViewModel()
        {
            Name = "sobuj";
            _comboSelected = ComboItems[0];
        }

        private string _name;
        public string Name { 
            get { return _name; }
            set
            {
                _name = value;
                if (PropertyChanged!=null) PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }


        public ICommand ButtonClicked { get { return this; } }

        //private string _tempText;
        //public string TempText { get; set; }

        //public string Color { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Execute(object parameter)
        {
            Name = ComboSelected.Key.ToString();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        private IList<KeyValuePair<int, string>> _comboItems; 
        public IList<KeyValuePair<int, string>> ComboItems 
        {
            get
            {
                return _comboItems = new[] {
                        new KeyValuePair<int, string>(1, "55"),
                        new KeyValuePair<int, string>(2, "88"),
                        new KeyValuePair<int, string>(3, "103")
                };
            }
            set
            {
                _comboItems = value;
            }
        }

        private KeyValuePair<int, string> _comboSelected;
        public KeyValuePair<int, string> ComboSelected { get { return _comboSelected; } set { _comboSelected = value; } }

        private IEnumerable<GridViewSingleRow> _gridViewItems;
        public IEnumerable<GridViewSingleRow> GridViewItems
        {
            get
            {
                return _gridViewItems = new[] {
                        new GridViewSingleRow{Id=1, Name="Sobuj", Value=3.49},
                        new GridViewSingleRow{Id=2, Name="Murad", Value=3.31},
                        new GridViewSingleRow{Id=3, Name="Nahin", Value= 3.51}
                };
            }
            set
            {
                _gridViewItems = value;
            }
        }
    }


    public class GridViewSingleRow
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
    }
}
