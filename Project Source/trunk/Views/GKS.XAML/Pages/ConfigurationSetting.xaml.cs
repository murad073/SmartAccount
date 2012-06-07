using System;
using System.Windows;
using GKS.Model.ViewModels;

namespace GKS.XAML.Pages
{
    /// <summary>
    /// Interaction logic for FixedAssetSchedule.xaml
    /// </summary>
    public partial class ConfigurationSetting : Window
    {
        public ConfigurationSetting()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            propertyGrid.SelectedObject = new ConfigurationSettingModel();
        }
    }
}
