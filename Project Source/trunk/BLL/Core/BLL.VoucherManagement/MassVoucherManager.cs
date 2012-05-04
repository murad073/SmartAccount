using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Model;
using BLL.Model.Entity;
using BLL.Model.Managers;
using BLL.Model.Repositories;
using BLL.Model.Schema;

namespace BLL.VoucherManagement
{
    public class MassVoucherManager : ManagerBase, IMassVoucherManager
    {
        private readonly IRepository<Head> _headRepository;
        private readonly IRepository<Project> _projectRepository;
        private readonly IRepository<Record> _recordRepository;
        private readonly IRepository<FixedAsset> _fixedAssetRepository;
        private readonly IRepository<ProjectHead> _projectHeadRepository;
        private readonly IRepository<BankRecord> _bankRecordRepository;
        private ProjectHead _projectHead;

        private MassVoucher _massVoucher;

        private IList<Record> _entryableRecords;

        public MassVoucherManager(IRepository<Record> recordRepository, IRepository<Project> projectRepository,
                                  IRepository<Head> headRepository, IRepository<ProjectHead> projectHeadRepository, IRepository<FixedAsset> fixedAssetRepository, IRepository<BankRecord> bankRecordRepository)
        {
            _recordRepository = recordRepository;
            _bankRecordRepository = bankRecordRepository;
            _projectRepository = projectRepository;
            _headRepository = headRepository;
            _projectHeadRepository = projectHeadRepository;
            _fixedAssetRepository = fixedAssetRepository;
        }

        public override string ModuleName
        {
            get { return "MassVoucherManager"; }
        }

        public bool Set(MassVoucher massVoucher)
        {
            bool isValid = true;
            if (massVoucher.Project == null)
            {
                isValid = SetErrorMessage("NoProjectSelected");
            }
            else if (massVoucher.VoucherType != "Contra" && massVoucher.Head == null)
            {
                isValid = SetErrorMessage("NoHeadSelected");
            }
            else if (massVoucher.Amount == 0)
            {
                isValid = SetErrorMessage("AmountCannotBeZero");
            }
            else if (massVoucher.VoucherType == "Contra" && string.IsNullOrWhiteSpace(massVoucher.ContraType))
            {
                isValid = SetErrorMessage("ContraTypeIsNotSelected");
            }
            else if (massVoucher.VoucherType == "JV" && string.IsNullOrWhiteSpace(massVoucher.JVDebitOrCredit))
            {
                isValid = SetErrorMessage("JVDebitOrCreditNotSelected");
            }
            else if (massVoucher.IsFixedAsset && string.IsNullOrWhiteSpace(massVoucher.FixedAssetName))
            {
                isValid = SetWarningMessage("NoFixedAssetParticularNameFound");
            }
            else if (massVoucher.IsCheque &&
                     (string.IsNullOrWhiteSpace(massVoucher.ChequeNo) || string.IsNullOrWhiteSpace(massVoucher.BankName)))
            {
                isValid = SetInformationMessage("NoChequeOrBankInfo");
            }
            else if (massVoucher.IsFixedAsset && massVoucher.FixedAssetDepreciationRate == 0)
            {
                isValid = SetInformationMessage("ZeroDepreciationProvidedForFixedAsset");
            }

            if (isValid)
            {
                _massVoucher = massVoucher;
                _projectHead = _projectHeadRepository.GetSingle(
                    ph => ph.Project.ID == massVoucher.Project.ID && ph.Head.ID == massVoucher.Head.ID);
                isValid = SetEntryableRecords();
            }
            return isValid;
        }

        private bool SetErrorMessage(string messageKey)
        {
            InvokeManagerEvent(EventType.Error, messageKey);
            return false;
        }

        private bool SetWarningMessage(string messageKey)
        {
            InvokeManagerEvent(EventType.Warning, messageKey);
            return true;
        }

        private bool SetInformationMessage(string messageKey)
        {
            InvokeManagerEvent(EventType.Information, messageKey);
            return true;
        }

        private bool SetEntryableRecords()
        {
            bool isValid = true;

            IList<Record> records = new List<Record>();
            if (_massVoucher.VoucherType.Equals("DV", StringComparison.OrdinalIgnoreCase))
            {
                DebitVoucher debitVoucher = GetDebitVoucher();
                Record transaction = _massVoucher.IsCheque
                                         ? (Record)GetTransactionInCheque(0, _massVoucher.Amount)
                                         : (Record)GetTransactionInCash(0, _massVoucher.Amount);
                //if (debitVoucher.IsValid() && transaction.IsValid())
                //{
                records.Add(debitVoucher);
                records.Add(transaction);
                //}
                //else
                //{
                //    isValid = SetErrorMessage("UnknownProblemArise");
                //}
            }
            else if (_massVoucher.VoucherType.Equals("CV", StringComparison.OrdinalIgnoreCase))
            {
                CreditVoucher creditVoucher = GetCreditVoucher();
                Record transaction = _massVoucher.IsCheque
                                         ? (Record)GetTransactionInCheque(_massVoucher.Amount, 0)
                                         : (Record)GetTransactionInCash(_massVoucher.Amount, 0);
                //if (creditVoucher.IsValid() && transaction.IsValid())
                //{
                records.Add(creditVoucher);
                records.Add(transaction);
                //}
                //else
                //{
                //    isValid = SetErrorMessage("UnknownProblemArise");
                //}
            }
            else if (_massVoucher.VoucherType.Equals("JV", StringComparison.OrdinalIgnoreCase))
            {
                JournalVoucher journalVoucher = GetJournalVoucher();
                //if (journalVoucher.IsValid())
                //{
                records.Add(journalVoucher);
                //}
                //else
                //{
                //    isValid = SetErrorMessage("UnknownProblemArise");
                //}
            }
            else if (_massVoucher.VoucherType.Equals("Contra", StringComparison.OrdinalIgnoreCase))
            {
                double cashDebit = _massVoucher.ContraType.Equals("bank to cash", StringComparison.OrdinalIgnoreCase)
                                       ? _massVoucher.Amount
                                       : 0;
                double cashCredit = _massVoucher.ContraType.Equals("cash to bank", StringComparison.OrdinalIgnoreCase)
                                        ? _massVoucher.Amount
                                        : 0;
                TransactionInCash cashTransaction = GetTransactionInCash(cashDebit, cashCredit);
                TransactionInCheque chequeTransaction = GetTransactionInCheque(cashCredit, cashDebit);
                //if (cashTransaction.IsValid() && chequeTransaction.IsValid())
                //{
                records.Add(cashTransaction);
                records.Add(chequeTransaction);
                //}
                //else
                //{
                //    isValid = SetErrorMessage("UnknownProblemArise");
                //}
            }

            if (isValid && records.Count > 0) _entryableRecords = records;
            else isValid = false;

            return isValid;
        }

        public IList<Record> GetEntryableRecords()
        {
            return _entryableRecords;
        }

        public int GetNewVoucherNo(string voucherType, Project project)
        {
            IList<ProjectHead> projectHeads = _projectHeadRepository.Get(ph => ph.Project.ID == project.ID).ToList();
            int[] ids = projectHeads.Select(ph => ph.ID).ToArray();
            IList<Record> records = _recordRepository.GetAll().ToList();
            if (records.Count == 0) return 1;
            records = records.Where(r => r.VoucherType == voucherType).ToList();
            if (records.Count == 0) return 1;
            records = records.Where(r => ids.Contains(r.ProjectHead.ID)).ToList();
            if (records.Count == 0) return 1;
            int maxVoucherSerialNo = records.Max(r => r.VoucherSerialNo);
            return maxVoucherSerialNo + 1;
        }

        private DebitVoucher GetDebitVoucher()
        {
            DebitVoucher debitVoucher = new DebitVoucher(_recordRepository, _fixedAssetRepository)
            {
                Amount = _massVoucher.Amount,
                ProjectHead = _projectHead,
                Date = _massVoucher.VoucherDate,
                Narration = _massVoucher.Narration,
                Tag = _massVoucher.Tag,
                VoucherSerialNo = _massVoucher.VoucherSerialNo,
                VoucherType = _massVoucher.VoucherType,
                IsActive = true
            };
            if (_massVoucher.IsFixedAsset)
            {
                debitVoucher.IsFixedAsset = true;
                debitVoucher.FixedAsset = GetFixedAsset();
            }
            return debitVoucher;
        }

        private CreditVoucher GetCreditVoucher()
        {
            CreditVoucher creditVoucher = new CreditVoucher(_recordRepository)
            {
                Amount = _massVoucher.Amount,
                ProjectHead = _projectHead,
                Date = _massVoucher.VoucherDate,
                Narration = _massVoucher.Narration,
                Tag = _massVoucher.Tag,
                VoucherSerialNo = _massVoucher.VoucherSerialNo,
                Link = _massVoucher.LinkedVoucherNo,
                VoucherType = _massVoucher.VoucherType,
                IsActive = true,
                IsFixedAsset = false
            };
            return creditVoucher;
        }

        private JournalVoucher GetJournalVoucher()
        {
            double debit = _massVoucher.JVDebitOrCredit.Equals("debit", StringComparison.OrdinalIgnoreCase)
                               ? _massVoucher.Amount
                               : 0;
            double credit = _massVoucher.JVDebitOrCredit.Equals("credit", StringComparison.OrdinalIgnoreCase)
                               ? _massVoucher.Amount
                               : 0;
            JournalVoucher journalVoucher = new JournalVoucher(_recordRepository, _fixedAssetRepository)
            {
                Amount = _massVoucher.Amount,
                ProjectHead = _projectHead,
                Date = _massVoucher.VoucherDate,
                Narration = _massVoucher.Narration,
                Tag = _massVoucher.Tag,
                VoucherSerialNo = _massVoucher.VoucherSerialNo,
                Link = _massVoucher.LinkedVoucherNo,
                VoucherType = _massVoucher.VoucherType,
                JVDebitOrCredit = _massVoucher.JVDebitOrCredit,
                Debit = debit,
                Credit = credit,
                IsActive = true
            };
            if (_massVoucher.IsFixedAsset)
            {
                journalVoucher.IsFixedAsset = true;
                journalVoucher.FixedAsset = GetFixedAsset();
            }
            return journalVoucher;
        }

        private FixedAsset GetFixedAsset()
        {
            return new FixedAsset
            {
                Name = _massVoucher.FixedAssetName,
                DepreciationRate = _massVoucher.FixedAssetDepreciationRate
            };
        }

        private TransactionInCheque GetTransactionInCheque(double debit, double credit)
        {
            var transactionInCheque = new TransactionInCheque(_recordRepository, _bankRecordRepository)
                                          {
                                              ProjectHead = _projectHead,
                                              Date = _massVoucher.VoucherDate,
                                              Narration = _massVoucher.Narration,
                                              Tag = _massVoucher.Tag,
                                              VoucherSerialNo = _massVoucher.VoucherSerialNo,
                                              Link = _massVoucher.LinkedVoucherNo,
                                              VoucherType = _massVoucher.VoucherType,
                                              Debit = debit,
                                              Credit = credit,
                                              BankRecord = _massVoucher.IsCheque ? GetBankRecord() : null
                                          };
            return transactionInCheque;
        }

        private TransactionInCash GetTransactionInCash(double debit, double credit)
        {
            TransactionInCash transactionInCash = new TransactionInCash(_recordRepository)
            {
                ProjectHead = _projectHead,
                Date = _massVoucher.VoucherDate,
                Narration = _massVoucher.Narration,
                Tag = _massVoucher.Tag,
                VoucherSerialNo = _massVoucher.VoucherSerialNo,
                Link = _massVoucher.LinkedVoucherNo,
                VoucherType = _massVoucher.VoucherType,
                Debit = debit,
                Credit = credit,
                IsActive = true
            };
            return transactionInCash;
        }

        private BankRecord GetBankRecord()
        {
            return new BankRecord
                       {
                           BankName = _massVoucher.BankName,
                           Branch = _massVoucher.BankBranch,
                           ChequeNo = _massVoucher.ChequeNo,
                           ChequeDate = _massVoucher.ChequeDate
                       };
        }
    }
}
