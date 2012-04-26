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
        private readonly IRepository<Head> _headRepository;
        public HeadManager(IRepository<Head> headRepository)
        {
            _headRepository = headRepository;
        }

        public IList<Head> GetHeads(bool isCashOrBankIncluded = true, bool bringInactive = true)
        {
            IList<Head> heads = _headRepository.GetAll();
            if (heads == null) return new List<Head>();

            heads = bringInactive ? heads.OrderBy(h => h.Name).ToList() : heads.Where(h => h.IsActive).OrderBy(h => h.Name).ToList();

            if (!isCashOrBankIncluded)
                return heads.Where(p => p.Name != "Cash Book" && p.Name != "Bank Book").ToList();
            return heads;
        }

        public IList<Head> GetHeads(int projectId, bool isCashOrBankIncluded = true, bool bringInactive = true)
        {
            IList<Head> heads = _headRepository.GetAll(projectId);
            if (heads == null) return new List<Head>();

            heads = bringInactive ? heads.OrderBy(h => h.Name).ToList() : heads.Where(h => h.IsActive).OrderBy(h => h.Name).ToList();

            if (!isCashOrBankIncluded)
                return heads.Where(p => p.Name != "Cash Book" && p.Name != "Bank Book").ToList();
            return heads;
        }

        public bool Add(Head head)
        {
            Head existingHead = _headRepository.Get(head.Name);

            if (existingHead != null)
            {
                InvokeManagerEvent(new BLLEventArgs { EventType = EventType.Error, MessageKey = "HeadAlreadyExists", Parameters = new Dictionary<string, string> { { "HeadName", head.Name } } });
                return false;
            }
            Head insertedHead = _headRepository.Insert(head);
            InvokeManagerEvent(new BLLEventArgs { EventType = EventType.Success, MessageKey = "NewHeadSuccessfullyCreated", Parameters = new Dictionary<string, string> { { "HeadName", insertedHead.Name } } });
            return true;
        }

        public bool Update(Head head)
        {
            Head existingHead = _headRepository.Get(head.Name);

            if (existingHead != null)
            {
                _headRepository.Update(head);
                InvokeManagerEvent(new BLLEventArgs { EventType = EventType.Success, MessageKey = "HeadSuccessfullyUpdated", Parameters = new Dictionary<string, string> { { "HeadName", head.Name } } });
                return true;
            }
            InvokeManagerEvent(new BLLEventArgs { EventType = EventType.Error, MessageKey = "HeadUpdatedFailed", Parameters = new Dictionary<string, string> { { "HeadName", head.Name } } });
            return false;
        }


    }
}


