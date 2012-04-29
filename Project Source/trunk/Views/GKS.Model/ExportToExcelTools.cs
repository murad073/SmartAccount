using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace GKS.Model
{
    public static class DataGridExcelTools
    {
        #region Attached Property

        /// <summary>
        /// Include current column in export report to excel
        /// </summary>
        public static readonly DependencyProperty IsExportedProperty = DependencyProperty.RegisterAttached("IsExported",
                                                                                        typeof(bool), typeof(DataGrid), new PropertyMetadata(true));

        /// <summary>
        /// Use custom header for report
        /// </summary>
        public static readonly DependencyProperty HeaderForExportProperty = DependencyProperty.RegisterAttached("HeaderForExport",
                                                                                        typeof(string), typeof(DataGrid), new PropertyMetadata(null));

        /// <summary>
        /// Use custom path to get value for report
        /// </summary>
        public static readonly DependencyProperty PathForExportProperty = DependencyProperty.RegisterAttached("PathForExport",
                                                                                        typeof(string), typeof(DataGrid), new PropertyMetadata(null));

        /// <summary>
        /// Use custom path to get value for report
        /// </summary>
        public static readonly DependencyProperty FormatForExportProperty = DependencyProperty.RegisterAttached("FormatForExport",
                                                                                        typeof(string), typeof(DataGrid), new PropertyMetadata(null));

        #region Attached properties helpers methods

        public static void SetIsExported(DataGridColumn element, Boolean value)
        {
            element.SetValue(IsExportedProperty, value);
        }

        public static Boolean GetIsExported(DataGridColumn element)
        {
            return (Boolean)element.GetValue(IsExportedProperty);
        }

        public static void SetPathForExport(DataGridColumn element, string value)
        {
            element.SetValue(PathForExportProperty, value);
        }

        public static string GetPathForExport(DataGridColumn element)
        {
            return (string)element.GetValue(PathForExportProperty);
        }

        public static void SetHeaderForExport(DataGridColumn element, string value)
        {
            element.SetValue(HeaderForExportProperty, value);
        }

        public static string GetHeaderForExport(DataGridColumn element)
        {
            return (string)element.GetValue(HeaderForExportProperty);
        }

        public static void SetFormatForExport(DataGridColumn element, string value)
        {
            element.SetValue(FormatForExportProperty, value);
        }

        public static string GetFormatForExport(DataGridColumn element)
        {
            return (string)element.GetValue(FormatForExportProperty);
        }

        #endregion

        #endregion

        /// <summary>
        /// Export data from <paramref name="grid"/> to Excel
        /// </summary>
        /// <param name="grid"></param>
        public static void ExportToExcel(this DataGrid grid)
        {
            Thread thread = new Thread(StartExport);
            thread.Start(PrepareData(grid));
        }

        private static void StartExport(object data)
        {
            ExportManager.ExportToExcel(data as object[,]);
        }

        private static object[,] PrepareData(DataGrid grid)
        {
            // Get only columns which have binding or have custom binding
            List<DataGridColumn> columns = grid.Columns.Where(x => (GetIsExported(x) && ((x is DataGridBoundColumn)
                                                || (!string.IsNullOrEmpty(GetPathForExport(x))) || (!string.IsNullOrEmpty(x.SortMemberPath))))).ToList();

            // Get list of items, bounded to grid
            List<object> list = grid.ItemsSource.Cast<object>().ToList();

            // Create data array (using array for data export optimization)
            object[,] data = new object[list.Count + 1, columns.Count];

            for (int columnIndex = 0; columnIndex < columns.Count; columnIndex++)
            {
                DataGridColumn gridColumn = columns[columnIndex];

                data[0, columnIndex] = GetHeader(gridColumn);

                string[] path = GetPath(gridColumn);

                string formatForExport = GetFormatForExport(gridColumn);

                if (path != null)
                {
                    // Fill data with values
                    for (int rowIndex = 1; rowIndex <= list.Count; rowIndex++)
                    {
                        object source = list[rowIndex - 1];
                        data[rowIndex, columnIndex] = GetValue(path, source, formatForExport);
                    }
                }
            }

            return data;
        }

        private static string GetHeader(DataGridColumn column)
        {
            string headerForExport = GetHeaderForExport(column);
            if (headerForExport == null && column.Header != null)
                return column.Header.ToString();
            return headerForExport;
        }

        /// <summary>
        /// Get value of <paramref name="obj"/> by <paramref name="path"/>
        /// </summary>
        /// <param name="path"></param>
        /// <param name="obj"></param>
        /// <param name="formatForExport"></param>
        /// <returns></returns>
        private static object GetValue(string[] path, object obj, string formatForExport)
        {
            foreach (string pathStep in path)
            {
                if (obj == null)
                    return null;

                Type type = obj.GetType();
                PropertyInfo property = type.GetProperty(pathStep);

                if (property == null)
                {
                    Debug.WriteLine(string.Format("Couldn't find property '{0}' in type '{1}'", pathStep, type.Name));
                    return null;
                }

                obj = property.GetValue(obj, null);
            }

            if (!string.IsNullOrEmpty(formatForExport))
                return string.Format("{0:" + formatForExport + "}", obj);

            return obj;
        }

        /// <summary>
        /// Get path to get value. First try to get attached path value, then try to get path from binding
        /// </summary>
        /// <param name="gridColumn"></param>
        /// <returns></returns>
        private static string[] GetPath(DataGridColumn gridColumn)
        {
            string path = GetPathForExport(gridColumn);

            if (string.IsNullOrEmpty(path))
            {
                if (gridColumn is DataGridBoundColumn)
                {
                    Binding binding = ((DataGridBoundColumn)gridColumn).Binding as Binding;
                    if (binding != null)
                    {
                        path = binding.Path.Path;
                    }
                }
                else
                {
                    path = gridColumn.SortMemberPath;
                }
            }

            return string.IsNullOrEmpty(path) ? null : path.Split('.');
        }
    }
}
