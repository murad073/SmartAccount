using System;
using System.Collections.Generic;

namespace Mock
{
    public class SmartAccountEntities
    {
        private IList<ProjectHead> _projectHeads = new List<ProjectHead>
                        {
                            new ProjectHead{ID = 1, HeadID = 1, IsActive = true, ProjectID = 1},
                            new ProjectHead{ID = 2, HeadID = 2, IsActive = true, ProjectID = 1},
                            new ProjectHead{ID = 3, HeadID = 3, IsActive = true, ProjectID = 1},
                            new ProjectHead{ID = 4, HeadID = 4, IsActive = true, ProjectID = 1}
                        };
        public IList<ProjectHead> ProjectHeads
        {
            get { return _projectHeads; }
        }

        private IList<Record> _records = new List<Record>
                        {
                            new Record{ID = 1, IsActive = true, LedgerType = "CV"},
                            new Record{ID = 2, IsActive = true, LedgerType = "CV"},
                            new Record{ID = 3, IsActive = true, LedgerType = "CV"},
                            new Record{ID = 4, IsActive = true, LedgerType = "CV"}
                        };
        public IList<Record> Records { get { return _records; } }
    }

    public class BankRecord
    {
        public int ID { get; set; }
        public int RecordID { get; set; }
        public string ChequeNo { get; set; }
        public string BankName { get; set; }
        public string Branch { get; set; }
        public DateTime ChequeDate { get; set; }
    }

    public class Budget
    {
        public int ID { get; set; }
        public int? ProjectHeadID { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public string Note { get; set; }
        public bool? IsActive { get; set; }
    }

    public class FixedAsset
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double? DepreciationRate { get; set; }
        public int? DepreciatedValue { get; set; }
        public string DepreciationType { get; set; }
        public bool? ByForceDisposed { get; set; }
        public int? RecordID { get; set; }
    }

    public class Head
    {
        public int ID { get; set; }
        public int? ParentID { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
    }

    public class Log
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string UserName { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
    }

    public class OpeningBalance
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public int? ProjectHeadID { get; set; }
        public int? Balance { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
    }

    public class Parameter
    {
        public int ID { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public bool IsActive { get; set; }
    }

    public class Project
    {
        public int ID { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public string Description { get; set; }
    }

    public class ProjectHead
    {
        public int ID { get; set; }
        public int ProjectID { get; set; }
        public int HeadID { get; set; }
        public bool IsActive { get; set; }
    }

    public class Record
    {
        public int ID { get; set; }
        public int ProjectHeadID { get; set; }
        public DateTime Date { get; set; }
        public string VoucherType { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
        public string Narration { get; set; }
        public string LedgerType { get; set; }
        public int VoucherSerialNo { get; set; }
        public string Link { get; set; }
        public string Tag { get; set; }
        public bool IsActive { get; set; }
    }
}
