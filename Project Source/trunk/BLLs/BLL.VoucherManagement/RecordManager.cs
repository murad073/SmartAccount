using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.Model.Schema;
using BLL.Model.Repositories;
using BLL.Messaging;

namespace BLL.VoucherManagement
{
    public class RecordManager
    {
        private readonly IRecordRepository _recordRepository = default(IRecordRepository);
        private readonly IList<Record> _records = default(List<Record>);
        private Message _latestMessage;
        public RecordManager(IRecordRepository recordRepository, IList<Record> records)
        {
            _recordRepository = recordRepository;
            _records = records;
            _latestMessage = new Message();
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
                    _latestMessage = MessageService.Instance.Get(ErrorMessage.UnknownProblemArise.ToString(), MessageType.Error);
                }

                if (success)
                {
                    if (_recordRepository.CommitTransaction())
                    {
                        _latestMessage = MessageService.Instance.Get(
                            SuccessMessage.VoucherPostedSuccessfully.ToString(), MessageType.Success);
                    }
                }
                else _recordRepository.RollbackTransaction();
                return success;
            }

            _latestMessage = MessageService.Instance.Get(ErrorMessage.VoucherBalanceIsNotZero.ToString(),
                                                             MessageType.Error);
            return false;
        }

        public Message GetLatestMessage()
        {
            return _latestMessage;
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
