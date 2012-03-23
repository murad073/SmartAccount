using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.Model.Repositories;
using BLLParameter = BLL.Model.Schema.Parameter;
using DALParameter = SQL2K8.Parameter;

namespace SQL2K8
{
    public class ParameterRepository : IParameterRepository
    {
        private  readonly SmartAccountEntities _db;
        public ParameterRepository()
        {
            _db = DBFactory.Instance.DB;
        }

        //public int Set(string key, string value)
        //{
        //    Parameter parameter = new Parameter
        //                              {
        //                                  Key = key,
        //                                  Value = value,
        //                                  IsActive = true
        //                              };
        //    _db.AddToParameters(parameter);
        //    _db.SaveChanges();
        //    return parameter.ID;
        //}

        //public string Get(string key)
        //{
        //    Parameter parameter =  _db.Parameters.Where(p => p.Key == key).SingleOrDefault();
        //    return parameter == null ? "" : parameter.Value;
        //}


        //public bool HasKey(string key)
        //{
        //    Parameter parameter = _db.Parameters.Where(p => p.Key == key).SingleOrDefault();
        //    return parameter != null;
        //}

        public BLLParameter Insert(BLLParameter entity)
        {
            DALParameter dalParameter = GetDALParameter(entity);
            _db.AddToParameters(dalParameter);
            _db.SaveChanges();
            entity.Id = dalParameter.ID;
            return entity;
        }

        public BLLParameter Delete(string key)
        {
            DALParameter dalParameter = GetDALParameter(key);
            if (dalParameter == null) return null;
            _db.DeleteObject(dalParameter);
            _db.SaveChanges();
            return GetBLLParameter(dalParameter);
        }

        public bool Update(BLLParameter entity)
        {
            DALParameter dalParameter = GetDALParameter(entity.Key);
            if (dalParameter == null) return false;
            dalParameter.Value = entity.Value;
            dalParameter.IsActive = entity.IsActive;
            return _db.SaveChanges() > 0;
        }

        public BLLParameter Get(string key)
        {
            return GetBLLParameter(GetDALParameter(key));
        }

        public IList<BLLParameter> GetAll()
        {
            return _db.Parameters.Select(GetBLLParameter).ToList();
        }

        internal static DALParameter GetDALParameter(string key)
        {
            return DBFactory.Instance.DB.Parameters.Where(p => p.Key == key).SingleOrDefault();
        }

        internal static DALParameter GetDALParameter(BLLParameter bllParameter)
        {
            if (bllParameter == null) return null;
            return new DALParameter
                       {
                           Key = bllParameter.Key,
                           Value = bllParameter.Value,
                           ID = bllParameter.Id,
                           IsActive = bllParameter.IsActive
                       };
        }

        internal static BLLParameter GetBLLParameter(DALParameter dalParameter)
        {
            if (dalParameter == null) return null;
            return new BLLParameter
                       {
                           Id = dalParameter.ID,
                           Key = dalParameter.Key,
                           Value = dalParameter.Value,
                           IsActive = dalParameter.IsActive
                       };
        }

    }
}
