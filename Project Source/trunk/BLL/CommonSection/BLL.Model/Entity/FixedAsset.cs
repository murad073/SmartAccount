using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.Model.Entity
{
    public class FixedAsset
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double DepreciationRate { get; set; }
        public int DepreciatedValue { get; set; }
        public string DepreciationType { get; set; }
        public bool ByForceDisposed { get; set; }
        public bool LifeTimeFinished { get; set; }

        [Required]
        public virtual Record Record { get; set; }
    }
}

