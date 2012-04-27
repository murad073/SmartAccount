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
using GKS.Model.ViewModels;

namespace GKS.XAML
{
    public partial class AddEditProjectWindow : Window
    {
        public delegate void SimpleDelegate();
        public SimpleDelegate CallbackOnClose { get; set; }
        private AddEditProjectModel _vm;

        private void Init()
        {
            InitializeComponent();
            _vm = new AddEditProjectModel();
            _vm.CloseWindow = () => { CallbackOnClose(); this.Close(); };
            DataContext = _vm;

            this.PreviewKeyDown += HandleEsc;
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        public AddEditProjectWindow()
        {
            Init();
        }

        public AddEditProjectWindow(Project project)
        {
            Init();
            _vm.ProjectNameText = project.Name;
            _vm.DescriptionText = project.Description;
            _vm.CreateDate = project.CreateDate;
            _vm.IsActive = project.IsActive;
        }

        public void SetOperationType(OperationType operationType)
        {
            _vm.OperationType = operationType;
        }
    }
}

