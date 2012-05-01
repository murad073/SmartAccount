using System.Collections.Generic;

namespace BLL.Model.Entity
{
    //public class Head
    //{
    //    public Head()
    //    {
    //        IsActive = true;
    //    }

    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public HeadType Type { get; set; }
    //    public string Description { get; set; }
    //    public bool IsActive { get; set; }
    //}

    public class Head
    {
        public int ID { get; set; }
        //public int ParentID { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string HeadType { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ProjectHead> ProjectHeads { get; set; }
    }
}