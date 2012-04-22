
using BLL.Model.Repositories;

namespace BLL.Model.Schema
{
    public class DebitVoucher : VoucherBase
    {
        public DebitVoucher(IRecordRepository recordRepository)
            : base(recordRepository)
        { }

        public override string VoucherTypeKey
        {
            get { return Constants.DebitVoucherShortKey; }
        }

        public override double Debit
        {
            get { return Amount; }
        }

        public override double Credit
        {
            get { return 0; }
        }
    }
}
