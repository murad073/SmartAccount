using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Model.Schema;
using DALBudget = SQL2K8.Budget;
using BLLBudget = BLL.Model.Schema.Budget;
using BLL.Model.Repositories;

namespace SQL2K8
{
    public class BudgetRepository : IBudgetRepository
    {
        private SmartAccountEntities _db;
        public BudgetRepository()
        {
            _db = DBFactory.Instance.DB;
        }

        public BLLBudget Get(int id)
        {
            DALBudget dalBudget = GetDALBudget(id);
            if (dalBudget == null) return null;
            return GetBLLBudget(dalBudget);
        }

        public BLLBudget Insert(BLLBudget entity)
        {
            DALBudget dalBudget = GetDALBudget(entity);
            _db.AddToBudgets(dalBudget);
            _db.SaveChanges();
            entity.Id = dalBudget.ID;
            return entity;
        }

        public BLLBudget Delete(int id)
        {
            DALBudget dalBudget = GetDALBudget(id);
            if (dalBudget == null) return null;
            _db.DeleteObject(dalBudget);
            _db.SaveChanges();
            return GetBLLBudget(dalBudget);
        }

        public bool Update(BLLBudget entity)
        {
            DALBudget dalBudget = GetDALBudget(entity.Name);
            if (dalBudget == null) return false;
            //dalBudget.Name = entity.Name;
            dalBudget.Type = entity.Type.ToString();
            dalBudget.Description = entity.Description;
            dalBudget.IsActive = entity.IsActive;
            return _db.SaveChanges() > 0;
        }

        public IList<BLLBudget> GetAll()
        {
            return _db.Budgets.ToList().Select(GetBLLBudget).ToList();
        }

        public IList<BLLBudget> GetAll(int projectId)
        {
            int[] headIds = _db.ProjectBudgets.Where(pc => pc.ProjectID == projectId).Select(pcc => pcc.BudgetID).ToArray();
            return _db.Budgets.Where(h => headIds.Contains(h.ID)).Select(GetBLLBudget).ToList();
        }

        internal static DALBudget GetDALBudget(int id)
        {
            return DBFactory.Instance.DB.Budgets.Where(h => h.ID == id).SingleOrDefault();
        }

        internal static DALBudget GetDALBudget(string name)
        {
            return DBFactory.Instance.DB.Budgets.Where(h => h.Name == name).SingleOrDefault();
        }

        internal static DALBudget GetDALBudget(BLLBudget bllBudget)
        {
            return new DALBudget
                       {
                           ID = bllBudget.Id,
                           Name = bllBudget.Name,
                           Type = bllBudget.Type.ToString(),
                           Description = bllBudget.Description,
                           IsActive = bllBudget.IsActive
                       };
        }

        internal static BLLBudget GetBLLBudget(DALBudget dalBudget)
        {
            return new BLLBudget
            {
                Id = dalBudget.ID,
                Name = dalBudget.Name,
                Type = dalBudget.Type == "Capital" ? BudgetType.Capital : BudgetType.Revenue,
                Description = dalBudget.Description,
                IsActive = dalBudget.IsActive
            };
        }

        public BLLBudget Get(string headName)
        {
            DALBudget dalBudget = _db.Budgets.Where(h => h.Name == headName).SingleOrDefault();
            if (dalBudget == null) return null;
            return GetBLLBudget(dalBudget);
        }
    }
}

