using System.Collections.Generic;
using BLL.ConfigurationManager;

namespace BLL.Messaging
{
    public class MessageService
    {
        private readonly Dictionary<string, Message> _messages;

        private MessageService()
        {
            _messages = new Dictionary<string, Message>();
            AttachAllDefaultMessage();
        }

        private static MessageService _instance;
        public static MessageService Instance
        {
            get { return _instance ?? (_instance = new MessageService()); }
        }

        private void AttachAllDefaultMessage()
        {
            /**********  ERROR Messages ***************/
            Add(ErrorMessage.AmountCannotBeZero.ToString(), MessageType.Error);
            Add(ErrorMessage.ContraTypeIsNotSelected.ToString(), MessageType.Error);
            Add(ErrorMessage.JVDebitOrCreditNotSelected.ToString(), MessageType.Error);
            Add(ErrorMessage.NoHeadSelected.ToString(), MessageType.Error);
            Add(ErrorMessage.NoProjectSelected.ToString(), MessageType.Error);
            Add(ErrorMessage.UnknownProblemArise.ToString(), MessageType.Error);

            /**********  SUCCESS Messages ***************/
            Add(SuccessMessage.TemporaryRecordsAdded.ToString(), MessageType.Success);
            Add(SuccessMessage.VoucherPostedSuccessfully.ToString(), MessageType.Success);

            /**********  Information Messages ***************/
            Add(InformationMessage.NoChequeOrBankInfo.ToString(), MessageType.Information);
            Add(InformationMessage.UnknownProblemArise.ToString(), MessageType.Information);
        }

        public void Add(string key, MessageType type, string text = "")
        {
            if (string.IsNullOrWhiteSpace(text)) text = ConfigValues.Get(key);
            Message message = CreateMessageInstance(text, type);
            if (_messages.ContainsKey(key))
                _messages[key] = message;
            else
                _messages.Add(key, message);
        }

        public Message Get(string key)
        {
            if( _messages.ContainsKey(key))
            {
                return _messages[key];
            }
            return null;
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


