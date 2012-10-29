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
        public HtmlValidator(Uri ValidationUri)
        {
            this.ValidationUri = ValidationUri;
        }

        public Uri ValidationUri;

        public HtmlValidationResult Validate(HttpRequestResult HttpRequestResult)
        {
            var nvc = new NameValueCollection();
            nvc.Add("charset", "(detect automatically)");
            nvc.Add("doctype", "Inline");
            nvc.Add("group", "0");
            nvc.Add("user-agent", "W3C_Validator/1.3");
            nvc.Add("output", "soap12");
            return new HtmlValidationResult(HttpRequestResult, UploadFileToValidator(HttpRequestResult.Content, nvc));
        }

        private string UploadFileToValidator(string file, NameValueCollection nvc)
        {
            string boundary = "----------------------------" +
            DateTime.Now.Ticks.ToString("x");


            HttpWebRequest httpWebRequest2 = (HttpWebRequest)WebRequest.Create(ValidationUri);
            httpWebRequest2.ContentType = "multipart/form-data; boundary=" +
            boundary;
            httpWebRequest2.Method = "POST";
            httpWebRequest2.KeepAlive = true;
            httpWebRequest2.Credentials =
            System.Net.CredentialCache.DefaultCredentials;



            Stream memStream = new System.IO.MemoryStream();

            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" +
            boundary + "\r\n");


            string formdataTemplate = "\r\n--" + boundary +
            "\r\nContent-Disposition: form-data; name=\"{0}\";\r\n\r\n{1}";

            foreach (string key in nvc.Keys)
            {
                string formitem = string.Format(formdataTemplate, key, nvc[key]);
                byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                memStream.Write(formitembytes, 0, formitembytes.Length);
            }


            memStream.Write(boundarybytes, 0, boundarybytes.Length);

            string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: text/html\r\n\r\n";


            //string header = string.Format(headerTemplate, "file" + i, files[i]);
            string header = string.Format(headerTemplate, "uploaded_file", "temp.html");

            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);

            memStream.Write(headerbytes, 0, headerbytes.Length);

            byte[] fileInBytes = System.Text.Encoding.UTF8.GetBytes(file);
            memStream.Write(fileInBytes, 0, fileInBytes.Length);

            memStream.Write(boundarybytes, 0, boundarybytes.Length);

            httpWebRequest2.ContentLength = memStream.Length;

            Stream requestStream = httpWebRequest2.GetRequestStream();

            memStream.Position = 0;
            byte[] tempBuffer = new byte[memStream.Length];
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
