using System;
using System.ComponentModel;
using System.Windows.Input;
using BLL.Utils;
using System.Collections.Generic;
using BLL.Model.Entity;
using BLL.Factories;
using BLL.Model.Repositories;
using System.Windows.Controls;
using System.Printing;
using System.Windows.Media;
using System.Windows;

namespace GKS.Model.ViewModels
{
    public class VoucherDetailsModel : ViewModelBase
    {
        private IRepository<Record> _recordRepository;
        public VoucherDetailsModel()
        {
            _recordRepository = BLLCoreFactory.RecordRepository;
        }

        private string _projectName;
        public string ProjectName
        {
            get { return _projectName; }
            set
            {
                _projectName = value;
                NotifyPropertyChanged("ProjectName");
            }
        }

        private string _voucherNo;
        public string VoucherNo
        {
            get { return _voucherNo; }
            set
            {
                _voucherNo = value;
                NotifyPropertyChanged("VoucherNo");
            }
        }

        private DateTime _voucherDate;
        public DateTime VoucherDate
        {
            get { return _voucherDate; }
            set
            {
                _voucherDate = value;
                NotifyPropertyChanged("VoucherDate");
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
                TakaInWords = Utilities.NumberToTextInLacCrore(((double)_amount).ToString()) + "Only";
                NotifyPropertyChanged("Amount");
            }
        }

        private string _chequeNo;
        public string ChequeNo
        {
            get { return _chequeNo; }
            set
            {
                _chequeNo = value;
                NotifyPropertyChanged("ChequeNo");
            }
        }

        private DateTime _chequeDate;
        public DateTime ChequeDate
        {
            get { return _chequeDate; }
            set
            {
                _chequeDate = value;
                NotifyPropertyChanged("ChequeDate");
            }
        }

        private string _bankName;
        public string BankName
        {
            get { return _bankName; }
            set
            {
                _bankName = value;
                NotifyPropertyChanged("BankName");
            }
        }

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

        private string _voucherNarration;
        public string VoucherNarration
        {
            get { return _voucherNarration; }
            set
            {
                _voucherNarration = value;
                NotifyPropertyChanged("VoucherNarration");
            }
        }

        // TODO (MURAD):
        //private IList<Record> _recordItems;
        //private IList<Record> RecordItems
        //{
        //    get
        //    {
        //        return _recordItems;
        //    }
        //    set
        //    {
        //        string[] voucherNo = VoucherNo.Split('-');
        //        _recordItems = _recordRepository.Get(r => r.VoucherType == voucherNo[0] && r.VoucherSerialNo.ToString() == voucherNo[1]).ToList();
        //    }
        //}

        //public IList<ViewableItem> VoucherGridItems
        //{
        //    get
        //    {
        //        if (RecordItems == null || RecordItems.Count == 0) return null;
        //        double balance = 0;
        //        return RecordItems.Select(v => new ViewableItem
        //        {
        //            Balance = (balance += (v.Debit - v.Credit)),
        //            Debit = v.Debit,
        //            Credit = v.Credit,
        //            HeadName = v.HeadName(),
        //        }).ToList();
        //    }
        //}

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
                // Uncomment the following to scale the printed portion to the print page.

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
            get { return _printButtonClicked ?? (_printButtonClicked = new SimpleCommand<Grid> { CanExecuteDelegate = execute => true, ExecuteDelegate = grid => { PrintPanel(grid); } }); }
        }

        private RelayCommand _deleteButtonClicked;
        public ICommand DeleteButtonClicked
        {
            get { return _deleteButtonClicked ?? (_deleteButtonClicked = new RelayCommand(p1 => this.InvokeOnFinish())); }
        }
        #endregion
    }

    public class ViewableItem
    {
        string HeadName { get; set; }
        double Debit { get; set; }
        double Credit { get; set; }
        double Balance { get; set; }
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
