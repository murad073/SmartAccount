namespace BLL.Model.Managers
{
    public interface IBudgetManager
    {
        bool Set(string projectName, string headName, double amount);
    }
}