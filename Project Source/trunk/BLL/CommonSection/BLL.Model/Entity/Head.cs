using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.Model.Entity
{
    public class Head
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }
        public string HeadType { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ProjectHead> ProjectHeads { get; set; }
    }
}