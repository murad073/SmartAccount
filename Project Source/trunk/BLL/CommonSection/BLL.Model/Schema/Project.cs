using System;
using System.Collections.Generic;

namespace BLL.Model.Schema
{
    public class Project
    {
        public Project()
        {
            CreateDate = DateTime.Now;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }
    }

    public class Projects : List<Project> { }
}
