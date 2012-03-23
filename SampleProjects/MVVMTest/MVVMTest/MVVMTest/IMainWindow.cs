using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace MVVMTest
{
    public interface IMainWindow
    {
        string Name { get; set; }
        ICommand ButtonClicked { get; }
        IList<KeyValuePair<int, string>> ComboItems { get; set; }
        KeyValuePair<int, string> ComboSelected { get; set; }
        IEnumerable<GridViewSingleRow> GridViewItems { get; set; }
    }
}
