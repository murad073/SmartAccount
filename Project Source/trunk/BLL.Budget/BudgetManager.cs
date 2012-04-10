using BLL.Messaging;
using BLL.Model.Repositories;
using BLL.Model.Schema;
using System;

namespace BLL.BudgetManagement
{
    public class BudgetManager
    {
        private readonly IBudgetRepository _budgetRepository;
        private readonly IProjectRepository _projectRepository;
        private Message _latestMessage;

        public BudgetManager(IBudgetRepository budgetRepository, IProjectRepository projectRepository)
        {
            _budgetRepository = budgetRepository;
            _projectRepository = projectRepository;
            _latestMessage = new Message();
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
                    _latestMessage = MessageService.Instance.Get("NewBudgetSavedSuccessfully", MessageType.Success);
                    _latestMessage.MessageText = string.Format(_latestMessage.MessageText, insertedBudget.Date.Year);
                    return true;
                }
                _latestMessage = MessageService.Instance.Get("NewBudgetInsertFailed", MessageType.Error);
                return false;
            }
            else
            {
                budget.Amount = amount;
                budget.IsActive = true;
                bool isUpdate = _budgetRepository.Update(budget);
                if (isUpdate)
                    _latestMessage = MessageService.Instance.Get("BudgetUpdatedSuccessfully", MessageType.Success);
                else
                    _latestMessage = MessageService.Instance.Get("BudgetUpdatedFailed", MessageType.Error);
                return isUpdate;
            }
        }

        public Message GetLatestMessage()
        {
            return _latestMessage;
        }
    }
}
