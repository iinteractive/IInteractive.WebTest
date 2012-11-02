using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IInteractive.WebConsole;
using System.IO;
using System.Net;
using System.Collections.Specialized;

namespace IInteractive.MarkupValidator
{
    /// <summary>
    /// The purpose of this class is to hold the control logic for making 
    /// requests to the W3C validation service.
    /// </summary>
    public class HtmlValidator
    {
        public static string[] EncodingValues = new string[] {
	            "utf-8",
	            "utf-16",
	            "iso-8859-1",
	            "iso-8859-2",
	            "iso-8859-3",
	            "iso-8859-4",
	            "iso-8859-5",
	            "iso-8859-6-i",
	            "iso-8859-7",
	            "iso-8859-8",
	            "iso-8859-8-i",
	            "iso-8859-9",
	            "iso-8859-10",
	            "iso-8859-11",
	            "iso-8859-13",
	            "iso-8859-14",
	            "iso-8859-15",
	            "iso-8859-16",
	            "us-ascii",
	            "euc-jp",
	            "shift_jis",
	            "iso-2022-jp",
	            "euc-kr",
	            "gb2312",
	            "gb18030",
	            "big5",
	            "big5-HKSCS",
	            "tis-620",
	            "koi8-r",
	            "koi8-u",
	            "iso-ir-111",
	            "macintosh",
	            "windows-1250",
	            "windows-1251",
	            "windows-1252",
	            "windows-1253",
	            "windows-1254",
	            "windows-1255",
	            "windows-1256",
	            "windows-1257"
            };

        public static string AutoDetectEncodingValue = "(detect automatically)";

        public HtmlValidator(Uri ValidationUri)
        {
            this.ValidationUri = ValidationUri;
        }

        public Uri ValidationUri;

        public void Validate(HttpRequestResult HttpRequestResult)
        {
            var nvc = new NameValueCollection();
            bool nonDotNetEncodingException = false;
            try
            {
                nvc.Add("charset", DetermineCharset(HttpRequestResult));
            }
            catch (NonDotNetEncodingException ex)
            {
                nonDotNetEncodingException = true;
                nvc.Add("charset", ex.CharsetToUse);
            }
            nvc.Add("doctype", "Inline");
            nvc.Add("group", "0");
            nvc.Add("user-agent", "W3C_Validator/1.3");
            nvc.Add("output", "soap12");
            HttpRequestResult.HtmlValidationResult = new HtmlValidationResult(UploadFileToValidator(HttpRequestResult, nvc), nonDotNetEncodingException);
        }

        /// <summary>
        /// Determines the charset string to pass to the w3c validator.
        /// </summary>
        /// <param name="HttpRequestResult"></param>
        /// <returns></returns>
        public string DetermineCharset(HttpRequestResult HttpRequestResult)
        {
            if (String.IsNullOrEmpty(HttpRequestResult.Charset))
            {
                return AutoDetectEncodingValue;
            }
            else
            {
                bool isDotNetEncoding = true;
                try
                {
                    var dotNetEncoding = Encoding.GetEncoding(HttpRequestResult.Charset);
                }
                catch (ArgumentException)
                {
                    isDotNetEncoding = false;
                }

                bool isW3CEncoding = EncodingValues.Contains(HttpRequestResult.Charset, StringComparer.CurrentCultureIgnoreCase);

                if (isDotNetEncoding && isW3CEncoding)
                {
                    return HttpRequestResult.Charset;
                }
                else if (!isDotNetEncoding && isW3CEncoding)
                {
                    throw new NonDotNetEncodingException(HttpRequestResult.Charset);
                }
                else if (isDotNetEncoding && !isW3CEncoding)
                {
                    return HttpRequestResult.Charset;
                }
                else 
                {
                    throw new NonDotNetEncodingException(AutoDetectEncodingValue);
                }
                
            }
        }

        private string UploadFileToValidator(HttpRequestResult file, NameValueCollection nvc)
        {
            long contentLength = 0;
            string boundary = "----------------------------" +
            DateTime.Now.Ticks.ToString("x");


            HttpWebRequest httpWebRequest2 = (HttpWebRequest)WebRequest.Create(ValidationUri);
            httpWebRequest2.ContentType = "multipart/form-data; boundary=" +
            boundary;
            httpWebRequest2.Method = "POST";
            httpWebRequest2.KeepAlive = true;
            httpWebRequest2.Credentials =
            System.Net.CredentialCache.DefaultCredentials;
            httpWebRequest2.CookieContainer = new CookieContainer();



            Stream memStream = new System.IO.MemoryStream();

            byte[] boundarybytes;
            try
            {
                boundarybytes = Encoding.GetEncoding(file.Charset).GetBytes("\r\n--" + boundary + "\r\n");
            }
            catch(ArgumentException)
            {
                boundarybytes = System.Text.Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");
            }
                


            string formdataTemplate = "\r\n--" + boundary +
            "\r\nContent-Disposition: form-data; name=\"{0}\";\r\n\r\n{1}";

            foreach (string key in nvc.Keys)
            {
                string formitem = string.Format(formdataTemplate, key, nvc[key]);
                byte[] formitembytes;
                try
                {
                    formitembytes = Encoding.GetEncoding(file.Charset).GetBytes(formitem);
                }
                catch(ArgumentException)
                {
                    formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                }
                contentLength += formitembytes.Length;
                memStream.Write(formitembytes, 0, formitembytes.Length);
            }

            contentLength += boundarybytes.Length;
            memStream.Write(boundarybytes, 0, boundarybytes.Length);

            string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: text/html;charset={2}\r\n\r\n";


            //string header = string.Format(headerTemplate, "file" + i, files[i]);
            string header = string.Format(headerTemplate, "uploaded_file", "temp.html", file.Charset);

            byte[] headerbytes;
            byte[] fileInBytes;
            try
            {
                headerbytes = Encoding.GetEncoding(file.Charset).GetBytes(header);
            }
            catch (ArgumentException)
            {
                headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            }

            try
            {
                fileInBytes = Encoding.GetEncoding(file.Charset).GetBytes(file.Content);
            }
            catch (ArgumentException)
            {
                fileInBytes = System.Text.Encoding.UTF8.GetBytes(file.Content);
            }

            contentLength += headerbytes.Length;
            memStream.Write(headerbytes, 0, headerbytes.Length);

            contentLength += fileInBytes.Length;
            memStream.Write(fileInBytes, 0, fileInBytes.Length);

            contentLength += boundarybytes.Length;
            memStream.Write(boundarybytes, 0, boundarybytes.Length);

            httpWebRequest2.ContentLength = contentLength;

            Stream requestStream = httpWebRequest2.GetRequestStream();

            memStream.Position = 0;
            byte[] tempBuffer = new byte[contentLength];
            memStream.Read(tempBuffer, 0, tempBuffer.Length);
            memStream.Close();
            requestStream.Write(tempBuffer, 0, tempBuffer.Length);
            requestStream.Close();


            WebResponse webResponse2 = httpWebRequest2.GetResponse();

            Stream stream2 = webResponse2.GetResponseStream();
            StreamReader reader2 = new StreamReader(stream2);

            string output = reader2.ReadToEnd();

            webResponse2.Close();
            httpWebRequest2 = null;
            webResponse2 = null;

            return output;
        }
    }
}
