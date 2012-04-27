using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace GKS.XAML
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            base.DispatcherUnhandledException += AppDispatcherUnhandledException;
        }

        static void AppDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            File.WriteAllText("errorlog.txt", e.Exception.ToString());
            e.Handled = false;
        }
    }
}

