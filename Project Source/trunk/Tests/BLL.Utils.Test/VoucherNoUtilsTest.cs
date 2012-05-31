using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BLL.Utils.Test
{
    [TestClass]
    public class VoucherNoUtilsTest
    {
        [TestMethod]
        public void GetVoucherNumberTestByKeyAndNumber()
        {
            // count = 20
            string[] keys = new[]
                                {
                                    "DV", // 1
                                    "dv", // 2
                                    "dV", // 3
                                    "Dv", // 4
                                    "CV", // 5
                                    "cV", // 6
                                    "Cv", // 7
                                    "cv", // 8
                                    "c", // 9
                                    "C", // 10
                                    "contra", // 11
                                    "Contra", // 12
                                    "conTRA", // 13
                                    "JV", // 14
                                    "jV", // 15
                                    "Jv", // 16
                                    "jv", // 17
                                    "", // 18
                                    null, // 19
                                    "2" // 20
                                };
            int[] numbers = new[]
                                {
                                    1, // 1
                                    2, // 2
                                    0, // 3
                                    3, // 4
                                    99999, // 5
                                    -9009, // 6
                                    -16677, // 7
                                    1001001, // 8
                                    99, // 9
                                    5999999, // 10
                                    0, // 11
                                    505050, // 12
                                    -10, // 13
                                    898989899, // 14
                                    4, // 15
                                    -90, //16
                                    0, // 17
                                    0, //18
                                    99228877, // 19
                                    -9991 // 20
                                };
            string[] expectedNotEqual = new[]
                                            {
                                                "DV", // 1
                                                "dv", // 2
                                                "", // 3
                                                "Dv", // 4
                                                "CV", // 5
                                                "cV--9009", // 6
                                                "Cv-16677", // 7
                                                "cv", // 8
                                                "c", // 9
                                                "C", // 10
                                                "", // 11
                                                "Contra", // 12
                                                "conTRA--10", // 13
                                                "", // 14
                                                "jV", // 15
                                                "Jv-90", // 16
                                                "jv", // 17
                                                "-0", // 18
                                                "-99228877", // 19
                                                "2--9991" // 20
                                            };
            string[] expectedEqual = new[]
                                         {
                                             "DV-1", // 1
                                             "dv-2", // 2
                                             "dV-0", // 3
                                             "Dv-3", // 4
                                             "CV-99999", // 5
                                             "", // 6
                                             "", // 7
                                             "cv-1001001", // 8
                                             "c-99", // 9
                                             "C-5999999", // 10
                                             "contra-0", // 11
                                             "Contra-505050", // 12
                                             "", // 13
                                             "JV-898989899", // 14
                                             "jV-4", // 15
                                             "", // 16
                                             "jv-0", // 17
                                             "", // 18
                                             "", // 19
                                             "" // 20
                                         };

            for (int i = 0; i < keys.Length; i++)
            {
                string actual = VoucherNoUtils.GetVoucherNumber(keys[i], numbers[i]);
                Assert.AreEqual(expectedEqual[i], actual, "TestMethod GetVoucherNumberTestByKeyAndNumber with Assert.AreEqual() at index " + i);
                Assert.AreNotEqual(expectedNotEqual[i], actual, "TestMethod GetVoucherNumberTestByKeyAndNumber with Assert.AreNotEqual() at index " + i);
            }
        }

        [TestMethod]
        public void TestTryParseWithSeveralData()
        {
            string[] voucherNumbers = new[]
                                          {
                                              "DV-1", //1
                                              "DV1", //2
                                              "Contra-9900", //3
                                              "990099", //4
                                              "J--9", //5
                                              "-9", //6
                                              "CV-", //7
                                              "", //8
                                              "CV-9-", //9
                                              "DV-10190986", //10
                                              "CV-0", //11
                                              "C-99999999" //12
                                          };
            bool[] expectedCanParsed = new[]
                                           {
                                               true, //1
                                               false, //2
                                               true, //3
                                               false, //4
                                               false, //5
                                               false, //6
                                               false, //7
                                               false, //8
                                               false, //9
                                               true, //10
                                               true, //11
                                               true //12
                                           };
            string[] expectedVoucherTypes = new[]
                                                {
                                                    "DV", //1
                                                    "", //2
                                                    "Contra", //3
                                                    "", //4 
                                                    "", //5
                                                    "", //6
                                                    "", //7
                                                    "", //8
                                                    "", //9
                                                    "DV", //10
                                                    "CV", //11
                                                    "C" //12
                                                };
            int[] expectedVoucherSerialNo = new[]
                                                {
                                                    1, //1
                                                    0, //2
                                                    9900, //3
                                                    0, //4
                                                    0, //5
                                                    0, //6
                                                    0, //7
                                                    0, //8
                                                    0, //9
                                                    10190986, //10
                                                    0, //11
                                                    99999999 //12
                                                };

            for (int i = 0; i < voucherNumbers.Length; i++)
            {
                string actualVoucherType;
                int actualVoucherSerialNo;
                bool actualCanParsed = VoucherNoUtils.TryParse(voucherNumbers[i], out actualVoucherType, out actualVoucherSerialNo);

                Assert.AreEqual(expectedCanParsed[i], actualCanParsed, "TestMethod TestTryParseWithSeveralData for expectedCanParsed with index " + i);
                Assert.AreEqual(expectedVoucherTypes[i], actualVoucherType, "TestMethod TestTryParseWithSeveralData for expectedVoucherTypes with index " + i);
                Assert.AreEqual(expectedVoucherSerialNo[i], actualVoucherSerialNo, "TestMethod TestTryParseWithSeveralData for expectedVoucherSerialNo with index " + i);
            }
        }
    }
}

