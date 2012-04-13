namespace BLL.Messaging
{
    public class Message
    {
        public string MessageText { get; set; }
        public MessageType MessageType { get; set; }
        public bool IsRead { get; set; }
    }

    public enum MessageType
    {
        None,
        Success,
        Error,
        Warning,
        Information
    }
}
