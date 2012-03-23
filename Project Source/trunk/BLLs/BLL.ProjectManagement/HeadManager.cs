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
        private readonly Message _latestMessage;
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
                _latestMessage.MessageText = "Head '" + head.Name + "' already exists.";
                _latestMessage.MessageType = MessageType.Error;
                return false;
            }
            Head insertedHead = _headRepository.Insert(head);
            _latestMessage.MessageText = "New Account of head '" + insertedHead.Name + "' successfully created";
            _latestMessage.MessageType = MessageType.Success;
            return true; 
        }

        public bool Update(Head head)
        {
            Head existingHead = _headRepository.Get(head.Name);

            if (existingHead != null)
            {
                _headRepository.Update(head);
                _latestMessage.MessageText = "Account of head '" + head.Name + "' successfully updated";
                _latestMessage.MessageType = MessageType.Success;
                return true;
            }
            _latestMessage.MessageText = "The head '" + head.Name + "' is not available for update";
            _latestMessage.MessageType = MessageType.Error;
            return false;
        }

        public Message GetLatestMessage()
        {
            return _latestMessage;
        }
    }
}


