using System.Collections.Generic;
using System.Linq;
using BLL.Model;
using BLL.Model.Entity;
using BLL.Model.Managers;
using BLL.Model.Repositories;

namespace BLL.ProjectManagement
{
    public class HeadManager : ManagerBase, IHeadManager
    {
        private readonly IRepository<ProjectHead> _projectHeadRepository;
        private readonly IRepository<Head> _headRepository;

        public HeadManager(IRepository<ProjectHead> projectHeadRepository, IRepository<Head> headRepository)
        {
            _projectHeadRepository = projectHeadRepository;
            _headRepository = headRepository;
        }

        public IList<Head> GetHeads(bool isCashOrBankIncluded = true, bool bringInactive = true)
        {
            IList<Head> heads = _headRepository.GetAll().ToList();
            heads = bringInactive ? heads.OrderBy(h => h.Name).ToList() : heads.Where(h => h.IsActive).OrderBy(h => h.Name).ToList();

            if (!isCashOrBankIncluded)
                return heads.Where(p => p.Name != "Cash Book" && p.Name != "Bank Book").ToList();
            return heads;
        }

        public IList<Head> GetHeads(Project project, bool isCashOrBankIncluded = true, bool bringInactive = true)
        {
            IList<ProjectHead> headQuery = _projectHeadRepository.Get(ph => ph.Project.ID == project.ID).ToList();
            //TODO: Nazmul direct referencing is not working. ID is working...  ph => ph.Project.ID == project.ID
            IList<Head> heads = headQuery.Select(ph => ph.Head).ToList();

            heads = bringInactive ? heads.OrderBy(h => h.Name).ToList() : heads.Where(h => h.IsActive).OrderBy(h => h.Name).ToList();

            if (!isCashOrBankIncluded)
                return heads.Where(p => p.Name != "Cash Book" && p.Name != "Bank Book").ToList();
            return heads;
        }

        public bool Add(Head head)
        {
            Head existingHead = _headRepository.GetSingle(h => h.Name == head.Name);

            if (existingHead != null)
            {
                InvokeManagerEvent(new BLLEventArgs { EventType = EventType.Error, MessageKey = "HeadAlreadyExists", Parameters = new Dictionary<string, string> { { "HeadName", head.Name } } });
                return false;
            }
            _headRepository.Insert(head);
            if (_headRepository.Save() > 0)
            {
                InvokeManagerEvent(new BLLEventArgs
                                       {
                                           EventType = EventType.Success,
                                           MessageKey = "NewHeadSuccessfullyCreated",
                                           Parameters = new Dictionary<string, string> { { "HeadName", head.Name } }
                                       });
                return true;
            }
            //TODO: possible bug
            return false;
        }

        public bool Update(Head head)
        {
            //Head existingHead = _projectHeadRepository.Get(head.Name);

            //if (existingHead != null)
            //{
                _headRepository.Update(head);
                if (_headRepository.Save() > 0)
                {
                    InvokeManagerEvent(new BLLEventArgs
                                           {
                                               EventType = EventType.Success,
                                               MessageKey = "HeadSuccessfullyUpdated",
                                               Parameters = new Dictionary<string, string> {{"HeadName", head.Name}}
                                           });
                    return true;
                }
            //}
            InvokeManagerEvent(new BLLEventArgs { EventType = EventType.Error, MessageKey = "HeadUpdatedFailed", Parameters = new Dictionary<string, string> { { "HeadName", head.Name } } });
            return false;
        }
    }
}


