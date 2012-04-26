using System.Collections.Generic;
using System.Linq;
using BLL.Model.Repositories;

namespace Mock
{
    public class HeadRepository : IHeadRepository
    {
        private SmartAccountEntities _db;
        public HeadRepository()
        {
            _db = DBFactory.Instance.DB;
        }

        public Head Get(int id)
        {
            DALHead dalHead = GetDALHead(id);
            if (dalHead == null) return null;
            return GetBLLHead(dalHead);
        }

        public Head Insert(Head entity)
        {
            DALHead dalhead = GetDALHead(entity);
            _db.AddToHeads(dalhead);
            _db.SaveChanges();
            entity.Id = dalhead.ID;
            return entity;
        }

        public Head Delete(int id)
        {
            DALHead dalHead = GetDALHead(id);
            if (dalHead == null) return null;
            _db.DeleteObject(dalHead);
            _db.SaveChanges();
            return GetBLLHead(dalHead);
        }

        public bool Update(Head entity)
        {
            DALHead dalHead = GetDALHead(entity.Name);
            if (dalHead == null) return false;
            dalHead.Type = entity.Type.ToString();
            dalHead.Description = entity.Description;
            dalHead.IsActive = entity.IsActive;
            return _db.SaveChanges() > 0;
        }

        public IList<Head> GetAll()
        {
            return _db.Heads.ToList().Select(GetBLLHead).ToList();
        }

        public IList<Head> GetAll(int projectId)
        {
            int[] headIds = _db.ProjectHeads.Where(pc => pc.ProjectID == projectId).Select(pcc => pcc.HeadID).ToArray();
            return _db.Heads.Where(h => headIds.Contains(h.ID)).Select(GetBLLHead).ToList();
        }

        internal static DALHead GetDALHead(int id)
        {
            return DBFactory.Instance.DB.Heads.Where(h => h.ID == id).SingleOrDefault();
        }

        internal static DALHead GetDALHead(string name)
        {
            return DBFactory.Instance.DB.Heads.Where(h => h.Name == name).SingleOrDefault();
        }

        internal static DALHead GetDALHead(Head bllHead)
        {
            return new DALHead
                       {
                           ID = bllHead.Id,
                           Name = bllHead.Name,
                           Type = bllHead.Type.ToString(),
                           Description = bllHead.Description,
                           IsActive = bllHead.IsActive
                       };
        }

        internal static Head GetBLLHead(DALHead dalHead)
        {
            return new Head
            {
                Id = dalHead.ID,
                Name = dalHead.Name,
                Type = dalHead.Type == "Capital" ? HeadType.Capital : HeadType.Revenue,
                Description = dalHead.Description,
                IsActive = dalHead.IsActive
            };
        }

        public Head Get(string headName)
        {
            DALHead dalHead = _db.Heads.Where(h => h.Name == headName).SingleOrDefault();
            if (dalHead == null) return null;
            return GetBLLHead(dalHead);
        }
    }
}

