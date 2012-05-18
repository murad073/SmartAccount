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

        public IList<Record> GetVouchers(Project project, string voucherType)
        {
            // TODO: The LedgerType filtering will not allow to get the Cash/Bank column in the DataGrid.
            // What if we keep a column Cash/Bank in the Record table and create a separate table for CashBook?
            IList<Record> records = _recordRepository.Get(r => r.ProjectHead.Project.ID == project.ID && r.LedgerType == "LedgerBook").ToList();
            if (voucherType != "All")
                records = records.Where(r => r.VoucherType == voucherType).ToList();
            records = records.OrderBy(r => r.VoucherType).ToList();
            records = records.Where(r => GetDateAt12Am(r.Date) >= VoucherStartDate && GetDateAt12Am(r.Date) <= VoucherEndDate).ToList();

            return records;
        }

        public static DateTime GetDateAt12Am(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day);
        }
    }
}
