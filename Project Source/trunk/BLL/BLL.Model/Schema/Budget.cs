using System;

namespace BLL.Model.Schema
{
    public class Budget
    {
        public Budget()
        {
            IsActive = true;
        }
        public int Id { get; set; }
        public int ProjectHeadId { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public string Note { get; set; }
        public bool IsActive { get; set; }
    }
}
