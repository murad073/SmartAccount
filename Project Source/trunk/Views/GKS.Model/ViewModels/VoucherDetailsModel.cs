using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;

namespace GKS.Model.ViewModels
{
    public class VoucherDetailsModel : INotifyPropertyChanged
    {
        public delegate void SimpleDelegate();
        public SimpleDelegate CloseWindow { get; set; }

        public VoucherDetailsModel()
        {
            OKButtonClicked = new VoucherDetailsOK(this);
            PrintButtonClicked = new PrintDetails(this);
        }

        private string _projectName;
        public string ProjectName
        {
            get { return _projectName; }
            set
            {
                _projectName = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ProjectName"));
            }
        }

        private string _voucherNo;
        public string VoucherNo
        {
            get { return _voucherNo; }
            set
            {
                _voucherNo = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("VoucherNo"));
            }
        }

        private string _voucherDate;
        public string VoucherDate
        {
            get { return _voucherDate; }
            set
            {
                _voucherDate = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("VoucherDate"));
            }
        }

        private string _chequeNo;
        public string ChequeNo
        {
            get { return _chequeNo; }
            set
            {
                _chequeNo = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ChequeNo"));
            }
        }

        private string _chequeDate;
        public string ChequeDate
        {
            get { return _chequeDate; }
            set
            {
                _chequeDate = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ChequeDate"));
            }
        }

        private string _bankName;
        public string BankName
        {
            get { return _bankName; }
            set
            {
                _bankName = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("BankName"));
            }
        }

        private string _takaInWords;
        public string TakaInWords
        {
            get { return _takaInWords; }
            set
            {
                _takaInWords = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("TakaInWords"));
            }
        }

        private string _voucherNarration;
        public string VoucherNarration
        {
            get { return _voucherNarration; }
            set
            {
                _voucherNarration = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("VoucherNarration"));
            }
        }

        public ICommand OKButtonClicked { get; set; }
        public ICommand PrintButtonClicked { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class VoucherDetailsOK : ICommand
    {
        VoucherDetailsModel _voucherModel;
        public VoucherDetailsOK(VoucherDetailsModel voucherModel)
        {
            _voucherModel = voucherModel;
        }

        public bool CanExecute(object parameter)
        {
            //if (_voucherModel.CloseWindow != null)
            //    return true;
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _voucherModel.CloseWindow();
        }
    }

    public class PrintDetails : ICommand
    {
        VoucherDetailsModel _voucherModel;
        public PrintDetails(VoucherDetailsModel voucherModel)
        {
            _voucherModel = voucherModel;
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
