using System.Collections.Generic;
using BLL.Model;
using BLL.Model.Entity;
using BLL.Model.Managers;
using BLL.Model.Repositories;
using System;
using System.Linq;

namespace BLL.BudgetManagement
{
    public class BudgetManager : ManagerBase, IBudgetManager
    {
        private readonly IRepository<Budget> _budgetRepository;
        private readonly IRepository<Project> _projectRepository;
        private readonly IRepository<ProjectHead> _projectHeadRepository;

        public BudgetManager(IRepository<Budget> budgetRepository, IRepository<Project> projectRepository, IRepository<ProjectHead> projectHeadRepository)
        {
            _budgetRepository = budgetRepository;
            _projectRepository = projectRepository;
            _projectHeadRepository = projectHeadRepository;
        }

        public bool Set(string projectName, string headName, double amount)
        {
            ProjectHead projectHead = _projectHeadRepository.GetSingle(ph => ph.Head.Name == headName && ph.Project.Name == projectName);
            Budget budget = projectHead.Budgets.SingleOrDefault(b => b.IsActive);
            //TODO: depends on current accounting year. 
            if (budget == null)
            {
                return InsertNewBudget(projectHead, amount);
            }
            else
            {
                budget.IsActive = false;
                return InsertNewBudget(projectHead, amount);
            }
        }

        private bool InsertNewBudget(ProjectHead projectHead, double amount)
        {
            Budget newBudget = new Budget
            {
                Amount = amount,
                Date = DateTime.Now,
                IsActive = true,
                ProjectHead = projectHead
            };
            _budgetRepository.Insert(newBudget);

            if (_budgetRepository.Save() > 0)
            {
                InvokeManagerEvent(new BLLEventArgs { EventType = EventType.Success, MessageKey = "NewBudgetSavedSuccessfully", Parameters = new Dictionary<string, string> { { "BudgetYear", newBudget.Date.Year.ToString() } } });
                return true;
            }
            InvokeManagerEvent(EventType.Error, "NewBudgetInsertFailed");
            return false;
        }
    }
}
