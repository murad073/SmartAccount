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
        private readonly Message _latestMessage;
        public RecordManager(IRecordRepository recordRepository, IList<Record> records)
        {
            _recordRepository = recordRepository;
            _records = records;
            _latestMessage = new Message();
        }

        public bool Save()
        {
            bool success = true;
            _recordRepository.BeginTransaction();

            foreach (Record record in _records)
            {
                if(!record.Save()) success = false;
            }

            if (success)
            {
                if (_recordRepository.CommitTransaction())
                {
                    _latestMessage.MessageType = MessageType.Success;
                    _latestMessage.MessageText = MessageText.VoucherPostedSuccessfully;
                }
            }
            else _recordRepository.RollbackTransaction();

            return true;
        }

        public Message GetLatestMessage()
        {
            return _latestMessage;
        }
    }
}
