using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;


namespace BLL.ConfigurationManager
{
    public static class ConfigValues
    {
        private static readonly NameValueCollection AppSettings = System.Configuration.ConfigurationManager.AppSettings;

        //public static string ErrorColorCode
        //{
        //    get { return AppSettings["ErrorColorCode"] ?? "FFFF2B2B"; }
        //}

        //public static string SuccessColorCode
        //{
        //    get { return AppSettings["SuccessColorCode"] ?? "FF82D457"; }
        //}

        //public static string WarningColorCode
        //{
        //    get { return AppSettings["WarningColorCode"] ?? "FFCECE12"; }
        //}

        //public static string InformationColorCode
        //{
        //    get { return AppSettings["InformationColorCode"] ?? "00000000"; }
        //}

        //public static string TemporaryRecordsAdded
        //{
        //    get { return AppSettings["TemporaryRecordsAdded"] ?? "{0} Temporary Voucher(s) Added"; }
        //}

        //public static string NoProjectSelected
        //{
        //    get { return AppSettings["NoProjectSelected"] ?? "Project cannot be blank."; }
        //}

        //public static string NoHeadSelected
        //{
        //    get { return AppSettings["NoHeadSelected"] ?? "Account head cannot be blank."; }
        //}

        //public static string AmountCannotBeZero
        //{
        //    get { return AppSettings["AmountCannotBeZero"] ?? "Amount cannot be zero."; }
        //}

        //public static string ContraTypeIsNotSelected
        //{
        //    get { return AppSettings["ContraTypeIsNotSelected"] ?? "Contra type is not selected."; }
        //}

        //public static string JVDebitOrCreditNotSelected
        //{
        //    get { return AppSettings["JVDebitOrCreditNotSelected"] ?? "Please select the amount as Debit or Credit"; }
        //}

        //public static string NoFixedAssetParticularNameFound
        //{
        //    get { return AppSettings["NoFixedAssetParticularNameFound"] ?? "Please provide fixed asset particulars."; }
        //}

        //public static string ZeroDepreciationProvidedForFixedAsset
        //{
        //    get { return AppSettings["ZeroDepreciationProvidedForFixedAsset"] ?? "Zero depreciation provided for fixed asset."; }
        //}

        //public static string NoChequeOrBankInfo
        //{
        //    get { return AppSettings["NoChequeOrBankInfo"] ?? "Cheque no or bank name is blank." + Environment.NewLine + "Do you wish to continue?"; }
        //}

        //public static string UnknownProblemArise
        //{
        //    get { return AppSettings["UnknownProblemArise"] ?? "Unknown error occured. Please contact the Administrator."; }
        //}

        //public static string VoucherPostedSuccessfully
        //{
        //    get { return AppSettings["VoucherPostedSuccessfully"] ?? "Voucher posted successfully."; }
        //}

        public static string Get(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) return "";
            return AppSettings[key] ?? "";
        }
    }
}

