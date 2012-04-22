
using BLL.Model.Repositories;

namespace BLL.Model.Schema
{
    public class CreditVoucher : VoucherBase
    {
        public CreditVoucher(IRecordRepository recordRepository) : base(recordRepository)
        {
        }

        public override string VoucherTypeKey
        {
            get { return Constants.CreditVoucherShortKey; }
        }

        public override double Debit
        {
            get { return 0; }
        }

        public override double Credit
        {
            get { return Amount; }
        }
    }
}


