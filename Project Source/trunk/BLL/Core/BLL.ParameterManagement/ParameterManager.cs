using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.Model.Entity;
using BLL.Model.Managers;
using BLL.Model.Repositories;

namespace BLL.ParameterManagement
{
    public class ParameterManager : ManagerBase, IParameterManager
    {
        private readonly IRepository<Parameter> _parameterRepository;
        public ParameterManager(IRepository<Parameter> parameterRepository)
        {
            _parameterRepository = parameterRepository;
        }

        private void Set(string key, string value)
        {
            Parameter existingParameter = _parameterRepository.GetSingle(p=>p.Key == key);
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
            Parameter existingParameter = _parameterRepository.GetSingle(p=>p.Key == key);
            return existingParameter == null ? "" : existingParameter.Value ?? "";
        }

        public DateTime GetCurrentFinantialYearStartDate()
        {
            DateTime currentFinantialYearStartDate;
            if(DateTime.TryParse(Get("CurrentFinantialYearStartDate"),out currentFinantialYearStartDate ))
            {
                return currentFinantialYearStartDate
            }
            DateTime financialYearStartDate = DateTime.Parse();
            //return new DateTime(DateTime.Now.Year, financialYearStartDate.Month, financialYearStartDate.Day);
            return DateTime.Parse("")
        }

        public string GetCurrentFinantialYear()
        {
            string currentFinantialYear = Get("CurrentFinantialYear");
            if(string.IsNullOrWhiteSpace(currentFinantialYear))
            {
                currentFinantialYear = DateTime.Now.Year.ToString();
                Parameter currentFinantialYearParameter = _parameterRepository.GetSingle(p => p.Key == "CurrentFinantialYear");
                if (currentFinantialYearParameter == null)
                {
                    currentFinantialYearParameter = new Parameter
                                                        {
                                                            Key = "CurrentFinantialYear",
                                                            Value = currentFinantialYear,
                                                            IsActive = true
                                                        };
                    _parameterRepository.Insert(currentFinantialYearParameter);
                }
                else
                {
                    
                }
                //_parameterRepository.
            }
        }


        public void SetFinancialYearStartDate(DateTime date)
        {
            Set("FinancialYearStartDate", date.ToString());
        }
    }
}
