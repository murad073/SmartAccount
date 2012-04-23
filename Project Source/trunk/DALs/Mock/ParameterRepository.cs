using System.Collections.Generic;
using BLL.Model.Repositories;
using BLL.Model.Schema;

namespace Mock
{
    public class ParameterRepository : IParameterRepository
    {
        private  readonly SmartAccountEntities _db;
        public ParameterRepository()
        {
            _db = DBFactory.Instance.DB;
        }

        public Parameter Insert(Parameter entity)
        {
            DALParameter dalParameter = GetDALParameter(entity);
            _db.AddToParameters(dalParameter);
            _db.SaveChanges();
            entity.Id = dalParameter.ID;
            return entity;
        }

        public Parameter Delete(string key)
        {
            DALParameter dalParameter = GetDALParameter(key);
            if (dalParameter == null) return null;
            _db.DeleteObject(dalParameter);
            _db.SaveChanges();
            return GetBLLParameter(dalParameter);
        }

        public bool Update(Parameter entity)
        {
            DALParameter dalParameter = GetDALParameter(entity.Key);
            if (dalParameter == null) return false;
            dalParameter.Value = entity.Value;
            dalParameter.IsActive = entity.IsActive;
            return _db.SaveChanges() > 0;
        }

        public Parameter Get(string key)
        {
            return GetBLLParameter(GetDALParameter(key));
        }

        public IList<Parameter> GetAll()
        {
            return _db.Parameters.Select(GetBLLParameter).ToList();
        }

        internal static DALParameter GetDALParameter(string key)
        {
            return DBFactory.Instance.DB.Parameters.Where(p => p.Key == key).SingleOrDefault();
        }

        internal static DALParameter GetDALParameter(Parameter bllParameter)
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

        internal static Parameter GetBLLParameter(DALParameter dalParameter)
        {
            if (dalParameter == null) return null;
            return new Parameter
                       {
                           Id = dalParameter.ID,
                           Key = dalParameter.Key,
                           Value = dalParameter.Value,
                           IsActive = dalParameter.IsActive
                       };
        }

    }
}
