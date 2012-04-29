using System.Collections.Generic;
using BLL.Model.Entity;
using BLL.Model.Schema;

namespace BLL.Model.Managers
{
    public interface IMassVoucherManager
    {
        bool Set(MassVoucher massVoucher);
        IList<Record> GetEntryableRecords();
        int GetNewVoucherNo(string voucherType, Project project);
    }
}