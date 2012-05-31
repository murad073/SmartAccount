using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.Model.Entity;

namespace BLL.Model.Managers
{
    public interface IVoucherManager
    {
        DateTime VoucherStartDate { get; set; }
        DateTime VoucherEndDate { get; set; }
        bool Validate(Project project, DateTime startDate, DateTime endDate);
        Record GetNextRecord(int id);
        IList<Record> GetVouchers(Project project, string voucherType);
        IList<Record> GetVouchers(string voucherNo);
        void DeleteVoucher(string voucherNumber);
    }
}
