using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Utils
{
    public static class MoneyToTextUtils
    {
        public static string NumberToText(string number)
        {
            StringBuilder tempText = new StringBuilder("");

            string[] oneArray = { " ", "One ", "Two ", "Three ", "Four ", "Five ", "Six ", "Seven ", "Eight ", "Nine " };
            string[] tenArray = { "Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ", "Fifteen ", "Sixteen ", "Seventeen ", "Eighteen ", "Nineteen " };
            string[] twentyArray = { " ", " ", "Twenty ", "Thirty ", "Fourty ", "Fifty ", "Sixty ", "Seventy ", "Eighty ", "Ninety " };
            string[] bigUnitArray = { "Trillion ", "Billion ", "Million ", "Thounsand ", "Hundred " };

            int MaxUnit = bigUnitArray.Length - 1;

            int i; //for only_number
            int j; //for tempString
            int rem;

            int sizeNumber = number.Length;
            int tFlag = 0;

            for (i = 0, j = 0; i < sizeNumber; i++)
            {
                rem = (sizeNumber - i) % 3;
                if (rem == 0) tFlag = 0;
                if (number[i] != '0')
                {
                    if (rem == 1) //oneth position
                    {
                        tempText.Append(oneArray[number[i] - 48]);
                        tFlag = 1;
                    }
                    else if (rem == 2) //in tenth position
                    {
                        if (number[i] == '1')
                            tempText.Append(tenArray[number[++i] - 48]);
                        else
                            tempText.Append(twentyArray[number[i] - 48]);
                        tFlag = 1;
                    }
                    else if (rem == 0) // in hundredth position
                    {
                        tempText.Append(oneArray[number[i] - 48]);
                        tempText.Append(bigUnitArray[MaxUnit]);
                        tFlag = 1;
                    }
                }
                if (tFlag == 1)
                {
                    switch (sizeNumber - i)
                    {
                        case 13: tempText.Append(bigUnitArray[MaxUnit - 4]); break;
                        case 10: tempText.Append(bigUnitArray[MaxUnit - 3]); break;
                        case 7: tempText.Append(bigUnitArray[MaxUnit - 2]); break;
                        case 4: tempText.Append(bigUnitArray[MaxUnit - 1]); break;
                    }
                }
            }
            return tempText.ToString();
        }

        public static string NumberToTextInLacCrore(string number)
        {
            string revNumber = StringReverse(number.Trim());
            StringBuilder tempText = new StringBuilder("");

            string[] oneArray = { " ", "One ", "Two ", "Three ", "Four ", "Five ", "Six ", "Seven ", "Eight ", "Nine " };
            string[] tenArray = { "Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ", "Fifteen ", "Sixteen ", "Seventeen ", "Eighteen ", "Nineteen " };
            string[] twentyArray = { " ", " ", "Twenty ", "Thirty ", "Fourty ", "Fifty ", "Sixty ", "Seventy ", "Eighty ", "Ninety " };
            string[] bigUnitArray = { "Crore ", "Lac ", "Thounsand ", "Hundred " };

            if (revNumber.Length == 1)
            {
                if (revNumber[0] - 48 == 0) return "Zero";
                return tempText.Append(oneArray[revNumber[0] - 48]).ToString();
            }

            try                 ///// Crorer position
            {
                string Crore = "";
                if (revNumber.Length > 8) Crore = StringReverse(revNumber.Substring(7, 2));
                else Crore = StringReverse(revNumber.Substring(7, 1));

                string appStr = NumberToText(Crore);
                if (appStr.Trim() != "")
                {
                    tempText.Append(appStr);
                    tempText.Append(bigUnitArray[0]);
                }
            }
            catch { }
            try
            {
                string Lac = "";
                if (revNumber.Length > 6) Lac = StringReverse(revNumber.Substring(5, 2));
                else Lac = StringReverse(revNumber.Substring(5, 1));
                string appStr = NumberToText(Lac);
                if (appStr.Trim() != "")
                {
                    tempText.Append(appStr);
                    tempText.Append(bigUnitArray[1]);
                }
            }
            catch { }
            try
            {
                string Thousand = "";
                if (revNumber.Length > 4) Thousand = StringReverse(revNumber.Substring(3, 2));
                else Thousand = StringReverse(revNumber.Substring(3, 1));
                string appStr = NumberToText(Thousand);
                if (appStr.Trim() != "")
                {
                    tempText.Append(appStr);
                    tempText.Append(bigUnitArray[2]);
                }
            }
            catch { }
            try
            {
                char c2 = revNumber[2];
                string appStr = oneArray[c2 - 48];
                if (appStr.Trim() != "")
                {
                    tempText.Append(appStr);
                    tempText.Append(bigUnitArray[3]);
                }
            }
            catch { }
            try
            {
                string twoDigitNumber = StringReverse(revNumber.Substring(0, 2));
                string appStr = NumberToText(twoDigitNumber);
                if (appStr.Trim() != "")
                {
                    tempText.Append(appStr);
                }
            }
            catch { }

            if (tempText.ToString().Trim() == "") tempText = new StringBuilder("Zero");

            return tempText.ToString();
        }

        public static string StringReverse(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
    }
}
