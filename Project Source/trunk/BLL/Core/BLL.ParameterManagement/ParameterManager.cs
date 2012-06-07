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

        private static string _currentFinancialYear;

        public ParameterManager(IRepository<Parameter> parameterRepository)
        {
            _parameterRepository = parameterRepository;
            _currentFinancialYear = Get("CurrentFinancialYear");
        }

        public void Set(string key, string value)
        {
            Parameter existingParameter = _parameterRepository.GetSingle(p => p.Key == key);
            if (existingParameter == null)
            {
                _parameterRepository.Insert(new Parameter { Key = key, Value = value, IsActive = true });
            }
            else
            {
                existingParameter.Value = value;
                _parameterRepository.Update(existingParameter);
                _parameterRepository.Save();
            }
        }

        public string Get(string key)
        {
            Parameter existingParameter = _parameterRepository.GetSingle(p => p.Key == key);
            return existingParameter == null ? "" : existingParameter.Value ?? "";
            //return "";
        }

        public DateTime GetCurrentFinancialYearStartDate()
        {
            DateTime currentFinancialYearStartDate;
            if (DateTime.TryParse(Get("CurrentFinancialYearStartDate"), out currentFinancialYearStartDate))
            {
                //return currentFinancialYearStartDate
            }
            //DateTime financialYearStartDate = DateTime.Parse();
            return new DateTime(DateTime.Now.Year, currentFinancialYearStartDate.Month, currentFinancialYearStartDate.Day);
            //return DateTime.Parse("")
        }

        public string GetCurrentFinancialYear()
        {
            return _currentFinancialYear;
        }

        public void SetCurrentFinancialYear(string currentFinancialYear)
        {
            if (string.IsNullOrWhiteSpace(currentFinancialYear) || currentFinancialYear.Length != 4)
                currentFinancialYear = "1900";

            int year;
            if (!int.TryParse(currentFinancialYear, out year)) currentFinancialYear = "1900";
            else year = int.Parse(currentFinancialYear);

            if (!(year >= 1900 && year <= 3000)) currentFinancialYear = "1900";

            Set("CurrentFinancialYear", currentFinancialYear);
            _currentFinancialYear = currentFinancialYear;
        }
    }
}
