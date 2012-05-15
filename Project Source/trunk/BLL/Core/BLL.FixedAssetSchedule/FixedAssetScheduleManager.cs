using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.Model.Entity;
using BLL.Model.Repositories;

namespace BLL.FixedAssetSchedule
{
    public class FixedAssetScheduleManager
    {
        private readonly IRepository<FixedAsset> _fixedAssetRepository;
        private readonly IRepository<Record> _recordRepository;

        public FixedAssetScheduleManager(IRepository<FixedAsset> fixedAssetRepository, IRepository<Record> recordRepository)
        {
            _fixedAssetRepository = fixedAssetRepository;
            _recordRepository = recordRepository;
        }

        public IList<Record> GetFixedAssetSchedule(Project project)
        {
            return _fixedAssetRepository.GetAll().Select(fa => fa.Record).ToList();
        }
    }
}

