using System;
using System.Collections.Generic;
using BLL.Model;
using BLL.Model.Managers;
using BLL.Model.Repositories;
using BLL.Model.Schema;

namespace BLL.VoucherManagement
{
    public class MassVoucherManager : ManagerBase, IMassVoucherManager
    {
        private readonly IHeadRepository _headRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IRecordRepository _recordRepository;

        private MassVoucher _massVoucher;

        private IList<Record> _entryableRecords;

        public MassVoucherManager(IRecordRepository dalVoucherManager, IProjectRepository dalProjectManager,
                                  IHeadRepository dalHeadManager)
        {
            _recordRepository = dalVoucherManager;
            _projectRepository = dalProjectManager;
            _headRepository = dalHeadManager;
        }

        public override string ModuleName
        {
            get { return "MassVoucherManager"; }
        }

        public bool Set(MassVoucher massVoucher)
        {
            bool isValid = true;
            if (string.IsNullOrWhiteSpace(massVoucher.ProjectName))
            {
                isValid = SetErrorMessage("NoProjectSelected");
            }
            else if (massVoucher.VoucherType != "Contra" && string.IsNullOrWhiteSpace(massVoucher.HeadName))
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
                isValid = SetEntryableRecords();
            }
            return isValid;
        }

        private bool SetErrorMessage(string messageKey)
        {
            //_latestMessage = MessageService.Instance.Get(messageKey, MessageType.Error);
            InvokeManagerEvent(EventType.Error, messageKey);
            return false;
        }

        private bool SetWarningMessage(string messageKey)
        {
            //_latestMessage = MessageService.Instance.Get(messageKey, MessageType.Warning);
            InvokeManagerEvent(EventType.Warning, messageKey);
            return true;
        }

        private bool SetInformationMessage(string messageKey)
        {
            //_latestMessage = MessageService.Instance.Get(messageKey, MessageType.Information);
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
                if (debitVoucher.IsValid() && transaction.IsValid())
                {
                    records.Add(debitVoucher);
                    records.Add(transaction);
                }
                else
                {
                    isValid = SetErrorMessage("UnknownProblemArise");
                }
            }
            else if (_massVoucher.VoucherType.Equals("CV", StringComparison.OrdinalIgnoreCase))
            {
                CreditVoucher creditVoucher = GetCreditVoucher();
                Record transaction = _massVoucher.IsCheque
                                         ? (Record)GetTransactionInCheque(_massVoucher.Amount, 0)
                                         : (Record)GetTransactionInCash(_massVoucher.Amount, 0);
                if (creditVoucher.IsValid() && transaction.IsValid())
                {
                    records.Add(creditVoucher);
                    records.Add(transaction);
                }
                else
                {
                    isValid = SetErrorMessage("UnknownProblemArise");
                }
            }
            else if (_massVoucher.VoucherType.Equals("JV", StringComparison.OrdinalIgnoreCase))
            {
                JournalVoucher journalVoucher = GetJournalVoucher();
                if (journalVoucher.IsValid())
                {
                    records.Add(journalVoucher);
                }
                else
                {
                    isValid = SetErrorMessage("UnknownProblemArise");
                }
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
                if (cashTransaction.IsValid() && chequeTransaction.IsValid())
                {
                    records.Add(cashTransaction);
                    records.Add(chequeTransaction);
                }
                else
                {
                    isValid = SetErrorMessage("UnknownProblemArise");
                }
            }

            if (isValid && records.Count > 0) _entryableRecords = records;
            else isValid = false;

            return isValid;
        }

        public IList<Record> GetEntryableRecords()
        {
            return _entryableRecords;
        }

        public int GetNewVoucherNo(string key, string projectName)
        {
            Project project = _projectRepository.Get(projectName);
            return _recordRepository.GetMaxVoucherNo(key, project.Id) + 1;
        }

        private DebitVoucher GetDebitVoucher()
        {
            DebitVoucher debitVoucher = new DebitVoucher(_recordRepository)
            {
                Amount = _massVoucher.Amount,
                HeadName = _massVoucher.HeadName,
                ProjectName = _massVoucher.ProjectName,
                Date = _massVoucher.VoucherDate,
                Narration = _massVoucher.Narration,
                Tag = _massVoucher.Tag,
                VoucherSerialNo = _massVoucher.VoucherSerialNo,
                LinkedVoucherNo = _massVoucher.LinkedVoucherNo,
                VoucherTypeKey = _massVoucher.VoucherType
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
                HeadName = _massVoucher.HeadName,
                ProjectName = _massVoucher.ProjectName,
                Date = _massVoucher.VoucherDate,
                Narration = _massVoucher.Narration,
                Tag = _massVoucher.Tag,
                VoucherSerialNo = _massVoucher.VoucherSerialNo,
                LinkedVoucherNo = _massVoucher.LinkedVoucherNo,
                VoucherTypeKey = _massVoucher.VoucherType,
                IsFixedAsset = false //TODO: (sobuj) CV cannot be fixed asset. Confirm from sobuj
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
            JournalVoucher journalVoucher = new JournalVoucher(_recordRepository)
            {
                Amount = _massVoucher.Amount,
                HeadName = _massVoucher.HeadName,
                ProjectName = _massVoucher.ProjectName,
                Date = _massVoucher.VoucherDate,
                Narration = _massVoucher.Narration,
                Tag = _massVoucher.Tag,
                VoucherSerialNo = _massVoucher.VoucherSerialNo,
                LinkedVoucherNo = _massVoucher.LinkedVoucherNo,
                VoucherTypeKey = _massVoucher.VoucherType,
                JVDebitOrCredit = _massVoucher.JVDebitOrCredit,
                Debit = debit,
                Credit = credit
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
            var transactionInCheque = new TransactionInCheque(_recordRepository)
                                          {
                                              HeadName = _massVoucher.HeadName,
                                              ProjectName = _massVoucher.ProjectName,
                                              Date = _massVoucher.VoucherDate,
                                              Narration = _massVoucher.Narration,
                                              Tag = _massVoucher.Tag,
                                              VoucherSerialNo = _massVoucher.VoucherSerialNo,
                                              LinkedVoucherNo = _massVoucher.LinkedVoucherNo,
                                              VoucherTypeKey = _massVoucher.VoucherType,
                                              Debit = debit,
                                              Credit = credit,
                                              ChequeInfo = _massVoucher.IsCheque ? GetChequeInfo() : null
                                          };
            return transactionInCheque;
        }

        private TransactionInCash GetTransactionInCash(double debit, double credit)
        {
            TransactionInCash transactionInCash = new TransactionInCash(_recordRepository)
            {
                HeadName = _massVoucher.HeadName,
                ProjectName = _massVoucher.ProjectName,
                Date = _massVoucher.VoucherDate,
                Narration = _massVoucher.Narration,
                Tag = _massVoucher.Tag,
                VoucherSerialNo = _massVoucher.VoucherSerialNo,
                LinkedVoucherNo = _massVoucher.LinkedVoucherNo,
                VoucherTypeKey = _massVoucher.VoucherType,
                Debit = debit,
                Credit = credit
            };
            return transactionInCash;
        }

        private ChequeInfo GetChequeInfo()
        {
            return new ChequeInfo
                       {
                           BankName = _massVoucher.BankName,
                           BankBranch = _massVoucher.BankBranch,
                           ChequeNo = _massVoucher.ChequeNo,
                           Date = _massVoucher.ChequeDate
                       };
        }


    }
}