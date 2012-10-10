using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IInteractive.WebTest
{
    public class HttpValidationError
    {
        public int HttpCode { get; set; }
        public string Message { get; set; }
        public Exception Error { get; set; }
        public Uri AbsoluteUri { get; set; }
    }
}
