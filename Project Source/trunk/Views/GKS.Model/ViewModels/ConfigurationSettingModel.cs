using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using BLL.Factories;
using BLL.Model.Entity;
using BLL.Model.Managers;

namespace GKS.Model.ViewModels
{
    public class ConfigurationSettingModel : ViewModelBase
    {
        private readonly IParameterManager _parameterManager;
        public ConfigurationSettingModel()
        {
            _parameterManager = BLLCoreFactory.GetParameterManager();
        }

        //private string _financialYearStartDate;
        [DisplayName("Financial Year Start Date"), Description("The date when new financial year starts.")]
        public DateTime FinancialYearStartDate
        {
            get { return DateTime.Parse(_parameterManager.Get("FinancialYearStartDate")); }
            set { _parameterManager.Set("FinancialYearStartDate", value.ToString()); NotifyPropertyChanged("FinancialYearStartDate"); }
        }

        //private string _currentFinancialYear;
        [DisplayName("Current Financial Year"), ReadOnly(true),  Description("Current opened/running accounting year. It will cause effect in your full system.")]
        public string CurrentFinancialYear
        {
            get { return _parameterManager.Get("CurrentFinancialYear"); }
            set { _parameterManager.Set("CurrentFinancialYear", value); NotifyPropertyChanged("CurrentFinancialYear"); }
        }

        ////private string _currentFinancialYearStartDate;
        //[DisplayName()]
        //public string CurrentFinancialYearStartDate
        //{
        //    get { return _parameterManager.Get("CurrentFinancialYearStartDate"); }
        //    set { _parameterManager.Set("CurrentFinancialYearStartDate", value); NotifyPropertyChanged("CurrentFinancialYearStartDate"); }
        //}

        //private string _bankName;
        [DisplayName("Bank Name"), Description("The bank where official bank account opened.")]
        public string BankName
        {
            get { return _parameterManager.Get("BankName"); }
            set { _parameterManager.Set("BankName", value); NotifyPropertyChanged("BankName"); }
        }

        //private string _bankAccountNumber;
        [DisplayName("Bank Account Number"), Description("Official bank account number")]
        public string BankAccountNumber
        {
            get { return _parameterManager.Get("BankAccountNumber"); }
            set { _parameterManager.Set("BankAccountNumber", value); NotifyPropertyChanged("BankAccountNumber"); }
        }

    }
}
