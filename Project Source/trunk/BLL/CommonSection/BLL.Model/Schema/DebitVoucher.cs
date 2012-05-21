
using BLL.Model.Entity;
using BLL.Model.Repositories;

namespace BLL.Model.Schema
{
    public class DebitVoucher : VoucherBase
    {
        public DebitVoucher()
        {
            
        }
        public DebitVoucher(IRepository<Record> recordRepository, IRepository<FixedAsset> fixedAssetRepository)
            : base(recordRepository, fixedAssetRepository)
        { }

        public override string VoucherTypeKey()
        {
            return Constants.DebitVoucherShortKey; 
        }

        public override void SetAmount(double amount)
        {
            Debit = amount;
        }

        //public override double Debit
        //{
        //    get { return Amount; }
        //}

        //public override double Credit
        //{
        //    get { return 0; }
        //}
    }
}
