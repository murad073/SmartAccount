namespace BLL.Messaging
{
    public enum ErrorMessage
    {
        NoProjectSelected,
        NoHeadSelected,
        AmountCannotBeZero,
        ContraTypeIsNotSelected,
        JVDebitOrCreditNotSelected,
        UnknownProblemArise,
        InvalidProject,
        InvalidHeadForProject,
        DebitOrCreditAmountIsInvalid,
        VoucherBalanceIsNotZero
    }

    public enum SuccessMessage
    {
        //TemporaryRecordsAdded,
        VoucherPostedSuccessfully
    }

    public enum WarningMessage
    {
        NoFixedAssetParticularNameFound,
        ZeroDepreciationProvidedForFixedAsset
    }

    public enum InformationMessage
    {
        NoChequeOrBankInfo
    }
}
