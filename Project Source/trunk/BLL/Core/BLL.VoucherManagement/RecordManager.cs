using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.Model;
using BLL.Model.Entity;
using BLL.Model.Managers;
using BLL.Model.Repositories;

namespace BLL.VoucherManagement
{
    public class RecordManager : ManagerBase, IRecordManager
    {
        private readonly IRepository<Record> _recordRepository = default(IRepository<Record>);
        private  IList<Record> _records = default(List<Record>);
        public RecordManager(IRepository<Record> recordRepository)
        {
            _recordRepository = recordRepository;
        }

        public void SetRecords(IList<Record> records)
        {
            _records = records;
        }

        public bool Save()
        {
            if (IsDebitCreditBalanced())
            {
                bool success = true;
                //_recordRepository.BeginTransaction();

                if (_records.Any(record => !record.Save()))
                {
                    success = false;
                    InvokeManagerEvent(EventType.Error, "UnknownProblemArise");
                    
                }

                if (success)
                {
                    //if (_recordRepository.CommitTransaction())
                    //{
                        InvokeManagerEvent(EventType.Success, "VoucherPostedSuccessfully");
                    //}
                }
                //else _recordRepository.RollbackTransaction();
                return success;
            }

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
