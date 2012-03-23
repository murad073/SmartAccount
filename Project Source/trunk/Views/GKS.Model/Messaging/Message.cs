using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GKS.Model.Messaging
{
    public class Message
    {
        public string MessageText;
        public MessageType MessageType = MessageType.Information;
        public bool IsValidMessage = false;
    }
}
