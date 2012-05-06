using System;
using System.Collections.Generic;
using BLL.Model.Entity;

namespace Mock
{
    public class MockEntities
    {
        public IList<ProjectHead> ProjectHeads = new List<ProjectHead>
                                                       {

                                                       };

        public IList<Record> Records = new List<Record>
                                             {

                                             };


        public IList<BankRecord> BankRecords = new List<BankRecord>
                                                   {

                                                   };

        public IList<Budget> Budgets = new List<Budget>
                                           {

                                           };

        public IList<FixedAsset> FixedAssets = new List<FixedAsset>
                                                   {

                                                   };

        public IList<Head> Heads = new List<Head>
                                       {

                                       };
        public IList<Log> Logs = new List<Log>
                                     {

                                     };
        public IList<OpeningBalance> OpeningBalances = new List<OpeningBalance>
                                                           {

                                                           };

        public IList<Parameter> Parameters = new List<Parameter>
                                                 {

                                                 };
        public IList<Project> Projects = new List<Project> { };

        public IList<T> GetTable<T>()
        {
            Type objType = typeof(T);
            if (objType.IsEquivalentTo(typeof(ProjectHead))) return ProjectHeads as IList<T>;
            else if (objType.IsEquivalentTo(typeof(Project))) return Projects as IList<T>;
            else if (objType.IsEquivalentTo(typeof(Head))) return Heads as IList<T>;


            return null;
        }
    }


}
