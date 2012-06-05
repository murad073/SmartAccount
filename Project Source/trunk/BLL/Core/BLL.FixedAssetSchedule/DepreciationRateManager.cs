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
        private readonly IRepository<DepreciationRate> _depreciationRate;
        private readonly IRepository<ProjectHead> _projectHeadRepository;

        public DepreciationRateManager(IRepository<DepreciationRate> depreciationRate, IRepository<ProjectHead> projectHeadRepository)
        {
            _depreciationRate = depreciationRate;
            _projectHeadRepository = projectHeadRepository;
        }

        public bool Set(string projectName, string headName, double rate)
        {
            ProjectHead projectHead = _projectHeadRepository.GetSingle(ph => ph.Head.Name == headName && ph.Project.Name == projectName);
            DepreciationRate depreciationRate = projectHead.DepreciationRates.SingleOrDefault(b => b.IsActive);
            
            bool update = depreciationRate != null;
            return InsertNewDepreciationRate(projectHead, rate, update);  
        }

        private bool InsertNewDepreciationRate(ProjectHead projectHead, double rate, bool update)
        {
            DepreciationRate newDepreciationRate = new DepreciationRate
            {
                Rate = rate,
                IsActive = true,
                ProjectHead = projectHead
            };

            if (update)
                _depreciationRate.Update(newDepreciationRate);
            else
                _depreciationRate.Insert(newDepreciationRate);

            if (_depreciationRate.Save() > 0)
            {
                InvokeManagerEvent(new BLLEventArgs { EventType = EventType.Success, MessageKey = "NewDepreciationRateSavedSuccessfully" });
                return true;
            }

            InvokeManagerEvent(EventType.Error, "NewDepreciationRateInsertFailed");
            return false;
        }
    }
}
