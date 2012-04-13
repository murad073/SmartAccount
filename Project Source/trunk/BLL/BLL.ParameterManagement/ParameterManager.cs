using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.Model.Managers;
using BLL.Model.Repositories;
using BLL.Model.Schema;

namespace BLL.ParameterManagement
{
    public class ParameterManager : ManagerBase, IParameterManager
    {
        private IParameterRepository _parameterRepository;
        public ParameterManager(IParameterRepository parameterRepository)
        {
            _parameterRepository = parameterRepository;
        }

        private void Set(string key, string value)
        {
            Parameter existingParameter = _parameterRepository.Get(key);
            if (existingParameter == null)
            {
                _parameterRepository.Insert(new Parameter { Key = key, Value = value, IsActive = true });
            }
            else
            {
                existingParameter.Value = value;
                _parameterRepository.Update(existingParameter);
            }
        }

        private string Get(string key)
        {
            Parameter existingParameter = _parameterRepository.Get(key);
            return existingParameter == null ? "" : existingParameter.Value ?? "";
        }

        public DateTime GetFinancialYearStartDate()
        {
            DateTime financialYearStartDate = DateTime.Parse(Get("FinancialYearStartDate"));
            return new DateTime(DateTime.Now.Year, financialYearStartDate.Month, financialYearStartDate.Day);
        }

        public void SetFinancialYearStartDate(DateTime date)
        {
            Set("FinancialYearStartDate", date.ToString());
        }
    }
}
