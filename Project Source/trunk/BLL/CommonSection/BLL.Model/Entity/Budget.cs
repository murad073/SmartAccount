using System;
using System.Collections.Generic;

namespace BLL.Model.Entity
{
    public class Budget
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public string Note { get; set; }
        public bool IsActive { get; set; }
        public string AccountingYear { get; set; }

        public virtual ProjectHead ProjectHead { get; set; }
    }
}

