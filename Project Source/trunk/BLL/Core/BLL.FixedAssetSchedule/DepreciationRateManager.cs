using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.Model.Managers;
using BLL.Model.Entity;
using BLL.Model.Repositories;
using BLL.Model;

namespace BLL.FixedAssetSchedule
{
    public class DepreciationRateManager : ManagerBase, IDepreciationRateManager
    {
        private readonly IRepository<DepreciationRate> _depreciationRateRepository;
        private readonly IRepository<ProjectHead> _projectHeadRepository;

        public DepreciationRateManager(IRepository<DepreciationRate> depreciationRate, IRepository<ProjectHead> projectHeadRepository)
        {
            _depreciationRateRepository = depreciationRate;
            _projectHeadRepository = projectHeadRepository;
        }

        public IList<DepreciationRate> GetDepreciationRates()
        {
            IList<DepreciationRate> depreciationRates = _depreciationRateRepository.GetAll().ToList();

            return depreciationRates;
        }

        public bool Set(string projectName, string headName, double rate)
        {
            ProjectHead projectHead = _projectHeadRepository.GetSingle(ph => ph.Head.Name == headName && ph.Project.Name == projectName);

            bool update = false;
            if (projectHead != null && projectHead.DepreciationRates != null)
            {
                DepreciationRate depreciationRate = projectHead.DepreciationRates.SingleOrDefault(b => b.IsActive);
                if (depreciationRate != null)
                {
                    depreciationRate.Rate = rate;
                    update = true;
                }
                else
                {
                    depreciationRate = new DepreciationRate
                    {
                        Rate = rate,
                        IsActive = true,
                        ProjectHead = projectHead
                    };
                }

                return InsertOrUpdateNewDepreciationRate(depreciationRate, update);
            }
            else
                return false;
        }

        private bool InsertOrUpdateNewDepreciationRate(DepreciationRate depreciationRate, bool update)
        {
            if (update)
                _depreciationRateRepository.Update(depreciationRate);
            else
                _depreciationRateRepository.Insert(depreciationRate);

            if (_depreciationRateRepository.Save() > 0)
            {
                InvokeManagerEvent(new BLLEventArgs { EventType = EventType.Success, MessageKey = "NewDepreciationRateSavedSuccessfully" });
                return true;
            }

            InvokeManagerEvent(EventType.Error, "NewDepreciationRateInsertFailed");
            return false;
        }
    }
}
