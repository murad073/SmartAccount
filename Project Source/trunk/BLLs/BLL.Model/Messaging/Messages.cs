using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Model.Messaging
{
    public enum Messages
    {
        TemporaryRecordsAdded,
        NoProjectSelected,
        NoHeadSelected,
        AmountCannotBeZero,
        ContraTypeIsNotSelected,
        JVDebitOrCreditNotSelected ,
        NoFixedAssetParticularNameFound ,
        ZeroDepreciationProvidedForFixedAsset ,
        NoChequeOrBankInfo ,
        UnknownProblemArise ,
        VoucherPostedSuccessfully 
    }
}
