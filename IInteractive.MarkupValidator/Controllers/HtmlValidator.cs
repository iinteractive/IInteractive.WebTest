using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IInteractive.WebConsole;

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
            throw new NotImplementedException();
        }
    }
}
