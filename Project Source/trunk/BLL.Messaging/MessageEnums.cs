namespace BLL.Messaging
{
    public enum ErrorMessage
    {
        NoProjectSelected,
        NoHeadSelected,
        AmountCannotBeZero,
        ContraTypeIsNotSelected,
        JVDebitOrCreditNotSelected,
        UnknownProblemArise
    }

    public enum SuccessMessage
    {
        TemporaryRecordsAdded,
        VoucherPostedSuccessfully
    }

    public enum WarningMessage
    {
    }

    public enum InformationMessage
    {
        NoChequeOrBankInfo,
        UnknownProblemArise
    }
}
