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
        private readonly IRepository<BankBook> _bankBookRepository;
        private ProjectHead _projectHead;

        private MassVoucher _massVoucher;
        private readonly IVoucherManager _voucherManager;

        private IList<Record> _entryableRecords;

        public MassVoucherManager(IRepository<Record> recordRepository, IRepository<Project> projectRepository,
                                  IRepository<Head> headRepository, IRepository<ProjectHead> projectHeadRepository, IRepository<FixedAsset> fixedAssetRepository, IRepository<BankBook> bankBookRepository)
        {
            _recordRepository = recordRepository;
            _bankBookRepository = bankBookRepository;
            _projectRepository = projectRepository;
            _headRepository = headRepository;
            _projectHeadRepository = projectHeadRepository;
            _fixedAssetRepository = fixedAssetRepository;

            _voucherManager = new VoucherManager(recordRepository);
        }

        public override string ModuleName
        {
            get { return "MassVoucherManager"; }
        }

        public bool Set(MassVoucher massVoucher)
        {
            bool isValid = true;
            double ignored = 0;
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
            else if (_voucherManager.GetVouchers(massVoucher.VoucherType + "-" + massVoucher.VoucherSerialNo.ToString(), ref ignored).Count() != 0)
            {
                isValid = SetWarningMessage("VoucherAlreadyExists");
            }

            if (isValid)
            {
                _massVoucher = massVoucher;

                if (!massVoucher.VoucherType.Equals("Contra", StringComparison.OrdinalIgnoreCase))
                {
                    _projectHead = _projectHeadRepository.GetSingle(
                        ph => ph.Project.ID == massVoucher.Project.ID && ph.Head.ID == massVoucher.Head.ID);
                }
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
                records.Add(debitVoucher);
                records.Add(transaction);
            }
            else if (_massVoucher.VoucherType.Equals("CV", StringComparison.OrdinalIgnoreCase))
            {
                CreditVoucher creditVoucher = GetCreditVoucher();
                Record transaction = _massVoucher.IsCheque
                                         ? (Record)GetTransactionInCheque(_massVoucher.Amount, 0)
                                         : (Record)GetTransactionInCash(_massVoucher.Amount, 0);
                records.Add(creditVoucher);
                records.Add(transaction);
            }
            else if (_massVoucher.VoucherType.Equals("JV", StringComparison.OrdinalIgnoreCase))
            {
                JournalVoucher journalVoucher = GetJournalVoucher();
                records.Add(journalVoucher);
            }
            else if (_massVoucher.VoucherType.Equals("Contra", StringComparison.OrdinalIgnoreCase))
            {
                double cashDebit = _massVoucher.ContraType.Equals("bank to cash", StringComparison.OrdinalIgnoreCase)
                                       ? _massVoucher.Amount
                                       : 0;
                double cashCredit = _massVoucher.ContraType.Equals("cash to bank", StringComparison.OrdinalIgnoreCase)
                                        ? _massVoucher.Amount
                                        : 0;

                Head bankBook = _headRepository.GetSingle(h => h.Name.Equals("Bank Book", StringComparison.OrdinalIgnoreCase));
                Head cashBook = _headRepository.GetSingle(h => h.Name.Equals("Cash Book", StringComparison.OrdinalIgnoreCase));

                ProjectHead bankBookProjectHead = _projectHeadRepository.GetSingle(ph => ph.Project.ID == _massVoucher.Project.ID && ph.Head.ID == bankBook.ID);
                ProjectHead cashBookProjectHead = _projectHeadRepository.GetSingle(ph => ph.Project.ID == _massVoucher.Project.ID && ph.Head.ID == cashBook.ID);

                TransactionInCash cashTransaction = GetTransactionInCash(cashDebit, cashCredit);
                cashTransaction.ProjectHead = cashBookProjectHead;

                TransactionInCheque chequeTransaction = GetTransactionInCheque(cashCredit, cashDebit);
                chequeTransaction.ProjectHead = bankBookProjectHead;

                records.Add(cashTransaction);
                records.Add(chequeTransaction);
            }

            if (records.Count > 0) _entryableRecords = records;
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
            if (!voucherType.Equals("Contra", StringComparison.OrdinalIgnoreCase))
            {
                records = records.Where(r => ids.Contains(r.ProjectHead.ID)).ToList();
                if (records.Count == 0) return 1;
            }
            int maxVoucherSerialNo = records.Max(r => r.VoucherSerialNo);
            return maxVoucherSerialNo + 1;
        }

        private DebitVoucher GetDebitVoucher()
        {
            DebitVoucher debitVoucher = new DebitVoucher(_recordRepository, _fixedAssetRepository)
            {
                ProjectHead = _projectHead,
                Date = _massVoucher.VoucherDate,
                Narration = _massVoucher.Narration,
                Tag = _massVoucher.Tag,
                VoucherSerialNo = _massVoucher.VoucherSerialNo,
                VoucherType = _massVoucher.VoucherType,
                FinancialYear = _massVoucher.FinancialYear,
                IsActive = true
            };
            debitVoucher.SetAmount(_massVoucher.Amount);
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
            creditVoucher.SetAmount(_massVoucher.Amount);
            return creditVoucher;
        }

        private JournalVoucher GetJournalVoucher()
        {
            //double debit = _massVoucher.JVDebitOrCredit.Equals("debit", StringComparison.OrdinalIgnoreCase)
            //                   ? _massVoucher.Amount
            //                   : 0;
            //double credit = _massVoucher.JVDebitOrCredit.Equals("credit", StringComparison.OrdinalIgnoreCase)
            //                   ? _massVoucher.Amount
            //                   : 0;
            JournalVoucher journalVoucher = new JournalVoucher(_recordRepository, _fixedAssetRepository)
            {
                ProjectHead = _projectHead,
                Date = _massVoucher.VoucherDate,
                Narration = _massVoucher.Narration,
                Tag = _massVoucher.Tag,
                VoucherSerialNo = _massVoucher.VoucherSerialNo,
                Link = _massVoucher.LinkedVoucherNo,
                VoucherType = _massVoucher.VoucherType,
                JVDebitOrCredit = _massVoucher.JVDebitOrCredit,
                IsActive = true
            };
            journalVoucher.SetAmount(_massVoucher.Amount);
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
            var transactionInCheque = new TransactionInCheque(_recordRepository, _bankBookRepository)
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
                                              IsActive = true,
                                              BankBook = _massVoucher.IsCheque ? GetBankBook() : null
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

        private BankBook GetBankBook()
        {
            return new BankBook
                       {
                           BankName = _massVoucher.BankName,
                           Branch = _massVoucher.BankBranch,
                           ChequeNo = _massVoucher.ChequeNo,
                           ChequeDate = _massVoucher.ChequeDate
                       };
        }
    }
}
