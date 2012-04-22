using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Model;
using BLL.Model.Schema;
using DALHead = SQL2K8.Head;
using BLLHead = BLL.Model.Schema.Head;
using BLL.Model.Repositories;
using IHeadRepository = BLL.Model.Repositories.IHeadRepository;

namespace SQL2K8
{
    public class HeadRepository : IHeadRepository
    {
        private SmartAccountEntities _db;
        public HeadRepository()
        {
            _db = DBFactory.Instance.DB;
        }

        public BLLHead Get(int id)
        {
            DALHead dalHead = GetDALHead(id);
            if (dalHead == null) return null;
            return GetBLLHead(dalHead);
        }

        public BLLHead Insert(BLLHead entity)
        {
            DALHead dalhead = GetDALHead(entity);
            _db.AddToHeads(dalhead);
            _db.SaveChanges();
            entity.Id = dalhead.ID;
            return entity;
        }

        public BLLHead Delete(int id)
        {
            DALHead dalHead = GetDALHead(id);
            if (dalHead == null) return null;
            _db.DeleteObject(dalHead);
            _db.SaveChanges();
            return GetBLLHead(dalHead);
        }

        public bool Update(BLLHead entity)
        {
            DALHead dalHead = GetDALHead(entity.Name);
            if (dalHead == null) return false;
            dalHead.Type = entity.Type.ToString();
            dalHead.Description = entity.Description;
            dalHead.IsActive = entity.IsActive;
            return _db.SaveChanges() > 0;
        }

        public IList<BLLHead> GetAll()
        {
            return _db.Heads.ToList().Select(GetBLLHead).ToList();
        }

        public IList<BLLHead> GetAll(int projectId)
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

        internal static DALHead GetDALHead(BLLHead bllHead)
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

        internal static BLLHead GetBLLHead(DALHead dalHead)
        {
            return new BLLHead
            {
                Id = dalHead.ID,
                Name = dalHead.Name,
                Type = dalHead.Type == "Capital" ? HeadType.Capital : HeadType.Revenue,
                Description = dalHead.Description,
                IsActive = dalHead.IsActive
            };
        }

        public BLLHead Get(string headName)
        {
            DALHead dalHead = _db.Heads.Where(h => h.Name == headName).SingleOrDefault();
            if (dalHead == null) return null;
            return GetBLLHead(dalHead);
        }
    }
}

