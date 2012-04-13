using System;
using BLL.Model.Repositories;

namespace BLL.Model.Schema
{
    public class VoucherFactory
    {
        public static VoucherBase NewVoucher(IRecordRepository recordRepository, string key, string type = "")
        {
            VoucherBase voucher = default(VoucherBase);
            switch (key)
            {
                case Constants.DebitVoucherShortKey:
                    voucher = new DebitVoucher(recordRepository);
                    break;
                case Constants.CreditVoucherShortKey:
                    voucher = new CreditVoucher(recordRepository);
                    break;
                case Constants.JournalVoucherShortKey:
                    if(string.IsNullOrWhiteSpace(type)) throw new Exception("Type required for journal voucher.");
                    voucher = new JournalVoucher(recordRepository) { JVDebitOrCredit = type };
                    break;
                //case Constants.ContraVoucherShortKey:
                //    if(string.IsNullOrWhiteSpace(type)) throw new Exception("Type required for contra voucher");
                //    //voucher = new Contra { ContraType = type };
                //    break;
                default:
                    throw new Exception("Invalid Voucher Key. No voucher is defined for the key - " + key);
            }
            return voucher;
        }
    }
}

