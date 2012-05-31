using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Utils
{
    public static class VoucherNoUtils
    {
        public static string GetVoucherNumber(string key, int number)
        {
            if (string.IsNullOrWhiteSpace(key) || number < 0) return "";
            return key + "-" + number;
        }

        public static bool TryParse(string voucherNumber, out string voucherType, out int voucherSerialNo)
        {
            voucherType = "";
            voucherSerialNo = 0;
            if (string.IsNullOrWhiteSpace(voucherNumber)) return false;

            if (voucherNumber.Contains('-') && voucherNumber.Count(c => c == '-') == 1)
            {
                string[] parts = voucherNumber.Split('-');
                string type = parts[0], serialNo = parts[1];

                if (!string.IsNullOrWhiteSpace(type) && int.TryParse(serialNo, out voucherSerialNo))
                {
                    voucherType = type;
                    return true;
                }
            }
            return false;
        }
    }
}


