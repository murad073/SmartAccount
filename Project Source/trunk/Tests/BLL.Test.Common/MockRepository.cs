using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using BLL.Factories;
using BLL.Model.Entity;
using CodeFirst;
using System.IO;

namespace BLL.Test.Common
{
    public class MockRepository
    {
        public const string FilePath = @"D:\My Projects\Practice self\.net projects\office Github SmartAccount\Project Source\trunk\Tests\TestDatabase\$TestName$\SmartAccountEntities.sdf";
        private readonly FileInfo _fullPath;

        public MockRepository(string testName)
        {
            //_fullPath = FilePath.Replace("$TestName$", testName);
            _fullPath = new FileInfo(FilePath.Replace("$TestName$", testName));
            if (!Directory.Exists(_fullPath.DirectoryName)) Directory.CreateDirectory(_fullPath.DirectoryName);
        }

        public void SetRepositories()
        {
            DbConnection con = new SqlCeConnection("Data Source=" + _fullPath.FullName);

            BLLCoreFactory.BudgetRepository = new Repository<Budget>(con);
            BLLCoreFactory.HeadRepository = new Repository<Head>(con);
            BLLCoreFactory.ParameterRepository = new Repository<Parameter>(con);
            BLLCoreFactory.ProjectHeadRepository = new Repository<ProjectHead>(con);
            BLLCoreFactory.ProjectRepository = new Repository<Project>(con);
            BLLCoreFactory.RecordRepository = new Repository<Record>(con);
            BLLCoreFactory.BankBookRepository = new Repository<BankBook>(con);
            BLLCoreFactory.FixedAssetRepository = new Repository<FixedAsset>(con);

            BLLCoreFactory.GetHeadManager().GetHeads();
        }

        public void CleanUp()
        {
            //File.Delete(_fullPath);
            Directory.Delete(_fullPath.DirectoryName, true);
        }
    }
}
