
using BLL.Model.Message;

namespace BLL.Messaging
{
    public class MessageService
    {
        //public static bool IsValidVoucher(Voucher voucher, out Message invalidCause)
        //{
        //    invalidCause = new Message();
        //    bool isValidVoucher = true;
        //    if (voucher == null) return false;

        //    if (voucher.Project == null)
        //    {
        //        invalidCause = GetErrorMessage(MessageText.NoProjectSelected);
        //        isValidVoucher = false;
        //    }
        //    else if (voucher.VoucherType != "Contra" && voucher.Head == null)
        //    {
        //        invalidCause = GetErrorMessage(MessageText.NoHeadSelected);
        //        isValidVoucher = false;
        //    }
        //    else if (voucher.Amount == 0)
        //    {
        //        invalidCause = GetErrorMessage(MessageText.AmountCannotBeZero);
        //        isValidVoucher = false;
        //    }
        //    else if (voucher.VoucherType == "Contra" && string.IsNullOrWhiteSpace(voucher.ContraType))
        //    {
        //        invalidCause = GetErrorMessage(MessageText.ContraTypeIsNotSelected);
        //        isValidVoucher = false;
        //    }
        //    else if (voucher.VoucherType == "JV" )
        //    {
        //        if(string.IsNullOrWhiteSpace(voucher.JournalDebitOrCredit))
        //        {
        //            invalidCause = GetErrorMessage(MessageText.JVDebitOrCreditNotSelected);
        //            isValidVoucher = false;
        //        }
        //    }
        //    return isValidVoucher;
        //}

        public static Message GetErrorMessage(string message)
        {
            Message invalidCause = new Message();
            invalidCause.MessageText = message;
            invalidCause.MessageType = MessageType.Error;
            //invalidCause.IsValidMessage = true;
            return invalidCause;
        }
    }
}


