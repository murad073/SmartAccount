using System.Data;
using System.Data.Common;
using BLL.LedgerManagement;
using CodeFirst;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BLL.Model.Entity;
using BLL.Model.Repositories;
using BLL.Model.Managers;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlServerCe;
namespace BLL.LedgerManagement.Test
{
    [TestClass]
    public class LedgerManagerTest
    {
        private const string ConnectionString = @"Data Source=D:\My Projects\Practice self\.net projects\office Github SmartAccount\Project Source\trunk\Tests\BLL.LedgerManagement.Test\bin\Debug\SmartAccountEntities.sdf";

        private readonly LedgerManager _ledgerManager;
        private IRepository<Project> _projectRepository;

        public LedgerManagerTest()
        {
            //DbProviderFactories.GetFactory("System.Data.SqlServerCe.4.0")

            DbConnection con = new SqlCeConnection(ConnectionString);

            _projectRepository = new Repository<Project>(con);
            var recordRepository = new Repository<Record>(con);
            var d = recordRepository.GetAll().ToList();
            var parmRepo = new Repository<Parameter>(con);
            var pManager = new BLL.ParameterManagement.ParameterManager(parmRepo);
            _ledgerManager = new LedgerManager(recordRepository, pManager);
        }


        [TestInitialize]
        public void Init()
        {

            //var initializer = new SmartAccountContext();
            //System.Data.Entity.Database.SetInitializer(initializer);

            //_dbContext = ContainerFactory.Container.GetInstance<IContext>();
            //initializer.InitializeDatabase((MyTestContext)_dbContext);

            //_testConnection = _dbContext.ConnectionString;
        }

        [TestCleanup]
        public void Cleanup()
        {
            
            //System.Data.Entity.Database.Delete(_testConnection);

            //_dbContext.Dispose();
        }


        [TestMethod]
        public void ValidateReturnsFalseProjectNull()
        {
            Project p = null;
            Head h = null;
            var actual = _ledgerManager.Validate(p, h, false);
            Assert.AreEqual(false, actual, "Should return false when project is null");
        }

        [TestMethod]
        public void ValidateReturnsFalseHeadNullShowlAllAdvavceFalse()
        {
            var p = new Project();
            Head h = null;
            var actual = _ledgerManager.Validate(p, h, false);
            Assert.AreEqual(false, actual, "Should return false when head is null and show all advance is false");
        }

        [TestMethod]
        public void ValidateReturnsTrueProjectNotNullHeadNotNull()
        {
            var p = new Project();
            var h = new Head();
            var actual = _ledgerManager.Validate(p, h, false);
            Assert.AreEqual(true, actual, "Shoild return true when  project and head is not null");
        }

        [TestMethod]
        public void ValidateTrueProjectNotNullHeadNullShowAllAdvanceTrue()
        {
            var p = new Project();
            Head h = null;
            var actual = _ledgerManager.Validate(p, h, true);
            Assert.AreEqual(true, actual, "Shoild return true when head null but show advance is true");
        }

        [TestMethod]
        public void GetAllAdvance()
        {
            var p = _projectRepository.Get(1);
            var list = _ledgerManager.GetAllAdvance(p);
            Assert.AreEqual(1, list.Count, "Only one record in the test database");
            Assert.AreEqual(2333, list.First().Debit, "Debit is 2333 in test database");
        }
    }
}


