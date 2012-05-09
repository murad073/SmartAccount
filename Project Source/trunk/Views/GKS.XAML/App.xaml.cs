using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using BLL.Factories;
using BLL.Model.Entity;
using GKS.Factory;

namespace GKS.XAML
{
    public partial class App : Application
    {
        public App()
        {
            base.DispatcherUnhandledException += AppDispatcherUnhandledException;
            GKSFactory.RepositoryType = RepositoryType.CodeFirst;

            BLLCoreFactory.BudgetRepository = GKSFactory.GetRepository<Budget>();
            BLLCoreFactory.HeadRepository = GKSFactory.GetRepository<Head>();
            BLLCoreFactory.ParameterRepository = GKSFactory.GetRepository<Parameter>();
            BLLCoreFactory.ProjectHeadRepository = GKSFactory.GetRepository<ProjectHead>();
            BLLCoreFactory.ProjectRepository = GKSFactory.GetRepository<Project>();
            BLLCoreFactory.RecordRepository = GKSFactory.GetRepository<Record>();
            BLLCoreFactory.BankRecordRepository = GKSFactory.GetRepository<BankRecord>();
            BLLCoreFactory.FixedAssetRepository = GKSFactory.GetRepository<FixedAsset>();

            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            ci.DateTimeFormat.LongDatePattern = "dd-MM-yyyy";
            ci.DateTimeFormat.FullDateTimePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;
        }

        static void AppDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            File.WriteAllText("errorlog.txt", e.Exception.ToString());
            e.Handled = false;
        }
    }
}

