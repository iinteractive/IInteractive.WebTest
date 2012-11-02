using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IInteractive.WebTest.UnitTests
{
    [TestClass]
    public class TestEncodingAssumptions
    {
        public static string[][] W3CEncodings = new string[][] {
	        new string[] { "emptyEncoding",	"(detect automatically)" },
	        new string[] { "UTF8",	"utf-8" },
	        new string[] { "UTF16",	"utf-16" },
	        new string[] { "iso_8859_1",	"iso-8859-1" },
	        new string[] { "iso_8859_2",	"iso-8859-2" },
	        new string[] { "iso_8859_3",	"iso-8859-3" },
	        new string[] { "iso_8859_4",	"iso-8859-4" },
	        new string[] { "iso_8859_5",	"iso-8859-5" },
	        new string[] { "iso_8859_6_i",	"iso-8859-6-i" },
	        new string[] { "iso_8859_7",	"iso-8859-7" },
	        new string[] { "iso_8859_8",	"iso-8859-8" },
	        new string[] { "iso_8859_8_i",	"iso-8859-8-i" },
	        new string[] { "iso_8859_9",	"iso-8859-9" },
	        new string[] { "iso_8859_10",	"iso-8859-10" },
	        new string[] { "iso_8859_11",	"iso-8859-11" },
	        new string[] { "iso_8859_13",	"iso-8859-13" },
	        new string[] { "iso_8859_14",	"iso-8859-14" },
	        new string[] { "iso_8859_15",	"iso-8859-15" },
	        new string[] { "iso_8859_16",	"iso-8859-16" },
	        new string[] { "us_ascii",	"us-ascii" },
	        new string[] { "euc_jp",	"euc-jp" },
	        new string[] { "shift_jis",	"shift_jis" },
	        new string[] { "iso_2022_jp",	"iso-2022-jp" },
	        new string[] { "euc_kr",	"euc-kr" },
	        new string[] { "gb2312",	"gb2312" },
	        new string[] { "gb18030",	"gb18030" },
	        new string[] { "big5",	"big5" },
	        new string[] { "big5_hkscs",	"big5-HKSCS" },
	        new string[] { "tis_620",	"tis-620" },
	        new string[] { "koi8_r",	"koi8-r" },
	        new string[] { "koi8_u",	"koi8-u" },
	        new string[] { "iso_ir_111",	"iso-ir-111" },
	        new string[] { "macintosh",	"macintosh" },
	        new string[] { "windows_1250",	"windows-1250" },
	        new string[] { "windows_1251",	"windows-1251" },
	        new string[] { "windows_1252",	"windows-1252" },
	        new string[] { "windows_1253",	"windows-1253" },
	        new string[] { "windows_1254",	"windows-1254" },
	        new string[] { "windows_1255",	"windows-1255" },
	        new string[] { "windows_1256",	"windows-1256" },
	        new string[] { "windows_1257",	"windows-1257" }
        };


        // Code Page, Name, Display Name
        public static string[][] EncodingTests = new string[][] { 
            new string[] {"37", "IBM037", "IBM EBCDIC (US-Canada)"},
            new string[] {"437", "IBM437", "OEM United States"},
            new string[] {"500", "IBM500", "IBM EBCDIC (International)"},
            new string[] {"708", "ASMO-708", "Arabic (ASMO 708)"},
            new string[] {"720", "DOS-720", "Arabic (DOS)"},
            new string[] {"737", "ibm737", "Greek (DOS)"},
            new string[] {"775", "ibm775", "Baltic (DOS)"},
            new string[] {"850", "ibm850", "Western European (DOS)"},
            new string[] {"852", "ibm852", "Central European (DOS)"},
            new string[] {"855", "IBM855", "OEM Cyrillic"},
            new string[] {"857", "ibm857", "Turkish (DOS)"},
            new string[] {"858", "IBM00858", "OEM Multilingual Latin I"},
            new string[] {"860", "IBM860", "Portuguese (DOS)"},
            new string[] {"861", "ibm861", "Icelandic (DOS)"},
            new string[] {"862", "DOS-862", "Hebrew (DOS)"},
            new string[] {"863", "IBM863", "French Canadian (DOS)"},
            new string[] {"864", "IBM864", "Arabic (864)"},
            new string[] {"865", "IBM865", "Nordic (DOS)"},
            new string[] {"866", "cp866", "Cyrillic (DOS)"},
            new string[] {"869", "ibm869", "Greek, Modern (DOS)"},
            new string[] {"870", "IBM870", "IBM EBCDIC (Multilingual Latin-2)"},
            new string[] {"874", "windows-874", "Thai (Windows)"},
            new string[] {"875", "cp875", "IBM EBCDIC (Greek Modern)"},
            new string[] {"932", "shift_jis", "Japanese (Shift-JIS)"},
            new string[] {"936", "gb2312", "Chinese Simplified (GB2312)"},
            new string[] {"949", "ks_c_5601-1987", "Korean"},
            new string[] {"950", "big5", "Chinese Traditional (Big5)"},
            new string[] {"1026", "IBM1026", "IBM EBCDIC (Turkish Latin-5)"},
            new string[] {"1047", "IBM01047", "IBM Latin-1"},
            new string[] {"1140", "IBM01140", "IBM EBCDIC (US-Canada-Euro)"},
            new string[] {"1141", "IBM01141", "IBM EBCDIC (Germany-Euro)"},
            new string[] {"1142", "IBM01142", "IBM EBCDIC (Denmark-Norway-Euro)"},
            new string[] {"1143", "IBM01143", "IBM EBCDIC (Finland-Sweden-Euro)"},
            new string[] {"1144", "IBM01144", "IBM EBCDIC (Italy-Euro)"},
            new string[] {"1145", "IBM01145", "IBM EBCDIC (Spain-Euro)"},
            new string[] {"1146", "IBM01146", "IBM EBCDIC (UK-Euro)"},
            new string[] {"1147", "IBM01147", "IBM EBCDIC (France-Euro)"},
            new string[] {"1148", "IBM01148", "IBM EBCDIC (International-Euro)"},
            new string[] {"1149", "IBM01149", "IBM EBCDIC (Icelandic-Euro)"},
            new string[] {"1200", "utf-16", "Unicode"},
            new string[] {"1201", "unicodeFFFE", "Unicode (Big endian)"},
            new string[] {"1250", "windows-1250", "Central European (Windows)"},
            new string[] {"1251", "windows-1251", "Cyrillic (Windows)"},
            new string[] {"1252", "Windows-1252", "Western European (Windows)"},
            new string[] {"1253", "windows-1253", "Greek (Windows)"},
            new string[] {"1254", "windows-1254", "Turkish (Windows)"},
            new string[] {"1255", "windows-1255", "Hebrew (Windows)"},
            new string[] {"1256", "windows-1256", "Arabic (Windows)"},
            new string[] {"1257", "windows-1257", "Baltic (Windows)"},
            new string[] {"1258", "windows-1258", "Vietnamese (Windows)"},
            new string[] {"1361", "Johab", "Korean (Johab)"},
            new string[] {"10000", "macintosh", "Western European (Mac)"},
            new string[] {"10001", "x-mac-japanese", "Japanese (Mac)"},
            new string[] {"10002", "x-mac-chinesetrad", "Chinese Traditional (Mac)"},
            new string[] {"10003", "x-mac-korean", "Korean (Mac)"},
            new string[] {"10004", "x-mac-arabic", "Arabic (Mac)"},
            new string[] {"10005", "x-mac-hebrew", "Hebrew (Mac)"},
            new string[] {"10006", "x-mac-greek", "Greek (Mac)"},
            new string[] {"10007", "x-mac-cyrillic", "Cyrillic (Mac)"},
            new string[] {"10008", "x-mac-chinesesimp", "Chinese Simplified (Mac)"},
            new string[] {"10010", "x-mac-romanian", "Romanian (Mac)"},
            new string[] {"10017", "x-mac-ukrainian", "Ukrainian (Mac)"},
            new string[] {"10021", "x-mac-thai", "Thai (Mac)"},
            new string[] {"10029", "x-mac-ce", "Central European (Mac)"},
            new string[] {"10079", "x-mac-icelandic", "Icelandic (Mac)"},
            new string[] {"10081", "x-mac-turkish", "Turkish (Mac)"},
            new string[] {"10082", "x-mac-croatian", "Croatian (Mac)"},
            new string[] {"12000", "utf-32", "Unicode (UTF-32)"},
            new string[] {"12001", "utf-32BE", "Unicode (UTF-32 Big endian)"},
            new string[] {"20000", "x-Chinese-CNS", "Chinese Traditional (CNS)"},
            new string[] {"20001", "x-cp20001", "TCA Taiwan"},
            new string[] {"20002", "x-Chinese-Eten", "Chinese Traditional (Eten)"},
            new string[] {"20003", "x-cp20003", "IBM5550 Taiwan"},
            new string[] {"20004", "x-cp20004", "TeleText Taiwan"},
            new string[] {"20005", "x-cp20005", "Wang Taiwan"},
            new string[] {"20105", "x-IA5", "Western European (IA5)"},
            new string[] {"20106", "x-IA5-German", "German (IA5)"},
            new string[] {"20107", "x-IA5-Swedish", "Swedish (IA5)"},
            new string[] {"20108", "x-IA5-Norwegian", "Norwegian (IA5)"},
            new string[] {"20127", "us-ascii", "US-ASCII"},
            new string[] {"20261", "x-cp20261", "T.61"},
            new string[] {"20269", "x-cp20269", "ISO-6937"},
            new string[] {"20273", "IBM273", "IBM EBCDIC (Germany)"},
            new string[] {"20277", "IBM277", "IBM EBCDIC (Denmark-Norway)"},
            new string[] {"20278", "IBM278", "IBM EBCDIC (Finland-Sweden)"},
            new string[] {"20280", "IBM280", "IBM EBCDIC (Italy)"},
            new string[] {"20284", "IBM284", "IBM EBCDIC (Spain)"},
            new string[] {"20285", "IBM285", "IBM EBCDIC (UK)"},
            new string[] {"20290", "IBM290", "IBM EBCDIC (Japanese katakana)"},
            new string[] {"20297", "IBM297", "IBM EBCDIC (France)"},
            new string[] {"20420", "IBM420", "IBM EBCDIC (Arabic)"},
            new string[] {"20423", "IBM423", "IBM EBCDIC (Greek)"},
            new string[] {"20424", "IBM424", "IBM EBCDIC (Hebrew)"},
            new string[] {"20833", "x-EBCDIC-KoreanExtended", "IBM EBCDIC (Korean Extended)"},
            new string[] {"20838", "IBM-Thai", "IBM EBCDIC (Thai)"},
            new string[] {"20866", "koi8-r", "Cyrillic (KOI8-R)"},
            new string[] {"20871", "IBM871", "IBM EBCDIC (Icelandic)"},
            new string[] {"20880", "IBM880", "IBM EBCDIC (Cyrillic Russian)"},
            new string[] {"20905", "IBM905", "IBM EBCDIC (Turkish)"},
            new string[] {"20924", "IBM00924", "IBM Latin-1"},
            new string[] {"20932", "EUC-JP", "Japanese (JIS 0208-1990 and 0212-1990)"},
            new string[] {"20936", "x-cp20936", "Chinese Simplified (GB2312-80)"},
            new string[] {"20949", "x-cp20949", "Korean Wansung"},
            new string[] {"21025", "cp1025", "IBM EBCDIC (Cyrillic Serbian-Bulgarian)"},
            new string[] {"21866", "koi8-u", "Cyrillic (KOI8-U)"},
            new string[] {"28591", "iso-8859-1", "Western European (ISO)"},
            new string[] {"28592", "iso-8859-2", "Central European (ISO)"},
            new string[] {"28593", "iso-8859-3", "Latin 3 (ISO)"},
            new string[] {"28594", "iso-8859-4", "Baltic (ISO)"},
            new string[] {"28595", "iso-8859-5", "Cyrillic (ISO)"},
            new string[] {"28596", "iso-8859-6", "Arabic (ISO)"},
            new string[] {"28597", "iso-8859-7", "Greek (ISO)"},
            new string[] {"28598", "iso-8859-8", "Hebrew (ISO-Visual)"},
            new string[] {"28599", "iso-8859-9", "Turkish (ISO)"},
            new string[] {"28603", "iso-8859-13", "Estonian (ISO)"},
            new string[] {"28605", "iso-8859-15", "Latin 9 (ISO)"},
            new string[] {"29001", "x-Europa", "Europa"},
            new string[] {"38598", "iso-8859-8-i", "Hebrew (ISO-Logical)"},
            new string[] {"50220", "iso-2022-jp", "Japanese (JIS)"},
            new string[] {"50221", "csISO2022JP", "Japanese (JIS-Allow 1 byte Kana)"},
            new string[] {"50222", "iso-2022-jp", "Japanese (JIS-Allow 1 byte Kana - SO/SI)"},
            new string[] {"50225", "iso-2022-kr", "Korean (ISO)"},
            new string[] {"50227", "x-cp50227", "Chinese Simplified (ISO-2022)"},
            new string[] {"51932", "euc-jp", "Japanese (EUC)"},
            new string[] {"51936", "EUC-CN", "Chinese Simplified (EUC)"},
            new string[] {"51949", "euc-kr", "Korean (EUC)"},
            new string[] {"52936", "hz-gb-2312", "Chinese Simplified (HZ)"},
            new string[] {"54936", "GB18030", "Chinese Simplified (GB18030)"},
            new string[] {"57002", "x-iscii-de", "ISCII Devanagari"},
            new string[] {"57003", "x-iscii-be", "ISCII Bengali"},
            new string[] {"57004", "x-iscii-ta", "ISCII Tamil"},
            new string[] {"57005", "x-iscii-te", "ISCII Telugu"},
            new string[] {"57006", "x-iscii-as", "ISCII Assamese"},
            new string[] {"57007", "x-iscii-or", "ISCII Oriya"},
            new string[] {"57008", "x-iscii-ka", "ISCII Kannada"},
            new string[] {"57009", "x-iscii-ma", "ISCII Malayalam"},
            new string[] {"57010", "x-iscii-gu", "ISCII Gujarati"},
            new string[] {"57011", "x-iscii-pa", "ISCII Punjabi"},
            new string[] {"65000", "utf-7", "Unicode (UTF-7)"},
            new string[] {"65001", "utf-8", "Unicode (UTF-8)"}
        };

        [TestMethod]
        public void TestUniqueness()
        {
            var nameSet = new SortedSet<string>(StringComparer.CurrentCultureIgnoreCase);
            var displayNameSet = new SortedSet<string>(StringComparer.CurrentCultureIgnoreCase);
            var lastCount = 0;
            foreach (var encodingInfo in EncodingTests)
            {
                lastCount++;
                nameSet.Add(encodingInfo[1]);
                displayNameSet.Add(encodingInfo[2]);

                if(lastCount != displayNameSet.Count)
                {
                    Console.WriteLine("Existing DisplayName Found: ");
                    Console.WriteLine("\tName = {0}", encodingInfo[1]);
                    Console.WriteLine("\tDisplayName = {0}", encodingInfo[2]);
                }
                if(lastCount != nameSet.Count)
                {
                    Console.WriteLine("Existing DisplayName Found: ");
                    Console.WriteLine("\tName = {0}", encodingInfo[1]);
                    Console.WriteLine("\tDisplayName = {0}", encodingInfo[2]);
                }
            }

            Assert.AreNotEqual(EncodingTests.Length, displayNameSet.Count);
            Assert.AreNotEqual(EncodingTests.Length, nameSet.Count);
        }

        [TestMethod]
        public void TestGetEncoding()
        {
            var encodingInfos = Encoding.GetEncodings();

            int encVsDn = 0;
            int encVsN = 0;
            int wnVsN = 0;
            int wnVsDn = 0;
            foreach (var encodingInfo in encodingInfos)
            {
                var encoding = Encoding.GetEncoding(encodingInfo.Name);
                Assert.IsNotNull(encoding);

                string dneTemplate = "{0}\t{1}\t{2}\t{3}\t{4}";
                // Asserting that the encoding name does not equal the display name.
                if (!encoding.EncodingName.Equals(encodingInfo.DisplayName, StringComparison.CurrentCultureIgnoreCase))
                {
                    encVsDn++;
                    Console.WriteLine(dneTemplate, encoding.EncodingName, encodingInfo.DisplayName, "EncodingName", "!=", "DisplayName");
                }
                // Asserting that the encoding name equals the name.
                if (encoding.EncodingName.Equals(encodingInfo.Name, StringComparison.CurrentCultureIgnoreCase))
                {
                    encVsN++;
                    //Console.WriteLine(dneTemplate, encoding.EncodingName, encodingInfo.Name, "EncodingName", "==", "Name");
                }
                // Asserting that the web name does not equal the name.
                if (!encoding.WebName.Equals(encodingInfo.Name, StringComparison.CurrentCultureIgnoreCase))
                {
                    wnVsN++;
                    Console.WriteLine(dneTemplate, encoding.WebName, encodingInfo.Name, "WebName", "!=", "Name");
                }
                // Asserting that the web name equals the display name.
                if (encoding.WebName.Equals(encodingInfo.DisplayName, StringComparison.CurrentCultureIgnoreCase))
                {
                    wnVsDn++;
                    //Console.WriteLine(dneTemplate, encoding.WebName, encodingInfo.DisplayName, "WebName", "==", "DisplayName");
                }


            }

            Assert.AreEqual(2, encVsDn);
            Assert.AreEqual(0, wnVsN);

            Assert.AreNotEqual(0, encVsN);
            Assert.AreNotEqual(0, wnVsDn);
        }

        [TestMethod]
        public void TestW3CNameHasEncodingName()
        {
            int failed = 0;
            foreach (var w3cEncodings in W3CEncodings)
            {
                try
                {
                    var encoding = Encoding.GetEncoding(w3cEncodings[1]);
                }
                catch (ArgumentException)
                {
                    if (!String.IsNullOrEmpty(w3cEncodings[1]))
                    {
                        failed++;
                        Console.WriteLine("w3cEncoding[1] = {0}", w3cEncodings[1]);
                    }
                }
            }
            Assert.AreEqual(6, failed);
        }

        [TestMethod]
        public void TestNormalRetrievals()
        {
            var encodings = new string[] {
                "UTF-8",
                "utf-8",
                "ISO-8859-1",
                "iso-8859-1"
            };

            foreach (var encodingStr in encodings)
            {
                Encoding encoding = null;
                try
                {
                    encoding = Encoding.GetEncoding(encodingStr);
                }
                catch(ArgumentException)
                {
                }
                Assert.IsNotNull(encoding);
            }

            try
            {
                var badEncoding = Encoding.GetEncoding("fdejfklfadkjlfsdkl");
                Assert.IsTrue(false);
            }
            catch (ArgumentException)
            {

            }

        }
    }
}
