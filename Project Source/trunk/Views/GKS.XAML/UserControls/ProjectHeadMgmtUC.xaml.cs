using System.Windows.Controls;
using GKS.Model.ViewModels;

namespace GKS.XAML.UserControls
{
    public partial class ProjectHeadMgmtUC : UserControl
    {
        public ProjectHeadMgmtUC()
        {
            InitializeComponent();
            DataContext = new ProjectHeadManagementModel();
        }
    }
}
