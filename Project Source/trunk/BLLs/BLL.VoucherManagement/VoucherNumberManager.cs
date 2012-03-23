using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.DALInterface;

namespace BLL.VoucherManagement
{
    public class VoucherNumberManager
    {
        private readonly IVoucherManager _dalVoucherManager;

        public VoucherNumberManager(IVoucherManager dalVoucherManager)
        {
            _dalVoucherManager = dalVoucherManager;
        }

        public int GetNewVoucherNo(string key)
        {
            return _dalVoucherManager.GetMaxVoucherNo(key) + 1;
        }


    }
}

