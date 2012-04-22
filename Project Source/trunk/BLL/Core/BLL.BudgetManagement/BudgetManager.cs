using System.Collections.Generic;
using BLL.Model;
using BLL.Model.Managers;
using BLL.Model.Repositories;
using BLL.Model.Schema;
using System;

namespace BLL.BudgetManagement
{
    public class BudgetManager : ManagerBase, IBudgetManager
    {
        private readonly IBudgetRepository _budgetRepository;
        private readonly IProjectRepository _projectRepository;

        public BudgetManager(IBudgetRepository budgetRepository, IProjectRepository projectRepository)
        {
            _budgetRepository = budgetRepository;
            _projectRepository = projectRepository;
        }

        public bool Set(string projectName, string headName, double amount)
        {
            int projectHeadId = _projectRepository.GetProjectHeadId(projectName, headName);
            Budget budget = _budgetRepository.GetByProjectHeadId(projectHeadId);
            if (budget == null)
            {
                Budget newBudget = new Budget
                                       {
                                           Amount = amount,
                                           Date = DateTime.Now,
                                           IsActive = true,
                                           ProjectHeadId = projectHeadId
                                       };
                Budget insertedBudget = _budgetRepository.Insert(newBudget);

                if (insertedBudget != null)
                {
                    InvokeManagerEvent(new BLLEventArgs { EventType = EventType.Success, MessageKey = "NewBudgetSavedSuccessfully", Parameters = new Dictionary<string, string> { { "BudgetYear", insertedBudget.Date.Year.ToString() } } });
                    return true;
                }
                InvokeManagerEvent(EventType.Error, "NewBudgetInsertFailed");
                return false;
            }
            else
            {
                budget.Amount = amount;
                budget.IsActive = true;
                bool isUpdate = _budgetRepository.Update(budget);
                if (isUpdate)
                    InvokeManagerEvent(EventType.Success, "BudgetUpdatedSuccessfully");
                else
                    InvokeManagerEvent(EventType.Error, "BudgetUpdatedFailed");
                return isUpdate;
            }
        }
    }
}
