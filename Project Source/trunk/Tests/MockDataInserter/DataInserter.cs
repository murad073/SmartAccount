using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using BLL.Model.Entity;
using BLL.Model.Repositories;
using CodeFirst;

namespace MockDataInserter
{
    internal class DataInserter
    {
        private readonly string _connectionString;

        #region Repository Initializer

        private readonly IRepository<BankBook> _bankBookRepository;
        private readonly IRepository<Budget> _budgetRepository;
        private readonly IRepository<FixedAsset> _fixedAssetRepository;
        private readonly IRepository<Head> _headRepository;
        private readonly IRepository<Log> _logrepository;
        private readonly IRepository<OpeningBalance> _openingBalanceRepository;
        private readonly IRepository<Parameter> _parameterReposiotry;
        private readonly IRepository<Project> _projectRepository;
        private readonly IRepository<ProjectHead> _projectHeadRepository;
        private readonly IRepository<Record> _recordRepository;

        public DataInserter(string connectionString)
        {
            _connectionString = connectionString;

            DbConnection con = new SqlCeConnection(_connectionString);

            _bankBookRepository = new Repository<BankBook>(con);
            _budgetRepository = new Repository<Budget>(con);
            _fixedAssetRepository = new Repository<FixedAsset>(con);
            _headRepository = new Repository<Head>(con);
            _logrepository = new Repository<Log>(con);
            _openingBalanceRepository = new Repository<OpeningBalance>(con);
            _parameterReposiotry = new Repository<Parameter>(con);
            _projectRepository = new Repository<Project>(con);
            _projectHeadRepository = new Repository<ProjectHead>(con);
            _recordRepository = new Repository<Record>(con);
        }
        #endregion

        public void InsertDataInDB()
        {
            _projectRepository.Insert(_projects[0]);
            _projectRepository.Insert(_projects[1]);
            _projectRepository.Insert(_projects[2]);
            _projectRepository.Insert(_projects[3]);

            _headRepository.Insert(_heads[0]);
            _headRepository.Insert(_heads[1]);
            _headRepository.Insert(_heads[2]);
        }

        private readonly IList<Project> _projects = new List<Project>
                                              {
                                                  new Project
                                                      {
                                                          Name = "P1",
                                                          CreateDate = DateTime.Now,
                                                          IsActive = true,
                                                          Description = "this is p1"
                                                      },
                                                  new Project
                                                      {
                                                          Name = "P2",
                                                          CreateDate = DateTime.Now,
                                                          IsActive = true,
                                                          Description = "this is p2"
                                                      },
                                                  new Project
                                                      {
                                                          Name = "P3",
                                                          CreateDate = DateTime.Now,
                                                          IsActive = true,
                                                          Description = "this is p3"
                                                      },
                                                  new Project
                                                      {
                                                          Name = "P4",
                                                          CreateDate = DateTime.Now,
                                                          IsActive = true,
                                                          Description = "this is p4"
                                                      }
                                              };

        private readonly IList<Head> _heads = new List<Head>
                                         {
                                             new Head
                                                 {
                                                     Name = "h1",
                                                     IsActive = true,
                                                     HeadType = "Capital",
                                                     Description = "this is h1"
                                                 },
                                             new Head
                                                 {
                                                     Name = "h2",
                                                     IsActive = true,
                                                     HeadType = "Revenue",
                                                     Description = "this is h2"
                                                 },
                                             new Head
                                                 {
                                                     Name = "h3",
                                                     IsActive = true,
                                                     HeadType = "Capital",
                                                     Description = "this is h3"
                                                 }
                                         };
    }
}
