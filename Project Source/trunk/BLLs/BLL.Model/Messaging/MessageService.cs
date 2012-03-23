using System.Collections.Generic;
using BLL.ConfigurationManager;

namespace BLL.Model.Messaging
{
    public class MessageService
    {
        private string _errorColorCode;
        private string _successColorCode;
        private string _warningColorCode;
        private string _informationColorCode;

        private Dictionary<string, Message> _messages;

        private MessageService()
        {
            _errorColorCode = ConfigValues.ErrorColorCode;
            _successColorCode = ConfigValues.SuccessColorCode;
            _warningColorCode = ConfigValues.WarningColorCode;
            _informationColorCode = ConfigValues.InformationColorCode;

            _messages = new Dictionary<string, Message>();
        }

        private static MessageService _instance;
        public static MessageService Instance
        {
            get { return _instance ?? (_instance = new MessageService()); }
        }


        private void AddAllDefaultMessage()
        {
            _messages[Messages.TemporaryRecordsAdded.ToString()] = GetMessage(ConfigValues.TemporaryRecordsAdded,
                                                                              MessageType.Success);


        }

        private Message GetMessage(string text, MessageType type)
        {
            return new Message
                       {
                           IsRead = false,
                           MessageText = text,
                           MessageType = type
                       };
        }







        //public static Message GetErrorMessage(string message)
        //{
        //    Message invalidCause = new Message();
        //    invalidCause.MessageText = message;
        //    invalidCause.MessageType = MessageType.Error;
        //    //invalidCause.IsValidMessage = true;
        //    return invalidCause;
        //}

        //public static string GetColorCode(MessageType type)
        //{
        //    string colorCode;
        //    switch (type)
        //    {
        //        case MessageType.Success:
        //            colorCode = "#FF82D457"; // Green
        //            break;
        //        case MessageType.Error:
        //            colorCode = "#FFFF2B2B"; // Red
        //            break;
        //        case MessageType.Warning:
        //            colorCode = "#FFCECE12"; // TODO: this will be Yellow color code
        //            break;
        //        default:
        //            colorCode = "#00000000";
        //            break;
        //    }
        //    return colorCode;
        //}

    }
}


