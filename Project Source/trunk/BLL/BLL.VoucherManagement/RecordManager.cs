using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.Model;
using BLL.Model.Managers;
using BLL.Model.Schema;
using BLL.Model.Repositories;


namespace BLL.VoucherManagement
{
    public class RecordManager : ManagerBase, IRecordManager
    {
        private readonly IRecordRepository _recordRepository = default(IRecordRepository);
        private readonly IList<Record> _records = default(List<Record>);
        public RecordManager(IRecordRepository recordRepository, IList<Record> records)
        {
            _recordRepository = recordRepository;
            _records = records;
        }

        public bool Save()
        {
            if (IsDebitCreditBalanced())
            {
                bool success = true;
                _recordRepository.BeginTransaction();

                if (_records.Any(record => !record.Save()))
                {
                    success = false;
                    //_latestMessage = MessageService.Instance.Get("UnknownProblemArise", MessageType.Error);
                    InvokeManagerEvent(EventType.Error, "UnknownProblemArise");
                    
                }

                if (success)
                {
                    if (_recordRepository.CommitTransaction())
                    {
                        //_latestMessage = MessageService.Instance.Get("VoucherPostedSuccessfully", MessageType.Success);
                        InvokeManagerEvent(EventType.Success, "VoucherPostedSuccessfully");
                    }
                }
                else _recordRepository.RollbackTransaction();
                return success;
            }

            //_latestMessage = MessageService.Instance.Get("VoucherBalanceIsNotZero", MessageType.Error);
            InvokeManagerEvent(EventType.Success, "VoucherBalanceIsNotZero");
            return false;
        }

        private bool IsDebitCreditBalanced()
        {
            double debit = _records.Sum(r => r.Debit);
            double credit = _records.Sum(r => r.Credit);

            if (debit == credit && (debit != 0 || credit != 0)) return true;
            return false;
        }
    }
}
