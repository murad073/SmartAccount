using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using BLL.Factories;
using BLL.Model.Entity;
using CodeFirst;

namespace BLL.Test.Common
{
    public class MockRepositoryFactory
    {
        public static readonly string FilePath = @"D:\My Projects\Practice self\.net projects\office Github SmartAccount\Project Source\trunk\Tests\BLL.LedgerManagement.Test\bin\Debug\SmartAccountEntities.sdf";

        static MockRepositoryFactory()
        {
            DbConnection con = new SqlCeConnection("Data Source=" + FilePath);

            BLLCoreFactory.BudgetRepository = new Repository<Budget>(con);
            BLLCoreFactory.HeadRepository = new Repository<Head>(con);
            BLLCoreFactory.ParameterRepository = new Repository<Parameter>(con);
            BLLCoreFactory.ProjectHeadRepository = new Repository<ProjectHead>(con);
            BLLCoreFactory.ProjectRepository = new Repository<Project>(con);
            BLLCoreFactory.RecordRepository = new Repository<Record>(con);
            BLLCoreFactory.BankBookRepository = new Repository<BankBook>(con);
            BLLCoreFactory.FixedAssetRepository = new Repository<FixedAsset>(con);
        }

        public static void CreateDB()
        {
            IList<Head> head = BLLCoreFactory.GetHeadManager().GetHeads();
        }
    }
}
