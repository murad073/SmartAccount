using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using BLL.Factories;
using BLL.Model.Entity;
using BLL.Model.Managers;
using BLL.Test.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BLL.BudgetManagement.Test
{
    [TestClass]
    public class BudgetManagerTest
    {
        private MockRepository _mockRepository;
        private IBudgetManager _budgetManager;

        private IList<Project> _projects;
        private IList<Head> _heads;
        private string _currentFinantialYear;

        [TestInitialize]
        public void Init()
        {
            _mockRepository = new MockRepository(Guid.NewGuid().ToString());
            _mockRepository.SetRepositories();
            _budgetManager = BLLCoreFactory.GetBudgetManager();
            _currentFinantialYear = BLLCoreFactory.GetParameterManager().GetCurrentFinancialYear();

            _projects = BLLCoreFactory.ProjectRepository.GetAll().ToList();
            _heads = BLLCoreFactory.HeadRepository.GetAll().ToList();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _mockRepository.CleanUp();
        }

        [TestMethod]
        public void NoProjectHeadFoundForSelectedProjectAndHead()
        {
            //TODO: remove dependencies from EntitiesContextInitializer

            Project project = _projects.FirstOrDefault(p => p.Name == "P1");
            Head head = _heads.FirstOrDefault(h => h.Name == "h1");

            bool expected = false;
            bool actual = _budgetManager.Set(project, head, _currentFinantialYear, 90);
            Assert.AreEqual(expected, actual, "NoProjectHeadFoundForSelectedProjectAndHead");
        }
    }
}
