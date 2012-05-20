using BLL.Model.Entity;
using BLL.Model.Repositories;
using BLL.Utils;

namespace BLL.Model.Schema
{
    public abstract class VoucherBase : Record
    {
        protected VoucherBase()
        { }
        internal IRepository<FixedAsset> FixedAssetRepository;
        protected VoucherBase(IRepository<Record> recordRepository, IRepository<FixedAsset> fixedAssetRepository)
        {
            base.RecordRepository = recordRepository;
            FixedAssetRepository = fixedAssetRepository;
        }

        public bool IsFixedAsset;
        public FixedAsset FixedAsset;

        //public double Amount;

        public override string LedgerType
        {
            get { return "LedgerBook"; }
        }

        //public override bool IsValid()
        //{
        //    return true;
        //}

        public override bool Save()
        {
            RecordRepository.Insert(this);

            if (IsFixedAsset)
            {
                FixedAsset.Record = this;
                FixedAssetRepository.Insert(FixedAsset);
            }
            if (RecordRepository.Save() > 0 || FixedAssetRepository.Save() > 0)
                return true;
            RecordRepository.Discard();
            return false;
        }
    }
}


