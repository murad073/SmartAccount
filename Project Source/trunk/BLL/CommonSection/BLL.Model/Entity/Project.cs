using System;
using System.Collections.Generic;

namespace BLL.Model.Entity
{
    //public class Project
    //{
    //    public Project()
    //    {
    //        CreateDate = DateTime.Now;
    //    }

    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public string Description { get; set; }
    //    public DateTime CreateDate { get; set; }
    //    public bool IsActive { get; set; }
    //}

    public class Project
    {
        public int ID { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ProjectHead> ProjectHeads { get; set; }
    }
}
