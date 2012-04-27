using System.Collections.Generic;

namespace BLL.Model.Entity
{
    //public class FixedAsset
    //{
    //    public string Name { get; set; }
    //    public double DepreciationRate { get; set; }
    //}

    public class FixedAsset
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double? DepreciationRate { get; set; }
        public int? DepreciatedValue { get; set; }
        public string DepreciationType { get; set; }
        public bool? ByForceDisposed { get; set; }
        //public int RecordID { get; set; }

        public virtual Record Record { get; set; }
    }
}

