using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrawlerTestApp
{
    /// <summary>
    /// Summary description for Handler1
    /// </summary>
    public class W3CCharsetHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            var inputs = new CharsetParameters(context);
            context.Response.ContentType = new Output().GetContentType(inputs);
            context.Response.Write(new Output().Create(inputs));
            context.Response.Flush();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    public class CharsetParameters
    {
        public CharsetParameters(HttpContext context)
        {
            var parameters = context.Request.Params;
            string[] MetaTag = parameters.GetValues("MetaTag");
            string[] Charset = parameters.GetValues("Charset");

            this.MetaTag = MetaTag != null;
            this.Charset = Charset[0];
        }
        

        public bool MetaTag;
        public string Charset;
    }


    public class Output
    {
        public static string HtmlTemplate = "<!DOCTYPE html><html><head><title></title>{0}</head><body>{1}</body>";
        public static string MetaTagTemplate = "<meta http-equiv=\"Content-type\" content=\"{0}\" />";
        public static string ContentTypeTemplate = "text/html; charset={0}";

        public string Create(CharsetParameters Parameters)
        {
            string contentType = GetContentType(Parameters);

            string metaTag = "";
            if(Parameters.MetaTag)
                metaTag = string.Format(MetaTagTemplate, contentType);

            return string.Format(HtmlTemplate, metaTag, "");
        }

        public string GetContentType(CharsetParameters Parameters)
        {
            return string.Format(ContentTypeTemplate, Parameters.Charset); ;
        }
    }
}