using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace IInteractive.WebConsole
{
    public class Browser
    {
        public string Name
        {
            get
            {
                return _BrowserConfig.Name;
            }
        }
        public int MaximumAutomaticRedirections
        {
            get
            {
                return _BrowserConfig.MaximumAutomaticRedirections;
            }
        }
        public bool AllowAutoRedirect 
        {
            get
            {
                return _BrowserConfig.AllowAutoRedirect;
            }
        }
        public string UserAgent
        {
            get
            {
                return _BrowserConfig.UserAgent;
            }
        }
        public string Accept
        {
            get
            {
                return _BrowserConfig.Accept;
            }
        }
        public string AcceptCharset
        {
            get
            {
                return _BrowserConfig.AcceptCharset;
            }
        }
        public string AcceptLanguage
        {
            get
            {
                return _BrowserConfig.AcceptLanguage;
            }
        }
        public Int32 Timeout
        {
            get
            {
                return _BrowserConfig.Timeout;
            }
        }
        public Int32 MaxRemoteAutomaticRedirects
        {
            get
            {
                return _BrowserConfig.MaxRemoteAutomaticRedirects;
            }
        }

        public CredentialCache Credentials
        {
            get
            {
                CredentialCache cache = new CredentialCache();
                if (_CredentialsConfig != null)
                {
                    for (int i = 0; i < _CredentialsConfig.Count; i++)
                    {
                        var credentialElement = _CredentialsConfig[i];
                        if (!string.IsNullOrEmpty(credentialElement.UriPrefix)
                            && !string.IsNullOrEmpty(credentialElement.User)
                            && !string.IsNullOrEmpty(credentialElement.Password))
                        {
                            NetworkCredential credential = new NetworkCredential(credentialElement.User, credentialElement.Password);
                            cache.Add(new Uri(credentialElement.UriPrefix), "Basic", credential);
                            cache.Add(new Uri(credentialElement.UriPrefix), "Digest", credential);
                        }
                    }
                }
                return cache;
            }
        }

        private BrowserConfigElement _BrowserConfig { get; set; }
        private NetworkCredentialsCollection _CredentialsConfig { get; set; }

        private SortedSet<HttpRequestResult> HttpRequestResults { get; set; }

        public Browser(BrowserConfigElement BrowserConfig, NetworkCredentialsCollection CredentialsConfig)
        {
            this._BrowserConfig = BrowserConfig;
            this._CredentialsConfig = CredentialsConfig;

            HttpRequestResults = new SortedSet<HttpRequestResult>();
        }

        public Browser(BrowserConfigElement BrowserConfig) : this(BrowserConfig, null)
        {
        }

        public Browser()
            : this(new LinkCheckerConfigSection().Browsers[0])
        {

        }

        public HttpRequestResult Get(Uri url, bool isRemote)
        {
            HttpRequestResult results = (from httpRequestResult in HttpRequestResults
                            where httpRequestResult.RequestUrl.Equals(url)
                            select httpRequestResult).FirstOrDefault();

            if (results == null)
            {
                results = new HttpRequestResult();
                results.RequestUrl = url;
                results.Start = DateTime.Now;
                results.BrowserUsed = this;

                StreamReader streamReader = null;
                WebResponse response = null;
                try
                {
                    var request = (HttpWebRequest)WebRequest.Create(url);
                    if (isRemote)
                        request.MaximumAutomaticRedirections = this.MaxRemoteAutomaticRedirects;
                    else
                        request.MaximumAutomaticRedirections = this.MaximumAutomaticRedirections;
                    request.Timeout = this.Timeout * 1000;
                    request.AllowAutoRedirect = AllowAutoRedirect;
                    request.UserAgent = UserAgent;
                    request.Accept = Accept;
                    request.Headers.Add("Accept-Charset", AcceptCharset);
                    request.Headers.Add("Accept-Language", AcceptLanguage);
                    request.Credentials = this.Credentials;
                    request.CookieContainer = new CookieContainer();

                    response = request.GetResponse();
                    streamReader = new StreamReader(response.GetResponseStream());

                    if (response is HttpWebResponse)
                        results.Charset = ((HttpWebResponse)response).CharacterSet;

                    string content = streamReader.ReadToEnd();

                    results.ContentType = response.ContentType;
                    if (results.IsCss || results.IsHtml)
                    {
                        results.Content = content;
                    }
                    results.ResultUrl = request.Address;

                    HttpRequestResults.Add(results);
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
                    results.End = DateTime.Now;
                    if (streamReader != null)
                    {
                        try { streamReader.Close(); }
                        catch { }
                    }
                    if (response != null)
                    {
                        try { response.Close(); }
                        catch { }
                    }
                }
            }

            return results;
        }
    }
}
