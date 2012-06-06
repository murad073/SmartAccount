using BLL.Model.Entity;
namespace BLL.Model.Managers
{
    public interface IBudgetManager
    {
        bool Set(Project project, Head head, double amount, int budgetYear);
    }
}