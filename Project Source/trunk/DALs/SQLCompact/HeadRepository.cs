using System.Collections.Generic;
using System.Linq;
using DALHead = SQLCompact.Head;
using IHeadRepository = BLL.Model.Repositories.IHeadRepository;

namespace SQLCompact
{
    public class HeadRepository : IHeadRepository
    {
        private SQLCEEntities _db;
        public HeadRepository()
        {
            _db = SQLCEFactory.Instance.DB;
        }

        public BLL.Model.Entity.Head Get(int id)
        {
            DALHead dalHead = GetDALHead(id);
            if (dalHead == null) return null;
            return GetBLLHead(dalHead);
        }

        public BLL.Model.Entity.Head Insert(BLL.Model.Entity.Head entity)
        {
            DALHead dalhead = GetDALHead(entity);
            _db.AddToHeads(dalhead);
            _db.SaveChanges();
            entity.Id = dalhead.ID;
            return entity;
        }

        public BLL.Model.Entity.Head Delete(int id)
        {
            DALHead dalHead = GetDALHead(id);
            if (dalHead == null) return null;
            _db.DeleteObject(dalHead);
            _db.SaveChanges();
            return GetBLLHead(dalHead);
        }

        public bool Update(BLL.Model.Entity.Head entity)
        {
            DALHead dalHead = GetDALHead(entity.Name);
            if (dalHead == null) return false;
            //dalHead.Name = entity.Name;
            dalHead.Type = entity.Type.ToString();
            dalHead.Description = entity.Description;
            dalHead.IsActive = entity.IsActive;
            return _db.SaveChanges() > 0;
        }

        public IList<BLL.Model.Entity.Head> GetAll()
        {
            return _db.Heads.ToList().Select(GetBLLHead).ToList();
        }

        public IList<BLL.Model.Entity.Head> GetAll(int projectId)
        {
            int[] headIds = _db.ProjectHeads.Where(pc => pc.ProjectID == projectId).Select(pcc => pcc.HeadID).ToArray();
            return _db.Heads.Where(h => headIds.Contains(h.ID)).Select(GetBLLHead).ToList();
        }

        internal static DALHead GetDALHead(int id)
        {
            return SQLCEFactory.Instance.DB.Heads.Where(h => h.ID == id).SingleOrDefault();
        }

        internal static DALHead GetDALHead(string name)
        {
            return SQLCEFactory.Instance.DB.Heads.Where(h => h.Name == name).SingleOrDefault();
        }

        internal static DALHead GetDALHead(BLL.Model.Entity.Head bllHead)
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

        internal static BLL.Model.Entity.Head GetBLLHead(DALHead dalHead)
        {
            return new BLL.Model.Entity.Head
            {
                Id = dalHead.ID,
                Name = dalHead.Name,
                Type = dalHead.Type == "Capital" ? HeadType.Capital : HeadType.Revenue,
                Description = dalHead.Description,
                IsActive = dalHead.IsActive
            };
        }

        public BLL.Model.Entity.Head Get(string headName)
        {
            DALHead dalHead = _db.Heads.Where(h => h.Name == headName).SingleOrDefault();
            if (dalHead == null) return null;
            return GetBLLHead(dalHead);
        }
    }
}

