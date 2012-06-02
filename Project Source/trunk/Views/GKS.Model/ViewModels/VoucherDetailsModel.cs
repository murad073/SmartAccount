using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using BLL.Model.Managers;
using System.Collections.Generic;
using BLL.Model.Entity;
using BLL.Factories;
using BLL.Model.Repositories;
using System.Windows.Controls;
using System.Printing;
using System.Windows.Media;
using System.Windows;
using BLL.Utils;

namespace GKS.Model.ViewModels
{
    public class VoucherDetailsModel : ViewModelBase
    {
        private readonly IVoucherManager _voucherManager;
        public VoucherDetailsModel()
        {
            _voucherManager = BLLCoreFactory.GetVoucherManager();
        }

        private VoucherItem _voucherItem;
        public VoucherItem VoucherItem
        {
            get { return _voucherItem; }
            set
            {
                _voucherItem = value;
                TakaInWords = MoneyToTextUtils.NumberToTextInLacCrore(((int)_voucherItem.Amount).ToString()) + "Only";
                NotifyPropertyChanged("VoucherItem");
            }
        }     
        
        private double _amount;
        public double Amount
        {
            get
            {
                return _amount;
            }
            set
            {
                _amount = value;
                TakaInWords = MoneyToTextUtils.NumberToTextInLacCrore(((double)_amount).ToString()) + "Only";
                NotifyPropertyChanged("Amount");
            }
        }


        // TODO: Think through the narration for multiple entry-DV/CV.
        //private string _voucherNarration;
        //public string VoucherNarration
        //{
        //    get { return _voucherNarration; }
        //    set
        //    {
        //        _voucherNarration = value;
        //        NotifyPropertyChanged("VoucherNarration");
        //    }
        //}

        private string _takaInWords;
        public string TakaInWords
        {
            get { return _takaInWords; }
            set
            {
                _takaInWords = value;
                NotifyPropertyChanged("TakaInWords");
            }
        }

        private IList<ViewableGridRow> _recordItems;
        public IList<ViewableGridRow> RecordItems
        {
            get { return _recordItems; }
            set
            {
                _recordItems = value;
                NotifyPropertyChanged("RecordItems");
            }
        }

        public void SetRecordItems()
        {
            double balance = 0;
            double amount = 0;
            RecordItems = _voucherManager.GetVouchers(VoucherItem.VoucherNo, ref amount).Select(r =>
                    new ViewableGridRow
                        {
                            Date = r.Date,
                            Debit = r.Debit,
                            Credit = r.Credit,
                            Balance = (balance += (r.Debit - r.Credit)),
                            Head = r.HeadName(),
                            VoucherNo = r.VoucherType + "-" + r.VoucherSerialNo
                        }).ToList();

            Amount = amount;
        }

        #region Relay Commands
        private RelayCommand _oKButtonClicked;
        public ICommand OKButtonClicked
        {
            get
            {
                return _oKButtonClicked ?? (_oKButtonClicked = new RelayCommand(p1 => this.InvokeOnFinish()));
            }
        }

        private void PrintPanel(Grid grid)
        {
            PrintDialog print = new PrintDialog();
            if (print.ShowDialog() == true)
            {
                // TODO: Uncomment the following to scale the printed portion to the print page. But test with a real printer.

                //PrintCapabilities capabilities = print.PrintQueue.GetPrintCapabilities(print.PrintTicket);
                //double scale = Math.Min(capabilities.PageImageableArea.ExtentWidth / grid.ActualWidth,
                //                        capabilities.PageImageableArea.ExtentHeight / grid.ActualHeight);
                
                //Transform oldTransform = grid.LayoutTransform;
                //grid.LayoutTransform = new ScaleTransform(scale, scale);

                //Size oldSize = new Size(grid.ActualWidth, grid.ActualHeight);
                //Size pageSize = new Size(capabilities.PageImageableArea.ExtentWidth, capabilities.PageImageableArea.ExtentHeight);
                
                //grid.Measure(pageSize);
                //((UIElement)grid).Arrange(new Rect(new Point(capabilities.PageImageableArea.OriginWidth, capabilities.PageImageableArea.OriginHeight), pageSize));
                
                print.PrintVisual(grid, "Voucher");
                
                //grid.LayoutTransform = oldTransform;
                //grid.Measure(oldSize);
                //((UIElement)grid).Arrange(new Rect(new Point(0, 0), oldSize));
            }
            return;
        }

        private SimpleCommand<Grid> _printButtonClicked;
        public ICommand PrintButtonClicked
        {
            get { return _printButtonClicked ?? (_printButtonClicked = new SimpleCommand<Grid> { CanExecuteDelegate = execute => true, ExecuteDelegate = PrintPanel }); }
        }

        private RelayCommand _deleteButtonClicked;
        public ICommand DeleteButtonClicked
        {
            get { return _deleteButtonClicked ?? (_deleteButtonClicked = new RelayCommand(p1 => DeleteVoucher() )); }
        }

        private void DeleteVoucher()
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this voucher?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _voucherManager.DeleteVoucher(VoucherItem.VoucherNo);
                InvokeOnFinish();

                //VoucherViewModel vm = new VoucherViewModel();
                //vm.NotifyVoucherGrid();
            }
        }

        #endregion
    }

    public class SimpleCommand<T> : ICommand
    {
        public Predicate<T> CanExecuteDelegate { get; set; }
        public Action<T> ExecuteDelegate { get; set; }

        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            if (CanExecuteDelegate != null)
                return CanExecuteDelegate((T)parameter);
            return true; // if there is no can execute default to true.
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            if (ExecuteDelegate != null)
                ExecuteDelegate((T)parameter);
        }

        #endregion
    }
}
