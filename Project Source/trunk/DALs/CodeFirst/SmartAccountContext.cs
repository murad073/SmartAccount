using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using BLL.Model.Entity;

namespace CodeFirst
{
    internal class SmartAccountContext : DbContext
    {
        private SmartAccountContext()
        {
            Database.SetInitializer(new EntitiesContextInitializer());
        }
        //public SmartAccountContext(string connectionString) : base(connectionString)
        //{
        //    Database.SetInitializer(new EntitiesContextInitializer());
        //}
        private SmartAccountContext(DbConnection dbConnection)
            : base(dbConnection, true)
        {
            Database.SetInitializer(new EntitiesContextInitializer());
        }

        public static DbConnection DBConnection { get; set; }

        private static SmartAccountContext _instance;
        public static SmartAccountContext Instance
        {
            get
            {
                return _instance ??
                       (_instance =
                        DBConnection == null ? new SmartAccountContext() : new SmartAccountContext(DBConnection));
            }
        }

        public void Reset()
        {
            //_instance.Dispose(true);
            _instance = new SmartAccountContext();
        }

        public DbSet<BankBook> BankBooks { get; set; }

        public DbSet<Budget> Budgets { get; set; }

        public DbSet<FixedAsset> FixedAssets { get; set; }

        public DbSet<Head> Heads { get; set; }

        public DbSet<Log> Logs { get; set; }

        public DbSet<OpeningBalance> OpeningBalances { get; set; }

        public DbSet<Parameter> Parameters { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<ProjectHead> ProjectHeads { get; set; }

        public DbSet<Record> Records { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
