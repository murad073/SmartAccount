using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.Model.Managers;
using BLL.Model.Repositories;
using BLL.Model.Entity;
using BLL.Model;

namespace BLL.VoucherManagement
{
    public class VoucherManager : ManagerBase, IVoucherManager
    {
        private readonly IRepository<Record> _recordRepository;
        public VoucherManager(IRepository<Record> recordRepository)
        {
            _recordRepository = recordRepository;
        }

        public override string ModuleName
        {
            get { return "Voucher"; }
        }

        public DateTime VoucherStartDate { get; set; }
        public DateTime VoucherEndDate { get; set; }

        public bool Validate(Project project, DateTime startDate, DateTime endDate)
        {
            if (project == null)
            {
                InvokeManagerEvent(EventType.Error, "NoProjectSelected");
                return false;
            }

            if (startDate > endDate)
            {
                InvokeManagerEvent(EventType.Error, "NoProjectSelected");
                return false;
            }

            return true;
        }

        public Record GetNextRecord(int id)
        {
            return _recordRepository.Get(r => r.ID == id + 1).SingleOrDefault();
        }

        public IList<Record> GetVouchers(Project project, string voucherType)
        {
            IList<Record> records = _recordRepository.Get(r => r.ProjectHead.Project.ID == project.ID && r.LedgerType == "LedgerBook").ToList();
            if (voucherType != "All")
                records = records.Where(r => r.VoucherType == voucherType).ToList();
            records = records.OrderBy(r => r.VoucherType).ToList();
            records = records.Where(r => GetDateAt12Am(r.Date) >= VoucherStartDate && GetDateAt12Am(r.Date) <= VoucherEndDate).ToList();

            return records;
        }

        public IList<Record> GetVouchers(string voucherNo)
        {
            string[] voucherNoParts = voucherNo.Split('-');
            string voucherType = voucherNoParts[0];
            int voucherSerilaNo = int.Parse(voucherNoParts[1]);
            IList<Record> records = _recordRepository.Get(r => r.VoucherType == voucherType && r.VoucherSerialNo == voucherSerilaNo).ToList();
            return records;
        }

        public static DateTime GetDateAt12Am(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day);
        }
    }
}
