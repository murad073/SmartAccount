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
using BLL.Model.Entity;
using BLL.Model.Schema;
using GKS.Model;
using GKS.Model.ViewModels;

namespace GKS.XAML
{
    public partial class AddEditHeadWindow : Window
    {
        private AddEditHeadModel _vm;

        private void Init()
        {
            InitializeComponent();
            _vm = new AddEditHeadModel();
            _vm.OnFinish += (sender, eventArgs) => this.Close();
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
            _vm.Head = head;
        }

        public void SetOperationType(OperationType operationType)
        {
            _vm.OperationType = operationType;
        }
    }
}
