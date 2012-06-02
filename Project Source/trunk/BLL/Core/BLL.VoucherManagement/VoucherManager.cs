using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.Model.Managers;
using BLL.Model.Repositories;
using BLL.Model.Entity;
using BLL.Model;
using BLL.Utils;

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
            get { return "VoucherManager"; }
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

        // TODO: This is the most ugly function of this project.
        public IList<Record> GetVouchers(Project project, string voucherType)
        {
            IList<Record> records = _recordRepository.Get(r => r.ProjectHead.Project.ID == project.ID).ToList();

            IList<Record> records1 = null;
            IList<Record> records2 = null;

            if (voucherType != "Contra")
                records1 = records.Where(r => r.LedgerType == "LedgerBook").ToList();

            if (voucherType == "Contra" || voucherType == "All")
                records2 = records.Where(r => r.VoucherType == "Contra").ToList();

            if (records1 == null && records2 == null)
                return null;
            else if (records1 != null && records2 == null)
                records = records1;
            else if (records1 == null && records2 != null)
                records = records2;
            else
                records = records1.Union(records2).ToList();


            if (voucherType != "All")
            {
                records = records.Where(r => r.VoucherType == voucherType).ToList();

            }
            records = records.OrderBy(r => r.VoucherType).ToList();
            records = records.Where(r => GetDateAt12Am(r.Date) >= VoucherStartDate && GetDateAt12Am(r.Date) <= VoucherEndDate).ToList();

            return records;
        }

        public IList<Record> GetVouchers(string voucherNo, ref double amount)
        {
            amount = 0;
            string[] voucherNoParts = voucherNo.Split('-');
            string voucherType = voucherNoParts[0];
            int voucherSerialNo = int.Parse(voucherNoParts[1]);
            IList<Record> records = _recordRepository.Get(r => r.VoucherType == voucherType && r.VoucherSerialNo == voucherSerialNo).ToList();

            amount = records.Sum(r => r.Debit);

            return records;
        }

        public static DateTime GetDateAt12Am(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day);
        }

        public void DeleteVoucher(string voucherNumber)
        {
            string voucherType;
            int voucherSerialNo;
            if (VoucherNoUtils.TryParse(voucherNumber, out voucherType, out voucherSerialNo))
            {
                IList<Record> deletableItems =
                    _recordRepository.Get(r => r.VoucherType == voucherType && r.VoucherSerialNo == voucherSerialNo).
                        ToList();
                int count = 0;
                foreach (Record deletableItem in deletableItems)
                {
                    _recordRepository.Delete(deletableItem);
                    count++;
                }

                if (count > 0 && _recordRepository.Save() > 0)
                {
                    InvokeManagerEvent("Voucher " + voucherNumber + " deleted successfully.");
                }
            }
            else
            {
                InvokeManagerEvent("Invalid Voucher Number : " + voucherNumber);
            }
        }
    }
}
