using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IInteractive.WebTest.Parsers
{
    public class CssParser
    {
        public CssParser(HttpRequestResult HttpRequestResult)
        {
            this.HttpRequestResult = HttpRequestResult;
        }

        public HttpRequestResult HttpRequestResult;

        public List<Link> Parse()
        {
            return new List<Link>();
        }

        private string RemoveComments()
        {
            throw new NotImplementedException();
        }

        public List<Link> GenerateUrls()
        {
            throw new NotImplementedException();
        }
    }
}
