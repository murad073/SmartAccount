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
using GKS.Factory;

namespace GKS.XAML
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            GKSFactory.RepositoryType = RepositoryType.SqlCompact;
            //GKSFactory.RepositoryType = RepositoryType.SqlExpress;

            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            ci.DateTimeFormat.LongDatePattern = "dd-MM-yyyy";
            ci.DateTimeFormat.FullDateTimePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            //BLLCoreFactory.HeadRepository = GKSFactory.GetHeadRepository();
            //BLLCoreFactory.LedgerRepository = GKSFactory.GetLedgerRepository();
            //BLLCoreFactory.ParameterRepository = GKSFactory.GetParameterRepository();
            //BLLCoreFactory.ProjectRepository = GKSFactory.GetProjectRepository();
            //BLLCoreFactory.RecordRepository = GKSFactory.GetRecordRepository();

            InitializeComponent();
        }
    }
}
