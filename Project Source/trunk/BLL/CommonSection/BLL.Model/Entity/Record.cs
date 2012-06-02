using System;
using System.Collections.Generic;
using BLL.Model.Repositories;
using System.ComponentModel.DataAnnotations;

namespace BLL.Model.Entity
{
    public class Record
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string VoucherType { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
        public string Narration { get; set; }
        public virtual string LedgerType { get; set; }
        public int VoucherSerialNo { get; set; }
        public string Link { get; set; }
        public string Tag { get; set; }
        public bool IsActive { get; set; }
        public string FinancialYear { get; set; }

        [Required]
        public virtual ProjectHead ProjectHead { get; set; }

        public virtual FixedAsset FixedAsset { get; set; }
        public virtual BankBook BankBook { get; set; }

        internal IRepository<Record> RecordRepository;

        public virtual void SetAmount(double amount) { }
        public virtual bool Save() { return false; }
        public virtual string VoucherTypeKey() { return ""; }
        public virtual string HeadName() { return ProjectHead.Head.Name; }

    }
}

