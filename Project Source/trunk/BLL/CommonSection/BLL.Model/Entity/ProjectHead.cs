using System.Collections.Generic;

namespace BLL.Model.Entity
{
    public class ProjectHead
    {
        public int ID { get; set; }
        public bool IsActive { get; set; }

        public virtual Project Project { get; set; }
        public virtual Head Head { get; set; }

        public virtual ICollection<Record> Records { get; set; }
        public virtual ICollection<Budget> Budgets { get; set; }
        public virtual ICollection<DepreciationRate> DepreciationRates { get; set; }
        public virtual ICollection<OpeningBalance> OpeningBalances { get; set; }
    }
}
