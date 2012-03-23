using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Model.Schema
{
    public class Parameter
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public bool IsActive { get; set; }
    }
}
