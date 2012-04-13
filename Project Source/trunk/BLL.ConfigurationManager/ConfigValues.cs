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

        public static string Get(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) return "";
            return AppSettings[key] ?? "";
        }
    }
}

