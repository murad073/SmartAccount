using BLL.Model.Entity;

using System.Collections.Generic;


namespace BLL.Model.Managers
{
    public interface IBudgetManager
    {

        //bool Set(Project project, Head head, string financialYear, double budgetAmount);
        IList<Budget> GetAllBudgets();
        bool Set(Project project, Head head, double amount, int budgetYear);
    }
}