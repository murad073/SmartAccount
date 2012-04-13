using BLL.Model.Repositories;
using BLL.Utils;

namespace BLL.Model.Schema
{
    public abstract class VoucherBase : Record
    {
        protected VoucherBase(IRecordRepository recordRepository)
            : base(recordRepository)
        {
        }

        public bool IsFixedAsset;
        public FixedAsset FixedAsset;

        public double Amount;

        //public override string VoucherNo
        //{
        //    get { return VoucherNumberHelper.GetVoucherNumber(VoucherTypeKey, VoucherSerialNo); }
        //}

        public override string LedgerType
        {
            get { return "LedgerBook"; }
        }

        public override bool IsValid()
        {
            return true;
        }

        public override bool Save()
        {
            if (IsFixedAsset)
                return RecordRepository.InsertLedgerBookRow(this, FixedAsset);
            return RecordRepository.InsertLedgerBookRow(this);
        }
    }
}

