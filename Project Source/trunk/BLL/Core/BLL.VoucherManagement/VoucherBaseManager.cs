//using System.Collections.Generic;
//using BLL.Messaging;
//using BLL.Model.Repositories;
//using BLL.Model.Schema;

//namespace BLL.VoucherManagement
//{
//    internal class VoucherBaseManager
//    {
//        private static Message _message;
//        private readonly IHeadRepository _dalHeadRepository;
//        private readonly IProjectRepository _dalProjectRepository;
//        //private readonly IRecordRepository _dalRecordRepository;

//        //private FixedAsset _fixedAsset;
//        //private bool _isValidFixedAsset;
//        //private VoucherBase _voucher;

//        public VoucherBaseManager(IRecordRepository dalVoucherManager, IProjectRepository dalProjectManager,
//                              IHeadRepository dalHeadManager)
//        {
//            //_dalRecordRepository = dalVoucherManager;
//            _dalProjectRepository = dalProjectManager;
//            _dalHeadRepository = dalHeadManager;
//            //if (voucher != null) _voucher = voucher;
//        }

//        //public void Attach(VoucherBase voucher)
//        //{
//        //    //_voucher = voucher;
//        //}

//        //public void Attach(FixedAsset fixedAsset)
//        //{
//        //    _isValidFixedAsset = true;
//        //    _fixedAsset = fixedAsset;
//        //}

//        public Message GetMessage()
//        {
//            return _message;
//        }

//        //public bool IsValid(VoucherBase voucher)  // TODO: Moved to VoucherBase
//        //{
//        //    //bool isValid = true;
//        //    if (voucher == null) return false;

//        //    Project project = _dalProjectRepository.Get(voucher.ProjectName);
//        //    Head head = _dalHeadRepository.Get(voucher.HeadName);

//        //    if (project == null)
//        //    {
//        //        _message = MessageService.Instance.Get(ErrorMessage.InvalidProject.ToString(), MessageType.Error);
//        //        return false;
//        //    }
//        //    if (head == null)
//        //    {
//        //        _message = MessageService.Instance.Get(ErrorMessage.InvalidHeadForProject.ToString(), MessageType.Error);
//        //        return false;
//        //    }

//        //    IList<Head> allValidHeads = _dalHeadRepository.GetAll(project.Id);
//        //    bool isValidHeadFound = false;
//        //    foreach (Head singleHead in allValidHeads)
//        //    {
//        //        if (singleHead.Id == head.Id)
//        //        {
//        //            isValidHeadFound = true;
//        //            break;
//        //        }
//        //    }

//        //    if (!isValidHeadFound)
//        //    {
//        //        _message = MessageService.Instance.Get(ErrorMessage.InvalidHeadForProject.ToString(), MessageType.Error);
//        //        _message.MessageText = string.Format(_message.MessageText, voucher.ProjectName);
//        //        return false;
//        //    }

//        //    if ((voucher.Debit == voucher.Credit) || voucher.Debit < 0 || voucher.Credit < 0 ||
//        //             (voucher.Debit > 0 && voucher.Credit != 0) || (voucher.Credit > 0 && voucher.Debit != 0))
//        //    {
//        //        _message = MessageService.Instance.Get(ErrorMessage.DebitOrCreditAmountIsInvalid.ToString(), MessageType.Error);
//        //        return false;
//        //    }

//        //    // is valid linking voucher

//        //    // set message value

//        //    return true;
//        //}

//        //public bool Save(VoucherBase voucher)
//        //{
//        // 1. Save a Voucher and get the id of new entered voucher
//        // 2. If associated fixed asset found, save fixed asset with the voucher id

//        //BLLRecord bllRecord = new BLLRecord { ProjectId = voucher.Project.Id, HeadId = voucher.Head.Id, Debit = 0, Credit = 0, Date = voucher.VoucherDate, Description = voucher.Narration, VoucherType = voucher.VoucherType, VoucherSerialNo = voucher.VoucherSerialNo };

//        //if (voucher.VoucherType == "CV")
//        //{
//        //    bllRecord.Credit = voucher.Amount;
//        //    _dalVoucherManager.InsertLedgerBookRow(bllRecord);
//        //    cashOrChequeTransaction(bllRecord, voucher.Amount, 0, voucher.TransactionType, voucher.ChequeInfo);

//        //}
//        //else if (voucher.VoucherType == "DV")
//        //{
//        //    bllRecord.Debit = voucher.Amount;

//        //    if (voucher.IsFixedAsset)
//        //    {
//        //        BLLFixedAssetRecord fixedAssetRecord = new BLLFixedAssetRecord(bllRecord);
//        //        fixedAssetRecord.FixedAssetName = voucher.FixedAssetInfo.Name;
//        //        fixedAssetRecord.DepreciationRate = voucher.FixedAssetInfo.DepreciationRate;
//        //        _dalVoucherManager.InsertFixedAssetRow(fixedAssetRecord);
//        //    }
//        //    else
//        //        _dalVoucherManager.InsertLedgerBookRow(bllRecord);

//        //    cashOrChequeTransaction(bllRecord, 0, voucher.Amount, voucher.TransactionType, voucher.ChequeInfo);
//        //}
//        //else if (voucher.VoucherType == "JV")
//        //{
//        //    if (voucher.JournalDebitOrCredit == "Debit") bllRecord.Debit = voucher.Amount;
//        //    else if (voucher.JournalDebitOrCredit == "Credit") bllRecord.Credit = voucher.Amount;
//        //    else return;    //TODO: remove the return line

//        //    if (voucher.IsFixedAsset)
//        //    {
//        //        BLLFixedAssetRecord fixedAssetRecord = new BLLFixedAssetRecord(bllRecord);
//        //        fixedAssetRecord.FixedAssetName = voucher.FixedAssetInfo.Name;
//        //        fixedAssetRecord.DepreciationRate = voucher.FixedAssetInfo.DepreciationRate;
//        //        _dalVoucherManager.InsertFixedAssetRow(fixedAssetRecord);
//        //    }
//        //    else
//        //        _dalVoucherManager.InsertLedgerBookRow(bllRecord);
//        //}
//        //return false;
//        //}

//        //private void cashOrChequeTransaction(BLLRecord record, double debit, double credit, string transactionType, Cheque chequeInfo = null)
//        //{
//        //    if (transactionType.ToLower().Equals("cash"))
//        //    {
//        //        BLLRecord cashRecord = new BLLRecord(record);
//        //        cashRecord.Debit = debit;
//        //        cashRecord.Credit = credit;
//        //        _dalVoucherManager.InsertCashBookRow(cashRecord);
//        //    }
//        //    else if (transactionType.ToLower().Equals("cheque"))
//        //    {
//        //        BLLBankRecord bankRecord = new BLLBankRecord(record);
//        //        bankRecord.ChequeNo = chequeInfo.ChequeNo;
//        //        bankRecord.Debit = debit;
//        //        bankRecord.Credit = credit;
//        //        _dalVoucherManager.InsertBankBookRow(bankRecord);
//        //    }
//        //}
//    }
//}