using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IInteractive.WebTest
{
    public class HttpRequestResult
    {
        public HttpValidationError Error { get; set; }
        public Uri ResultUrl { get; set; }
    }
}
