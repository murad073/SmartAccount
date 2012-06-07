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
        public IList<Budget> GetAllBudgets()
        {
            return _budgetRepository.GetAll().ToList();
        }



        //<<<<<<< HEAD
        //        public bool Set(Project project, Head head, string financialYear, double budgetAmount)
        //        {
        //            ProjectHead projectHead = _projectHeadRepository.GetSingle(ph => ph.Project.ID == project.ID && ph.Head.ID == head.ID );
        //            if (projectHead == null) return false;
        //            Budget budget = projectHead.Budgets.SingleOrDefault(b => b.FinancialYear == financialYear && b.IsActive);
        //            //TODO: depends on current accounting year. 
        //            if (budget == null)
        //            {
        //                return InsertNewBudget(projectHead, budgetAmount);
        //=======


        public bool Set(Project project, Head head, double amount, int budgetYear)
        {
            if (project == null)
            {
                InvokeManagerEvent(EventType.Error, "NoProjectSelected");
                return false;
                //>>>>>>> githubJakaria42/master
            }
            if (head == null)
            {
                InvokeManagerEvent(EventType.Error, "NoHeadSelected");
                return false;
            }
            if (amount == 0)
            {
                // TODO: This will not work right now, think through. But not a big problem.
                InvokeManagerEvent(EventType.Warning, "ZeroBudgetProvidedForFixedAsset");
            }

            string projectName = project.Name;
            string headName = head.Name;

            ProjectHead projectHead = _projectHeadRepository.GetSingle(ph => ph.Project.ID == project.ID && ph.Head.ID == head.ID);

            bool update = false;
            if (projectHead != null && projectHead.Budgets != null)
            {
                //<<<<<<< HEAD
                //                budget.IsActive = false;
                //                return InsertNewBudget(projectHead, budgetAmount);
                //=======
                Budget budget = projectHead.Budgets.SingleOrDefault(b => b.FinancialYear == budgetYear.ToString());
                if (budget != null)
                {
                    budget.Amount = amount;
                    budget.FinancialYear = budgetYear.ToString();
                    budget.Date = DateTime.Today;
                    budget.IsActive = budgetYear < DateTime.Now.Year ? false : true;

                    update = true;
                }
                else
                {
                    budget = new Budget
                    {
                        Amount = amount,
                        FinancialYear = budgetYear.ToString(),
                        Date = DateTime.Today,
                        IsActive = budgetYear < DateTime.Now.Year ? false : true,
                        ProjectHead = projectHead
                    };
                }

                return InsertOrUpdateBudget(budget, update);
                //>>>>>>> githubJakaria42/master
            }
            else
                return false;

            //ProjectHead projectHead = _projectHeadRepository.GetSingle(ph => ph.Head.Name == headName && ph.Project.Name == projectName);
            //Budget budget = projectHead.Budgets.SingleOrDefault(b => b.IsActive);
            ////TODO: depends on current accounting year. 
            //if (budget == null)
            //{
            //    return InsertNewBudget(projectHead, amount);
            //}
            //else
            //{
            //    budget.IsActive = false;
            //    return InsertNewBudget(projectHead, amount);
            //}
        }

        private bool InsertOrUpdateBudget(Budget budget, bool update)
        {
            if (update)
                _budgetRepository.Update(budget);
            else
                _budgetRepository.Insert(budget);

            if (_budgetRepository.Save() > 0)
            {
                InvokeManagerEvent(new BLLEventArgs { EventType = EventType.Success, MessageKey = "NewBudgetSavedSuccessfully", Parameters = new Dictionary<string, string> { { "BudgetYear", budget.Date.Year.ToString() } } });
                return true;
            }

            InvokeManagerEvent(EventType.Error, "BudgetUpdatedFailed");
            return false;
        }

        //private bool InsertNewBudget(ProjectHead projectHead, double amount)
        //{
        //    Budget newBudget = new Budget
        //    {
        //        Amount = amount,
        //        Date = DateTime.Now,
        //        IsActive = true,
        //        ProjectHead = projectHead
        //    };
        //    _budgetRepository.Insert(newBudget);

        //    if (_budgetRepository.Save() > 0)
        //    {
        //        InvokeManagerEvent(new BLLEventArgs { EventType = EventType.Success, MessageKey = "NewBudgetSavedSuccessfully", Parameters = new Dictionary<string, string> { { "BudgetYear", newBudget.Date.Year.ToString() } } });
        //        return true;
        //    }
        //    InvokeManagerEvent(EventType.Error, "NewBudgetInsertFailed");
        //    return false;
        //}
    }
}
