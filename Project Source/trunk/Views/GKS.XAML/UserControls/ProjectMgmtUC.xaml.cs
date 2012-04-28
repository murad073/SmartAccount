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

using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.ComponentModel;
using GKS.XAML.Pages;
//using tool = Microsoft.Windows.Controls;


namespace GKS.XAML.UserControls
{
    /// <summary>
    /// Interaction logic for ProjectMgmtUC.xaml
    /// </summary>
    public partial class ProjectMgmtUC : UserControl
    {
        public ProjectMgmtUC()
        {
            InitializeComponent();
            DataContext = new ProjectManagementModel();
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            ProjectManagementModel vm = DataContext as ProjectManagementModel;

            AddEditProjectWindow projectWindow = new AddEditProjectWindow {Owner = Window.GetWindow(this), CallbackOnClose = vm.Reset};
            projectWindow.SetOperationType(OperationType.Add);
            projectWindow.ShowDialog();
        }

        private void buttonView_Click(object sender, RoutedEventArgs e)
        {
            ProjectManagementModel vm = DataContext as ProjectManagementModel;
            Project project = vm.SelectedGridItem;
            if (project != null)
            {
                AddEditProjectWindow projectWindow = new AddEditProjectWindow(project) { Owner = Window.GetWindow(this), CallbackOnClose = vm.Reset };
                projectWindow.SetOperationType(OperationType.Update);
                projectWindow.ShowDialog();
            }
            else MessageBox.Show("No project is selected.");
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            ProjectManagementModel vm = DataContext as ProjectManagementModel;
            vm.Reset();
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            //ExportToExcel<Project, Projects> s = new ExportToExcel<Project, Projects>();
            //ICollectionView view = CollectionViewSource.GetDefaultView(dataGridAllProjects.ItemsSource);
            //s.dataToPrint = (Projects)view.SourceCollection;
            //s.GenerateReport();
        }

        private void buttonTest_Click(object sender, RoutedEventArgs e)
        {
            TestWindow testWnd = new TestWindow();
            testWnd.ShowDialog();
        }
    }

    
    /// <summary>
    /// Class for generator of Excel file
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    public class ExportToExcel<T, U>
        where T : class
        where U : List<T>
    {
        public List<T> dataToPrint;
        // Excel object references.
        private Excel.Application _excelApp = null;
        private Excel.Workbooks _books = null;
        private Excel._Workbook _book = null;
        private Excel.Sheets _sheets = null;
        private Excel._Worksheet _sheet = null;
        private Excel.Range _range = null;
        private Excel.Font _font = null;
        // Optional argument variable
        private object _optionalValue = Missing.Value;

        /// <summary>
        /// Generate report and sub functions
        /// </summary>
        public void GenerateReport()
        {
            try
            {
                if (dataToPrint != null)
                {
                    if (dataToPrint.Count != 0)
                    {
                        Mouse.SetCursor(Cursors.Wait);
                        CreateExcelRef();
                        FillSheet();
                        OpenReport();
                        Mouse.SetCursor(Cursors.Arrow);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error while generating Excel report.");
            }
            finally
            {
                ReleaseObject(_sheet);
                ReleaseObject(_sheets);
                ReleaseObject(_book);
                ReleaseObject(_books);
                ReleaseObject(_excelApp);
            }
        }
        /// <summary>
        /// Make MS Excel application visible
        /// </summary>
        private void OpenReport()
        {
            _excelApp.Visible = true;
        }
        /// <summary>
        /// Populate the Excel sheet
        /// </summary>
        private void FillSheet()
        {
            object[] header = CreateHeader();
            WriteData(header);
        }
        /// <summary>
        /// Write data into the Excel sheet
        /// </summary>
        /// <param name="header"></param>
        private void WriteData(object[] header)
        {
            object[,] objData = new object[dataToPrint.Count, header.Length];

            for (int j = 0; j < dataToPrint.Count; j++)
            {
                var item = dataToPrint[j];
                for (int i = 0; i < header.Length; i++)
                {
                    var y = typeof(T).InvokeMember(header[i].ToString(),
                    BindingFlags.GetProperty, null, item, null);
                    objData[j, i] = (y == null) ? "" : y.ToString();
                }
            }
            AddExcelRows("A2", dataToPrint.Count, header.Length, objData);
            AutoFitColumns("A1", dataToPrint.Count + 1, header.Length);
        }
        /// <summary>
        /// Method to make columns auto fit according to data
        /// </summary>
        /// <param name="startRange"></param>
        /// <param name="rowCount"></param>
        /// <param name="colCount"></param>
        private void AutoFitColumns(string startRange, int rowCount, int colCount)
        {
            _range = _sheet.get_Range(startRange, _optionalValue);
            _range = _range.get_Resize(rowCount, colCount);
            _range.Columns.AutoFit();
        }
        /// <summary>
        /// Create header from the properties
        /// </summary>
        /// <returns></returns>
        private object[] CreateHeader()
        {
            PropertyInfo[] headerInfo = typeof(T).GetProperties();

            // Create an array for the headers and add it to the
            // worksheet starting at cell A1.
            List<object> objHeaders = new List<object>();
            for (int n = 0; n < headerInfo.Length; n++)
            {
                objHeaders.Add(headerInfo[n].Name);
            }

            var headerToAdd = objHeaders.ToArray();
            AddExcelRows("A1", 1, headerToAdd.Length, headerToAdd);
            SetHeaderStyle();

            return headerToAdd;
        }
        /// <summary>
        /// Set Header style as bold
        /// </summary>
        private void SetHeaderStyle()
        {
            _font = _range.Font;
            _font.Bold = true;
        }
        /// <summary>
        /// Method to add an excel rows
        /// </summary>
        /// <param name="startRange"></param>
        /// <param name="rowCount"></param>
        /// <param name="colCount"></param>
        /// <param name="values"></param>
        private void AddExcelRows(string startRange, int rowCount,
        int colCount, object values)
        {
            _range = _sheet.get_Range(startRange, _optionalValue);
            _range = _range.get_Resize(rowCount, colCount);
            _range.set_Value(_optionalValue, values);
        }
        /// <summary>
        /// Create Excel application parameters instances
        /// </summary>
        private void CreateExcelRef()
        {
            _excelApp = new Excel.Application();
            _books = (Excel.Workbooks)_excelApp.Workbooks;
            _book = (Excel._Workbook)(_books.Add(_optionalValue));
            _sheets = (Excel.Sheets)_book.Worksheets;
            _sheet = (Excel._Worksheet)(_sheets.get_Item(1));
        }
        /// <summary>
        /// Release unused COM objects
        /// </summary>
        /// <param name="obj"></param>
        private void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
