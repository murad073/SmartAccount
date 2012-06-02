using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.Model.Entity
{
    public class Budget
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public string Note { get; set; }
        public bool IsActive { get; set; }
        public string FinancialYear { get; set; }

        [Required]
        public virtual ProjectHead ProjectHead { get; set; }
    }
}

