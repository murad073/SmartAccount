using System.Collections.Generic;
using System.Linq;
using BLL.Model;
using BLL.Messaging;
using BLL.Model.Repositories;
using BLL.Model.Schema;

namespace BLL.ProjectManagement
{
    public class HeadManager
    {
        private readonly IHeadRepository _headRepository;
        private  Message _latestMessage;
        public HeadManager(IHeadRepository headRepository)
        {
            _headRepository = headRepository;
            _latestMessage = new Message();
        }

        public IList<Head> GetHeads(bool isCashOrBankIncluded = true, bool bringInactive = true)
        {
            IList<Head> heads = _headRepository.GetAll();
            if (heads == null) return new List<Head>();

            heads = bringInactive ? heads.OrderBy(h => h.Name).ToList() : heads.Where(h=>h.IsActive).OrderBy(h => h.Name).ToList();

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
                _latestMessage = MessageService.Instance.Get("HeadAlreadyExists", MessageType.Error);
                _latestMessage.MessageText = string.Format(_latestMessage.MessageText, head.Name);
                return false;
            }
            Head insertedHead = _headRepository.Insert(head);
            _latestMessage = MessageService.Instance.Get("NewHeadSuccessfullyCreated", MessageType.Success);
            _latestMessage.MessageText = string.Format(_latestMessage.MessageText, insertedHead.Name);
            return true; 
        }

        public bool Update(Head head)
        {
            Head existingHead = _headRepository.Get(head.Name);

            if (existingHead != null)
            {
                _headRepository.Update(head);
                _latestMessage = MessageService.Instance.Get("HeadSuccessfullyUpdated", MessageType.Success);
                _latestMessage.MessageText = string.Format(_latestMessage.MessageText, head.Name);
                return true;
            }
            _latestMessage = MessageService.Instance.Get("HeadUpdatedFailed", MessageType.Error);
            _latestMessage.MessageText = string.Format(_latestMessage.MessageText, head.Name);
            return false;
        }

        public Message GetLatestMessage()
        {
            return _latestMessage;
        }
    }
}


