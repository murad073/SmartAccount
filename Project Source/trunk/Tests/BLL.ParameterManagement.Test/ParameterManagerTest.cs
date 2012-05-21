using System;
using System.Data.Common;
using System.Data.SqlServerCe;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using BLL.Factories;
using BLL.Model.Entity;
using BLL.Model.Managers;
using BLL.Model.Repositories;
using BLL.Test.Common;
using CodeFirst;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BLL.ParameterManagement.Test
{
    [TestClass]
    public class ParameterManagerTest
    {
        private IParameterManager _parameterManager;

        [TestInitialize]
        public void Init()
        {
            MockRepositoryFactory.CreateDB();
            _parameterManager = BLLCoreFactory.GetParameterManager();
        }

        [TestCleanup]
        public void Cleanup()
        {
            File.Delete(MockRepositoryFactory.FilePath);
        }


        [TestMethod]
        public void GetCurrentFinantialYearWillBeTodaysYear()
        {
            string expected = "2012";
            string actual = _parameterManager.GetCurrentFinantialYear();
            Assert.AreEqual(expected, actual, "Current finantial year will be create year. ie today");
        }

        [TestMethod]
        public void GetCurrentFinantialYearStartDateWillBeNow()
        {
            DateTime expected = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DateTime date = _parameterManager.GetCurrentFinantialYearStartDate();
            DateTime actual = new DateTime(date.Year, date.Month, date.Day);
            Assert.AreEqual(expected, actual, "Current finantial year start date will be today");
        }

        [TestMethod]
        public void GetCurrentFinantialYearAfterSettingCurrentFinantialYear()
        {
            var inputYears = new [] {     "",     null,   "2000", "2012", "",    "3014",  "1",    "abc", "1900", "3000", "1200", "2999" };
            var expectedValues = new [] { "1900", "1900", "2000", "2012", "1900", "1900", "1900", "1900", "1900", "3000", "1900", "2999" };

            for (int i = 0; i < inputYears.Count(); i++)
            {
                _parameterManager.SetCurrentFinancialYear(inputYears[i]);

                string expected = expectedValues[i];
                string actual = _parameterManager.GetCurrentFinantialYear();
                Assert.AreEqual(expected, actual, "After setting finantial year to the database, we must get finantial year when we call for it.");
                //Finantial year must be between 1900-3000 AD
            }

        }
    }
}
