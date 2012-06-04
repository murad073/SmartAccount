using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace BLL.Model.Entity
{
    public class DepreciationRate
    {
        public int ID { get; set; }
        public double Rate { get; set; }
        public bool IsActive { get; set; }
        public string FinancialYear { get; set; }

        [Required]
        public virtual ProjectHead ProjectHead { get; set; }
    }
}
