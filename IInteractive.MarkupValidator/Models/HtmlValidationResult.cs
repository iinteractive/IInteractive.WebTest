using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IInteractive.WebConsole;
using System.Xml.Linq;

namespace IInteractive.MarkupValidator
{

    /// <summary>
    /// This class serves as a POCO for holding the information returned from 
    /// the HTML Validation service.
    /// </summary>
    public class HtmlValidationResult
    {
        /// <summary>
        /// Creates an HtmlValidationResult object.
        /// </summary>
        /// <param name="HttpRequestResult">The HttpRequestResult that was validated.</param>
        /// <param name="HtmlValidationResponse">The response of the validation service.</param>
        public HtmlValidationResult(HttpRequestResult HttpRequestResult, string HtmlValidationResponse)
        {
            this.HttpRequestResult = HttpRequestResult;
            this.HtmlValidationResponse = HtmlValidationResponse;
        }

        /// <summary>
        /// The HtmlValidationResponse retrieved by the HtmlValidator.
        /// </summary>
        public string HtmlValidationResponse { get; private set; }
        /// <summary>
        /// HttpRequestResult to validate.
        /// </summary>
        public HttpRequestResult HttpRequestResult { get; private set; }
        /// <summary>
        /// Errors class instance
        /// </summary>
        public Errors Errors { get; private set; }
        /// <summary>
        /// Warnings class instance
        /// </summary>
        public Warnings Warnings { get; private set; }
        /// <summary>
        /// WarningPotentialIssues class instance
        /// </summary>
        public WarningPotentialIssues WarningPotentialIssues { get; private set; }
        /// <summary>
        /// Faults class instance
        /// </summary>
        public Faults Faults { get; private set; }
        /// <summary>
        /// Creating the XNamespace for the "env" namespace used in the xml document that we obtain.
        /// </summary>
        XNamespace EnvNamespace = "http://www.w3.org/2003/05/soap-envelope";
        /// <summary>
        /// Creating the XNamespace for the "m" namespace used in the xml document that we obtain.
        /// </summary>
        XNamespace MNamespace = "http://www.w3.org/2005/10/markup-validator";
    }
}
