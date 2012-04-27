using System;
using System.Collections.Generic;

namespace BLL.Model.Entity
{
    //public class Budget
    //{
    //    public Budget()
    //    {
    //        IsActive = true;
    //    }
    //    public int Id { get; set; }
    //    public int ProjectHeadId { get; set; }
    //    public DateTime Date { get; set; }
    //    public double Amount { get; set; }
    //    public string Note { get; set; }
    //    public bool IsActive { get; set; }
    //}

    public class Budget
    {
        public int ID { get; set; }
        //public int ProjectHeadID { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public string Note { get; set; }
        public bool IsActive { get; set; }

        public virtual ProjectHead ProjectHead { get; set; }
    }
}
