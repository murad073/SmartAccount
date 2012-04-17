using System;
using System.Collections.Generic;
using BLL.ConfigurationManager;
using BLL.Model;
using System.Linq;

namespace BLL.Messaging
{
    public class MessageService
    {

        private MessageService()
        {
        }

        private static readonly MessageService _instance = new MessageService();
        public static MessageService Instance
        {
            get { return _instance; }
        }

        public static IList<Message> MessageQueue = new List<Message>();

        public Message GetLatestMessage()
        {
            if (MessageQueue.Count > 0)
            {
                Message lastMessage = MessageQueue.Last();
                MessageQueue.Remove(lastMessage);
                return lastMessage;
            }
            return new Message { MessageText = "", MessageType = MessageType.None };
        }

        public void ManagerEventHandler(object sender, BLLEventArgs eventArgs)
        {
            if (!IsEventForMessage(eventArgs)) return;

            string messageText = ConfigValues.Get(eventArgs.MessageKey);

            if (eventArgs.Parameters != null && eventArgs.Parameters.Count > 0)
            {
                foreach (var pair in eventArgs.Parameters)
                {
                    messageText = messageText.Replace("${" + pair.Key + "}$", pair.Value);
                }
            }
            Message newMessage = new Message
                                     {
                                         IsRead = false,
                                         MessageText = messageText,
                                         MessageType = ConvertToMessageType(eventArgs.EventType)
                                     };
            MessageQueue.Add(newMessage);
        }

        private static bool IsEventForMessage(BLLEventArgs eventArgs)
        {
            if (string.IsNullOrWhiteSpace(eventArgs.MessageKey))
                return false;
            if (!new[] { EventType.Error, EventType.Information, EventType.Success, EventType.Warning }.Contains(eventArgs.EventType))
                return false;
            return true;
        }

        public void Reset()
        {
            MessageQueue.Clear();
        }

        private static MessageType ConvertToMessageType(EventType eventType)
        {
            switch (eventType)
            {
                case EventType.Success:
                    return MessageType.Success;
                case EventType.Error:
                    return MessageType.Error;
                case EventType.Warning:
                    return MessageType.Warning;
                case EventType.Information:
                    return MessageType.Information;
                default:
                    throw new ArgumentOutOfRangeException("eventType");
            }
        }

        public Message Get(string key, MessageType messageType)
        {
            return CreateMessageInstance(ConfigValues.Get(key), messageType);
        }

        public string GetColorCode(MessageType messageType)
        {
            string key = "";

            switch (messageType)
            {
                case MessageType.Error:
                    key = "ErrorColorCode";
                    break;
                case MessageType.Information:
                    key = "InformationColorCode";
                    break;
                case MessageType.Success:
                    key = "SuccessColorCode";
                    break;
                case MessageType.Warning:
                    key = "WarningColorCode";
                    break;
            }
            return ConfigValues.Get(key);
        }

        private static Message CreateMessageInstance(string text, MessageType type)
        {
            return new Message
                       {
                           IsRead = false,
                           MessageText = text,
                           MessageType = type
                       };
        }
    }
}


