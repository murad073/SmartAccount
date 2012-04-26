using System;
using System.Collections.Generic;

namespace Mock
{
    public class SmartAccountEntities
    {
        private IList<ProjectHead> _projectHeads = new List<ProjectHead>
                                                       {
                                                           new ProjectHead
                                                               {ID = 244, ProjectID = 24, HeadID = 52, IsActive = true},
                                                           new ProjectHead
                                                               {ID = 245, ProjectID = 24, HeadID = 51, IsActive = true},
                                                           new ProjectHead
                                                               {ID = 246, ProjectID = 25, HeadID = 52, IsActive = true},
                                                           new ProjectHead
                                                               {ID = 247, ProjectID = 25, HeadID = 51, IsActive = true},
                                                           new ProjectHead
                                                               {ID = 261, ProjectID = 24, HeadID = 69, IsActive = true},
                                                           new ProjectHead
                                                               {ID = 262, ProjectID = 25, HeadID = 69, IsActive = true},
                                                           new ProjectHead
                                                               {ID = 263, ProjectID = 29, HeadID = 52, IsActive = true},
                                                           new ProjectHead
                                                               {ID = 264, ProjectID = 29, HeadID = 51, IsActive = true},
                                                           new ProjectHead
                                                               {ID = 265, ProjectID = 29, HeadID = 72, IsActive = true},
                                                           new ProjectHead
                                                               {ID = 266, ProjectID = 29, HeadID = 71, IsActive = true},
                                                           new ProjectHead
                                                               {ID = 267, ProjectID = 29, HeadID = 69, IsActive = true},
                                                           new ProjectHead
                                                               {ID = 268, ProjectID = 29, HeadID = 73, IsActive = true},
                                                       };
        public IList<ProjectHead> ProjectHeads
        {
            get { return _projectHeads; }
        }

        private IList<Record> _records = new List<Record>
                                             {
                                                 new Record
                                                     {
                                                         ID = 160,
                                                         ProjectHeadID = 261,
                                                         Date = DateTime.Parse("2012-04-04 19:46:48.430"),
                                                         VoucherType = "DV",
                                                         Debit = 1000,
                                                         Credit = 0,
                                                         Narration = "",
                                                         LedgerType = "LedgerBook",
                                                         VoucherSerialNo = 1,
                                                         Link = "",
                                                         Tag = "",
                                                         IsActive = true
                                                     },
                                                 new Record
                                                     {
                                                         ID = 161,
                                                         ProjectHeadID = 244,
                                                         Date = DateTime.Parse("2012-04-04 19:46:48.430"),
                                                         VoucherType = "DV",
                                                         Debit = 0,
                                                         Credit = 1000,
                                                         Narration = "",
                                                         LedgerType = "CashBook",
                                                         VoucherSerialNo = 1,
                                                         Link = "",
                                                         Tag = "",
                                                         IsActive = true
                                                     },
                                                 new Record
                                                     {
                                                         ID = 162,
                                                         ProjectHeadID = 267,
                                                         Date = DateTime.Parse("2012-04-04 20:59:22.333"),
                                                         VoucherType = "DV",
                                                         Debit = 10000,
                                                         Credit = 0,
                                                         Narration = "",
                                                         LedgerType = "LedgerBook",
                                                         VoucherSerialNo = 1,
                                                         Link = "",
                                                         Tag = "Advance",
                                                         IsActive = true
                                                     },
                                                 new Record
                                                     {
                                                         ID = 163,
                                                         ProjectHeadID = 263,
                                                         Date = DateTime.Parse("2012-04-04 20:59:22.333"),
                                                         VoucherType = "DV",
                                                         Debit = 0,
                                                         Credit = 10000,
                                                         Narration = "",
                                                         LedgerType = "CashBook",
                                                         VoucherSerialNo = 1,
                                                         Link = "",
                                                         Tag = "Advance",
                                                         IsActive = true
                                                     },
                                                 new Record
                                                     {
                                                         ID = 164,
                                                         ProjectHeadID = 267,
                                                         Date = DateTime.Parse("2012-04-04 21:00:00.180"),
                                                         VoucherType = "CV",
                                                         Debit = 0,
                                                         Credit = 2000,
                                                         Narration = "cash back",
                                                         LedgerType = "LedgerBook",
                                                         VoucherSerialNo = 1,
                                                         Link = "",
                                                         Tag = "",
                                                         IsActive = true
                                                     },
                                                 new Record
                                                     {
                                                         ID = 165,
                                                         ProjectHeadID = 263,
                                                         Date = DateTime.Parse("2012-04-04 21:00:00.180"),
                                                         VoucherType = "CV",
                                                         Debit = 2000,
                                                         Credit = 0,
                                                         Narration = "cash back",
                                                         LedgerType = "CashBook",
                                                         VoucherSerialNo = 1,
                                                         Link = "",
                                                         Tag = "",
                                                         IsActive = true
                                                     },
                                                 new Record
                                                     {
                                                         ID = 166,
                                                         ProjectHeadID = 267,
                                                         Date = DateTime.Parse("2012-04-04 21:00:54.807"),
                                                         VoucherType = "JV",
                                                         Debit = 0,
                                                         Credit = 8000,
                                                         Narration = "",
                                                         LedgerType = "LedgerBook",
                                                         VoucherSerialNo = 1,
                                                         Link = "",
                                                         Tag = "",
                                                         IsActive = true
                                                     },
                                                 new Record
                                                     {
                                                         ID = 167,
                                                         ProjectHeadID = 265,
                                                         Date = DateTime.Parse("2012-04-04 21:00:54.807"),
                                                         VoucherType = "JV",
                                                         Debit = 2000,
                                                         Credit = 0,
                                                         Narration = "",
                                                         LedgerType = "LedgerBook",
                                                         VoucherSerialNo = 1,
                                                         Link = "",
                                                         Tag = "",
                                                         IsActive = true
                                                     },
                                                 new Record
                                                     {
                                                         ID = 168,
                                                         ProjectHeadID = 266,
                                                         Date = DateTime.Parse("2012-04-04 21:00:54.807"),
                                                         VoucherType = "JV",
                                                         Debit = 6000,
                                                         Credit = 0,
                                                         Narration = "",
                                                         LedgerType = "LedgerBook",
                                                         VoucherSerialNo = 1,
                                                         Link = "",
                                                         Tag = "",
                                                         IsActive = true
                                                     },
                                                 new Record
                                                     {
                                                         ID = 169,
                                                         ProjectHeadID = 266,
                                                         Date = DateTime.Parse("2012-04-04 21:21:53.417"),
                                                         VoucherType = "DV",
                                                         Debit = 3334,
                                                         Credit = 0,
                                                         Narration = "",
                                                         LedgerType = "LedgerBook",
                                                         VoucherSerialNo = 2,
                                                         Link = "",
                                                         Tag = "",
                                                         IsActive = true
                                                     },
                                                 new Record
                                                     {
                                                         ID = 170,
                                                         ProjectHeadID = 263,
                                                         Date = DateTime.Parse("2012-04-04 21:21:53.417"),
                                                         VoucherType = "DV",
                                                         Debit = 0,
                                                         Credit = 3334,
                                                         Narration = "",
                                                         LedgerType = "CashBook",
                                                         VoucherSerialNo = 2,
                                                         Link = "",
                                                         Tag = "",
                                                         IsActive = true
                                                     },
                                                 new Record
                                                     {
                                                         ID = 171,
                                                         ProjectHeadID = 267,
                                                         Date = DateTime.Parse("2012-04-04 21:22:30.407"),
                                                         VoucherType = "JV",
                                                         Debit = 0,
                                                         Credit = 2000,
                                                         Narration = "",
                                                         LedgerType = "LedgerBook",
                                                         VoucherSerialNo = 2,
                                                         Link = "",
                                                         Tag = "",
                                                         IsActive = true
                                                     },
                                                 new Record
                                                     {
                                                         ID = 172,
                                                         ProjectHeadID = 266,
                                                         Date = DateTime.Parse("2012-04-04 21:22:30.407"),
                                                         VoucherType = "JV",
                                                         Debit = 1000,
                                                         Credit = 0,
                                                         Narration = "",
                                                         LedgerType = "LedgerBook",
                                                         VoucherSerialNo = 2,
                                                         Link = "",
                                                         Tag = "",
                                                         IsActive = true
                                                     },
                                                 new Record
                                                     {
                                                         ID = 173,
                                                         ProjectHeadID = 268,
                                                         Date = DateTime.Parse("2012-04-04 21:22:30.407"),
                                                         VoucherType = "JV",
                                                         Debit = 1000,
                                                         Credit = 0,
                                                         Narration = "",
                                                         LedgerType = "LedgerBook",
                                                         VoucherSerialNo = 2,
                                                         Link = "",
                                                         Tag = "",
                                                         IsActive = true
                                                     }
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
