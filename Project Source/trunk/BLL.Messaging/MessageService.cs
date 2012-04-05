using System.Collections.Generic;
using BLL.ConfigurationManager;

namespace BLL.Messaging
{
    public class MessageService
    {
        //private readonly Dictionary<string, Message> _messages;

        private MessageService()
        {
            //_messages = new Dictionary<string, Message>();
            //AttachAllDefaultMessage();
        }

        private static readonly MessageService _instance = new MessageService();
        public static MessageService Instance
        {
            get { return _instance; }
        }

        //private void AttachAllDefaultMessage()
        //{
        //    /**********  ERROR Messages ***************/
        //    Add(ErrorMessage.AmountCannotBeZero.ToString(), MessageType.Error);
        //    Add(ErrorMessage.ContraTypeIsNotSelected.ToString(), MessageType.Error);
        //    Add(ErrorMessage.JVDebitOrCreditNotSelected.ToString(), MessageType.Error);
        //    Add(ErrorMessage.NoHeadSelected.ToString(), MessageType.Error);
        //    Add(ErrorMessage.NoProjectSelected.ToString(), MessageType.Error);
        //    Add(ErrorMessage.UnknownProblemArise.ToString(), MessageType.Error);

        //    /**********  SUCCESS Messages ***************/
        //    Add(SuccessMessage.TemporaryRecordsAdded.ToString(), MessageType.Success);
        //    Add(SuccessMessage.VoucherPostedSuccessfully.ToString(), MessageType.Success);

        //    /**********  Warning Messages ***************/
        //    Add(WarningMessage.NoFixedAssetParticularNameFound.ToString(), MessageType.Warning);            

        //    /**********  Information Messages ***************/
        //    Add(InformationMessage.NoChequeOrBankInfo.ToString(), MessageType.Information);
        //    Add(InformationMessage.UnknownProblemArise.ToString(), MessageType.Information);
        //}

        //public void Add(string key, MessageType type, string text = "")
        //{
        //    if (string.IsNullOrWhiteSpace(text)) text = ConfigValues.Get(key);
        //    Message message = CreateMessageInstance(text, type);
        //    if (_messages.ContainsKey(key))
        //        _messages[key] = message;
        //    else
        //        _messages.Add(key, message);
        //}

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


