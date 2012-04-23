using System.Collections.Generic;
using BLL.Model.Repositories;
using BLL.Model.Schema;

namespace Mock
{
    public class BudgetRepository : IBudgetRepository
    {
        private readonly SmartAccountEntities _db;
        public BudgetRepository()
        {
            _db = DBFactory.Instance.DB;
        }

        public Budget Get(int id)
        {
            DALBudget dalBudget = GetDALBudget(id);
            if (dalBudget == null) return null;
            return GetBLLBudget(dalBudget);
        }

        public Budget Insert(Budget entity)
        {
            DALBudget dalBudget = GetDALBudget(entity);
            _db.AddToBudgets(dalBudget);
            _db.SaveChanges();
            entity.Id = dalBudget.ID;
            return entity;
        }

        public Budget Delete(int id)
        {
            DALBudget dalBudget = GetDALBudget(id);
            if (dalBudget == null) return null;
            _db.DeleteObject(dalBudget);
            _db.SaveChanges();
            return GetBLLBudget(dalBudget);
        }

        public bool Update(Budget entity)
        {
            DALBudget dalBudget = GetDALBudget(entity.ProjectHeadId);
            if (dalBudget == null) return false;
            dalBudget.Amount = entity.Amount;
            dalBudget.Date = entity.Date;
            dalBudget.IsActive = entity.IsActive;
            dalBudget.Note = entity.Note;
            return _db.SaveChanges() > 0;
        }

        public IList<Budget> GetAll()
        {
            return _db.Budgets.Where(b=>b.IsActive==true).Select(GetBLLBudget).ToList();
        }

        public IList<Budget> GetAll(int projectId)
        {
            return _db.Budgets.Where(b => b.ProjectHead.ProjectID == projectId && b.IsActive==true).Select(GetBLLBudget).ToList();
        }

        internal static DALBudget GetDALBudget(int projectHeadId)
        {
            return DBFactory.Instance.DB.Budgets.Where(h => h.ProjectHeadID == projectHeadId && h.IsActive == true).SingleOrDefault();
        }

        internal static DALBudget GetDALBudget(string projectName, string headName)
        {
            ProjectHead projectHead = DBFactory.Instance.DB.ProjectHeads.Where(ph => ph.Project.Name == projectName && ph.Head.Name == headName).SingleOrDefault();
            return DBFactory.Instance.DB.Budgets.Where(b => b.ProjectHeadID == projectHead.ID && b.IsActive == true).SingleOrDefault();
        }

        internal static DALBudget GetDALBudget(Budget bllBudget)
        {
            return new DALBudget
                       {
                           Amount = bllBudget.Amount,
                           Date = bllBudget.Date,
                           ID = bllBudget.Id,
                           IsActive = bllBudget.IsActive,
                           Note = bllBudget.Note,
                           ProjectHeadID = bllBudget.ProjectHeadId
                       };
        }

        internal static Budget GetBLLBudget(DALBudget dalBudget)
        {
            return new Budget
            {
                Id = dalBudget.ID,
                Amount = dalBudget.Amount,
                Date = dalBudget.Date.Value,
                IsActive = dalBudget.IsActive == true,
                Note = dalBudget.Note,
                ProjectHeadId = dalBudget.ProjectHeadID.Value
            };
        }

        public Budget GetByProjectHeadId(int projectHeadId)
        {
            return
                _db.Budgets.Where(b => b.ProjectHead.ID == projectHeadId && b.IsActive == true).Select(GetBLLBudget).
                    SingleOrDefault();
        }
    }
}

