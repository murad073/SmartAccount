using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BLL.Model.Schema;
using GKS.Model.ViewModels;

namespace GKS.XAML
{
    /// <summary>
    /// Interaction logic for AddEditHeadPage.xaml
    /// </summary>
    public partial class AddEditHeadWindow : Window
    {
        public delegate void SimpleDelegate();
        public SimpleDelegate CallbackOnClose { get; set; }
        private AddEditHeadModel _vm;

        private void Init()
        {
            InitializeComponent();
            _vm = new AddEditHeadModel();
            _vm.CloseWindow = () => { CallbackOnClose(); this.Close(); };
            DataContext = _vm;

            this.PreviewKeyDown += HandleEsc;
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        public AddEditHeadWindow()
        {
            Init();
        }

        public AddEditHeadWindow(Head head)
        {
            Init();
            _vm.HeadName = head.Name;
            _vm.HeadDescription = head.Description;
            _vm.CurrentHeadOption = head.Type;
            _vm.IsActive = head.IsActive;
        }

        public void SetOperationType(OperationType operationType)
        {
            _vm.OperationType = operationType;
        }
    }
}
