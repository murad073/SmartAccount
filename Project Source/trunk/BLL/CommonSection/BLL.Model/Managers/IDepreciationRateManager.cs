namespace BLL.Model.Managers
{
    public interface IDepreciationRateManager
    {
        bool Set(string projectName, string headName, double rate);
    }
}
