namespace BLL.Model.Schema
{
    public class Head
    {
        public Head()
        {
            IsActive = true;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public HeadType Type { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}