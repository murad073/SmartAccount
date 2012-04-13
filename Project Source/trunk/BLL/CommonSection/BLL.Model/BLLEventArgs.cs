using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Model
{
    public enum EventType
    {
        None,
        Success,
        Error,
        Warning,
        Information,
        Exception
    }


    public class BLLEventArgs : EventArgs
    {
        public string Module { get; set; }
        public string MessageKey { get; set; }
        public string MessageDescription { get; set; }
        public EventType EventType { get; set; }
        public Dictionary<string, string> Parameters { get; set; }

        public Exception Exception { get; set; }
    }
}
