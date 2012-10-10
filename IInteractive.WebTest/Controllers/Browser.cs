using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace IInteractive.WebTest
{
    public class Browser
    {
        public int MaximumAutomaticRedirections { get; set; }
        public bool AllowAutoRedirect { get; set; }
        public string UserAgent { get; set; }
        public string Accept { get; set; }
        public string AcceptCharset { get; set; }
        public string AcceptLanguage { get; set; }

        public Browser()
        {
            MaximumAutomaticRedirections = 2;
            AllowAutoRedirect = true;
            UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.4 (KHTML, like Gecko) Chrome/22.0.1229.79 Safari/537.4";
            Accept = "*/*";
            AcceptCharset = "ISO-8859-1,utf-8;q=0.7,*;q=0.3";
            AcceptLanguage = "en-US,en;q=0.8";
        }

        public HtmlRequestResult GetHtml(Uri url)
        {
            var results = new HtmlRequestResult();

            StreamReader streamReader = null;
            WebResponse response = null;
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.MaximumAutomaticRedirections = MaximumAutomaticRedirections;
                request.AllowAutoRedirect = AllowAutoRedirect;
                request.UserAgent = UserAgent;
                request.Accept = Accept;
                request.Headers.Add("Accept-Charset", AcceptCharset);
                request.Headers.Add("Accept-Language", AcceptLanguage);

                response = request.GetResponse();
                streamReader = new StreamReader(response.GetResponseStream());

                results.Html = streamReader.ReadToEnd();
                results.ContentType = response.ContentType;
                results.ResultUrl = request.Address;
            }
            catch (WebException exception)
            {
                var error = new HttpValidationError()
                {
                    AbsoluteUri = url,
                    Error = exception,
                    Message = exception.Message
                };

                if (exception.Status == WebExceptionStatus.ProtocolError)
                    error.HttpCode = (int)((HttpWebResponse)exception.Response).StatusCode;

                results.Error = error;
            }
            catch (Exception exception)
            {
                results.Error = new HttpValidationError()
                    {
                        AbsoluteUri = url,
                        Error = exception,
                        Message = exception.Message
                    };
            }
            finally
            {
                if(streamReader != null)
                {
                    try { streamReader.Close(); } catch { }
                }
                if (response != null)
                {
                    try { response.Close(); } catch { }
                }
            }

            return results;
        }

        public HttpRequestResult Get(Uri url)
        {
            var results = new HtmlRequestResult();

            WebResponse response = null;
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.MaximumAutomaticRedirections = MaximumAutomaticRedirections;
                request.AllowAutoRedirect = AllowAutoRedirect;
                request.UserAgent = UserAgent;
                request.Accept = Accept;
                request.Headers.Add("Accept-Charset", AcceptCharset);
                request.Headers.Add("Accept-Language", AcceptLanguage);

                response = request.GetResponse();

                results.ResultUrl = request.Address;
            }
            catch (WebException exception)
            {
                var error = new HttpValidationError()
                {
                    AbsoluteUri = url,
                    Error = exception,
                    Message = exception.Message
                };

                if (exception.Status == WebExceptionStatus.ProtocolError)
                    error.HttpCode = (int)((HttpWebResponse)exception.Response).StatusCode;

                results.Error = error;
            }
            catch (Exception exception)
            {
                results.Error = new HttpValidationError()
                {
                    AbsoluteUri = url,
                    Error = exception,
                    Message = exception.Message
                };
            }
            finally
            {
                if (response != null)
                {
                    try { response.Close(); }
                    catch { }
                }
            }

            return results;
        }
    }
}
