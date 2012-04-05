using System.Collections.Generic;
using System.Linq;
using System.Data.Common;
using BLL.Model.Schema;
using BLL.Model.Repositories;


namespace SQL2K8
{
    public class RecordRepository : IRecordRepository
    {
        private SmartAccountEntities _db = new SmartAccountEntities();
        private DbTransaction _transaction;
        public int GetMaxVoucherNo(string voucherType, int projectId)
        {
            int[] projectHeadIds = _db.ProjectHeads.Where(ph => ph.ProjectID == projectId).Select(ph => ph.ID).ToArray();
            // TODO: remove ToList() function from Records, this will be much more time consuming
            IList<Record> records = _db.Records.Where(r=>projectHeadIds.Contains(r.ProjectHeadID) && r.VoucherType == voucherType).ToList();
            return records.Count == 0 ? 0 : records.Max(r => r.VoucherSerialNo);
        }

        private int GetProjectHeadId(string projectName, string headName)
        {
            //TODO: Validation for combobox selection
            int projectId = _db.Projects.Where(p => p.Name == projectName).SingleOrDefault().ID;
            int headId = _db.Heads.Where(h => h.Name == headName).SingleOrDefault().ID;
            ProjectHead pc = _db.ProjectHeads.Where(pcc => pcc.ProjectID == projectId && pcc.HeadID == headId).SingleOrDefault();
            return pc.ID;
        }

        //public int InsertLedgerBookRow(BLL.Model.Record.Record record, string recordType = "LedgerBook")
        //{
        //    Record dalRecord = new Record { Type = recordType, ProjectHeadID = GetProjectHeadId(record.ProjectId, record.HeadId), Date = record.Date, VoucherType = record.VoucherType, VoucherSerialNo = record.VoucherSerialNo, Debit = record.Debit, Credit = record.Credit, Narration = record.Description };
        //    db.AddToRecords(dalRecord);
        //    db.SaveChanges();

        //    return dalRecord.ID;
        //}

        //public int InsertCashBookRow(BLL.Model.Record.Record record)
        //{
        //    Record dalRecord = new Record { Type = "CashBook", ProjectHeadID = GetProjectHeadId(record.ProjectId, record.HeadId), Date = record.Date, VoucherType = record.VoucherType, VoucherSerialNo = record.VoucherSerialNo, Debit = record.Debit, Credit = record.Credit, Narration = record.Description };
        //    db.AddToRecords(dalRecord);
        //    db.SaveChanges();

        //    return dalRecord.ID;
        //}

        //public int InsertBankBookRow(BLL.Model.Record.BankRecord record)
        //{
        //    int recordId = InsertLedgerBookRow(record, "BankBook");

        //    BankRecord dalBankRecord = new BankRecord { RecordID = recordId, ChequeNo = record.ChequeNo };
        //    db.AddToBankRecords(dalBankRecord);
        //    db.SaveChanges();

        //    return dalBankRecord.ID;
        //}

        //public int InsertFixedAssetRow(FixedAssetRecord record)
        //{
        //    int recordId = InsertLedgerBookRow(record);

        //    FixedAsset dalFixedAssetRecord = new FixedAsset { RecordID = recordId, Name = record.FixedAssetName, DepreciationRate = record.DepreciationRate };
        //    db.AddToFixedAssets(dalFixedAssetRecord);
        //    db.SaveChanges();

        //    return dalFixedAssetRecord.ID;
        //}

        public bool IsRecordFound(string key, int serialNo)
        {
            Record record = _db.Records.Where(r => r.VoucherType == key && r.VoucherSerialNo == serialNo).SingleOrDefault();
            return record != null;
        }

        public bool IsRecordFound(int projectId, int headId)
        {
            ProjectHead projectHead =
                _db.ProjectHeads.Where(ph => ph.ProjectID == projectId && ph.HeadID == headId).SingleOrDefault();
            if (projectHead != null)
            {
                return _db.Records.Where(r => r.ProjectHeadID == projectHead.ID).Count() > 0;
            }
            return false;
        }

        private int InsertLedgerRecord(BLL.Model.Schema.Record record)
        {
            Record dalRecord = new Record
            {
                ProjectHeadID = GetProjectHeadId(record.ProjectName, record.HeadName),
                Date = record.Date,
                VoucherType = record.VoucherTypeKey,
                Debit = record.Debit,
                Credit = record.Credit,
                Narration = record.Narration,
                LedgerType = record.LedgerType,
                VoucherSerialNo = record.VoucherSerialNo,
                Link = record.LinkedVoucherNo,
                Tag = record.Tag,
                IsActive = true
            };
            _db.AddToRecords(dalRecord);
            _db.SaveChanges();
            return dalRecord.ID;
        }

        public bool InsertLedgerBookRow(VoucherBase record)
        {
            return InsertLedgerRecord(record) > 0;
        }

        public bool InsertLedgerBookRow(VoucherBase record, BLL.Model.Schema.FixedAsset fixedAsset)
        {
            int recordId = InsertLedgerRecord(record);
            if (recordId > 0)
            {
                FixedAsset dalFixedAsset = new FixedAsset
                                            {
                                                Name = fixedAsset.Name,
                                                DepreciationRate = fixedAsset.DepreciationRate,
                                                RecordID = recordId
                                            };
                _db.AddToFixedAssets(dalFixedAsset);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool InsertCashBookRow(TransactionInCash cashRecord)
        {
            return InsertLedgerRecord(cashRecord) > 0;
        }

        public bool InsertBankBookRow(TransactionInCheque chequeRecord)
        {
            int recordId = InsertLedgerRecord(chequeRecord);
            if (recordId > 0)
            {
                ChequeInfo chequeInfo = chequeRecord.ChequeInfo;
                BankRecord dalBankRecord = chequeInfo == null
                                               ? new BankRecord
                                                     {
                                                         RecordID = recordId
                                                     }
                                               : new BankRecord
                                                     {
                                                         RecordID = recordId,
                                                         ChequeNo = chequeRecord.ChequeInfo.ChequeNo,
                                                         BankName = chequeRecord.ChequeInfo.BankName,
                                                         Branch = chequeRecord.ChequeInfo.BankBranch,
                                                         ChequeDate = chequeRecord.ChequeInfo.Date
                                                     };
                _db.AddToBankRecords(dalBankRecord);
                _db.SaveChanges();
                return true;
            }
            return false;
        }


        public bool BeginTransaction()
        {
            _db.Connection.Open();
            _transaction = _db.Connection.BeginTransaction();
            return _transaction != null;
        }

        public bool CommitTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Commit();
                _transaction.Dispose();
                _db.Connection.Close();
                return true;
            }
            return false;
        }

        public bool RollbackTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction.Dispose();
                _db.Connection.Close();
                return true;
            }
            return false;
        }
    }
}
