using System;
using System.Collections.Generic;
using BLL.Model.Repositories;

namespace BLL.Model.Entity
{
    //public abstract class Record
    //{
    //    internal IRecordRepository RecordRepository;

    //    protected Record(IRecordRepository recordRepository)
    //    {
    //        RecordRepository = recordRepository;
    //    }

    //    public int VoucherSerialNo { get; set; }
    //    public string ProjectName { get; set; }
    //    public DateTime Date { get; set; }
    //    public string Narration { get; set; }
    //    public string LinkedVoucherNo { get; set; }
    //    public string Tag { get; set; }

    //    public virtual string VoucherTypeKey { get; set; }
    //    public virtual string HeadName { get; set; }
    //    public virtual double Debit { get; set; }
    //    public virtual double Credit { get; set; }

    //    public abstract string LedgerType { get; }
    //    public abstract bool Save();
    //    public abstract bool IsValid();

    //    public string VoucherNo
    //    {
    //        get { return VoucherNumberHelper.GetVoucherNumber(VoucherTypeKey, VoucherSerialNo); }
    //    }
    //    public double Balance
    //    {
    //        get { return Debit - Credit; }
    //    }
    //}

    public class Record
    {
        public int ID { get; set; }
        //public int ProjectHeadID { get; set; }
        public DateTime Date { get; set; }
        public string VoucherType { get; set; }
        public virtual double Debit { get; set; }
        public virtual double Credit { get; set; }
        public string Narration { get; set; }
        public virtual string LedgerType { get; set; }
        public int VoucherSerialNo { get; set; }
        public string Link { get; set; }
        public string Tag { get; set; }
        public bool IsActive { get; set; }

        public virtual ProjectHead ProjectHead { get; set; }
        public virtual ICollection<FixedAsset> FixedAssets { get; set; }
        public virtual ICollection<BankRecord> BankRecords { get; set; }

        internal IRepository<Record> RecordRepository;
        
        public virtual bool Save() { return false; }
        public virtual string VoucherTypeKey() { return ""; }
        public virtual string HeadName() { return ""; }
    }
}

