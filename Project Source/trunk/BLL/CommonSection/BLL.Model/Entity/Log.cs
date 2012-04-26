using System;

namespace BLL.Model.Entity
{
    public class Log
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string UserName { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
    }
}
