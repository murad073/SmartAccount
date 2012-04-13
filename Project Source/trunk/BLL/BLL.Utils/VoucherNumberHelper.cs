using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Utils
{
    public static class VoucherNumberHelper
    {
        public static string GetVoucherNumber(string key, int number)
        {
            return key + "-" + number;
        }
    }
}
