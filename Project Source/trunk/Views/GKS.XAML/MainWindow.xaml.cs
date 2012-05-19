using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BLL.Factories;
using GKS.Model.ViewModels;
using GKS.XAML.Pages;
using FixedAssetSchedule = GKS.XAML.Reports.FixedAssetSchedule;

using GKS.XAML.Reports;


namespace GKS.XAML
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void FixedAssetScheduleMenuItemClick(object sender, RoutedEventArgs e)
        {
            FixedAssetSchedule fixedAssetSchedule = new FixedAssetSchedule { Owner = this };
            fixedAssetSchedule.ShowDialog();
        }

        private void PostADebitVoucher_Click(object sender, RoutedEventArgs e)
        {
            VoucherPost voucherPostContext = postUC1.DataContext as VoucherPost;
            tabItemPost.Focus();
            voucherPostContext.SelectedVoucherType = "DV";
        }

        private void PostACreditVoucher_Click(object sender, RoutedEventArgs e)
        {
            VoucherPost voucherPostContext = postUC1.DataContext as VoucherPost;
            tabItemPost.Focus();
            voucherPostContext.SelectedVoucherType = "CV";
        }

        private void PostAJournalVoucher_Click(object sender, RoutedEventArgs e)
        {
            VoucherPost voucherPostContext = postUC1.DataContext as VoucherPost;
            tabItemPost.Focus();
            voucherPostContext.SelectedVoucherType = "JV";
        }

        private void PostAContraVoucher_Click(object sender, RoutedEventArgs e)
        {
            VoucherPost voucherPostContext = postUC1.DataContext as VoucherPost;
            tabItemPost.Focus();
            voucherPostContext.SelectedVoucherType = "Contra";
        }

        private void ViewAllProjects_Click(object sender, RoutedEventArgs e)
        {
            tabItemProjects.Focus();
        }

        private void AddANewProject_Click(object sender, RoutedEventArgs e)
        {
            tabItemProjects.Focus();
            projectMgmtUC1.buttonAdd_Click(sender, e);
        }

        private void ViewAllHeads_Click(object sender, RoutedEventArgs e)
        {
            tabItemHeads.Focus();
        }

        private void AddANewHead_Click(object sender, RoutedEventArgs e)
        {
            tabItemHeads.Focus();
            headMgmtUC1.buttonAdd_Click(sender, e);
        }

        private void ConfigurationSetting_Click(object sender, RoutedEventArgs e)
        {
            ConfigurationSetting configurationSetting = new ConfigurationSetting { Owner = this };
            configurationSetting.ShowDialog();
        }

        private void SetupBudgetClick(object sender, RoutedEventArgs e)
        {
            BudgetSetup budgetSetup = new BudgetSetup() { Owner = this };
            budgetSetup.ShowDialog();
        }

        private void StartNewAccountingYearClick(object sender, RoutedEventArgs e)
        {
            StartNewAccountingYear configurationSetting = new StartNewAccountingYear() { Owner = this };
            configurationSetting.ShowDialog();
        }

        private void CloseCurrentAccountingYearClick(object sender, RoutedEventArgs e)
        {
            CloseCurrentAccountingYear configurationSetting = new CloseCurrentAccountingYear { Owner = this };
            configurationSetting.ShowDialog();
        }

        private void FixedAssetManagement_Click(object sender, RoutedEventArgs e)
        {
            FixedAssetManagement configurationSetting = new FixedAssetManagement { Owner = this };
            configurationSetting.ShowDialog();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //Application.
        }
        
    }
}
