using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.Model.Schema;

namespace BLL.Model.Repositories
{
    public interface IParameterRepository
    {
        Parameter Insert(Parameter entity);
        Parameter Delete(string key);
        bool Update(Parameter entity);
        Parameter Get(string key);
        IList<Parameter> GetAll();
    }
}
