using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.ComponentModel;

namespace GKS.XAML.Pages
{
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        public TestWindow()
        {
            InitializeComponent();

            Employees employees = new Employees();

            Employee employee1 = new Employee();
            employee1.EmployeeID = 001;
            employee1.EmployeeName = "Jakaria";
            employee1.EmployeeDesignation = "User Experience";

            employees.Add(employee1);

            Employee employee2 = new Employee();
            employee2.EmployeeID = 002;
            employee2.EmployeeName = "Moshiur";
            employee2.EmployeeDesignation = "Development";

            employees.Add(employee2);

            Employee employee3 = new Employee();
            employee3.EmployeeID = 003;
            employee3.EmployeeName = "Nazmul";
            employee3.EmployeeDesignation = "Database";

            employees.Add(employee3);

            dataGridEmployees.ItemsSource = employees;
        }

        private void buttonExport_Click(object sender, RoutedEventArgs e)
        {
            ExportToExcel<Employee, Employees> s = new ExportToExcel<Employee, Employees>();
            ICollectionView view = CollectionViewSource.GetDefaultView(dataGridEmployees.ItemsSource);
            s.dataToPrint = (Employees)view.SourceCollection;
            s.GenerateReport();
        }
    }

    public class Employees : List<Employee> { }
    public class Employee
    {
        private int _id;
        public int EmployeeID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        private string _name;
        public string EmployeeName
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        private string _designation;
        public string EmployeeDesignation
        {
            get
            {
                return _designation;
            }
            set
            {
                _designation = value;
            }
        }
    }
}
