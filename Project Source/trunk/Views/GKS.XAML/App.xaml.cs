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
using CodeFirst;

namespace GKS.XAML
{
    public partial class App : Application
    {
        public App()
        {
            base.DispatcherUnhandledException += AppDispatcherUnhandledException;

            GKSFactory.RepositoryType = RepositoryType.CodeFirst;


            BLLCoreFactory.BudgetRepository = new Repository<Budget>();
            BLLCoreFactory.HeadRepository = new Repository<Head>();
            BLLCoreFactory.ParameterRepository = new Repository<Parameter>();
            BLLCoreFactory.ProjectHeadRepository = new Repository<ProjectHead>();
            BLLCoreFactory.ProjectRepository = new Repository<Project>();
            BLLCoreFactory.RecordRepository = new Repository<Record>();
            BLLCoreFactory.BankRecordRepository = new Repository<BankRecord>();
            BLLCoreFactory.FixedAssetRepository = new Repository<FixedAsset>();

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

