using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IInteractive.WebConsole;
using System.Xml.Linq;
using System.IO;

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
        /// <param name="HtmlValidationResponse">The response of the validation service.</param>
        /// /// <param name="IsDotNetEncoding">Whether the response had an encoding recognized by the .NET framework Encoding class.</param>
        public HtmlValidationResult(string HtmlValidationResponse, bool IsDotNetEncoding)
        {
            var reader = new StringReader(HtmlValidationResponse);
            this.HtmlValidationResponse = XDocument.Load(reader);
            this.IsDotNetEncoding = IsDotNetEncoding;
            HTMLErrors(true);
            HTMLWarnings();
            HTMLFaults();
            reader.Close();
        }

        /// <summary>
        /// Creates an HtmlValidationResult object.
        /// </summary>
        /// <param name="HtmlValidationResponse">The response of the validation service.</param>
        public HtmlValidationResult(string HtmlValidationResponse) : this(HtmlValidationResponse, true)
        {
        }

        /// <summary>
        /// The HtmlValidationResponse retrieved by the HtmlValidator.
        /// </summary>
        public XDocument HtmlValidationResponse { get; private set; }
        /// <summary>
        /// Whether or not the encoding was recognized by the .NET framework Encoding class.
        /// </summary>
        public bool IsDotNetEncoding = true;
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



        /// <summary>
        /// Method to obtain all the warnings as a result of the validation of the html
        /// </summary>
        /// <param name="urlDocument">Xml document that represents the result of the validation</param>
        /// <param name="mNamespace">Namespace used to obtain some data such as line, colum, etc.</param>
        private void HTMLWarnings()
        {
            this.Warnings = new Warnings();
            this.WarningPotentialIssues = new MarkupValidator.WarningPotentialIssues();

            //Obtaining the descendants of the elements labeled "warnings". With this we obtain all the warnings
            var warningsElements = from e in HtmlValidationResponse.Descendants(MNamespace + "warnings")
                                   select e;
            //Obtaining the descendants of the elements labeled "warningcount". With this we can obtain the number of warnings.
            var warningCountElement = from e in warningsElements.Descendants(MNamespace + "warningcount")
                                      select e;
            //Obtaining the descendants of the elements labeled "warning". With this we can obtain information from each of the warnings. 
            var warningListElements = from e in warningsElements.Descendants(MNamespace + "warning")
                                      select e;

            //Iterate over the 'warningaccount' variable to obtain the number of warnings
            foreach (var element in warningCountElement)
            {
                //Store the value of the count
                this.Warnings.warningCount = element.Value;

                //Iterate over the 'warningListElements' variable to obtain each error
                foreach (var warningElement in warningListElements)
                {
                    //Create an instance of a Warning
                    Warning warning = new Warning();

                    //If there is a number of line
                    if (warningElement.Descendants(MNamespace + "line").Count() > 0)
                        //Store all the información of the warning.
                        warning.line = warningElement.Descendants(MNamespace + "line").First().Value;
                    //If there is a number of column
                    if (warningElement.Descendants(MNamespace + "col").Count() > 0)
                        //Store all the información of the warning.
                        warning.col = warningElement.Descendants(MNamespace + "col").First().Value;
                    //If there is an explnation
                    if (warningElement.Descendants(MNamespace + "explanation").Count() > 0)
                        //Store all the información of the warning.
                        warning.explanation = warningElement.Descendants(MNamespace + "explanation").First().Value;
                    //If there is a source
                    if (warningElement.Descendants(MNamespace + "source").Count() > 0)
                        //Store all the información of the warning.
                        warning.source = warningElement.Descendants(MNamespace + "source").First().Value;
                    //If there is a messageid
                    if (warningElement.Descendants(MNamespace + "messageid").Count() > 0)
                    {
                        //If the messageid stars with a 'W' it means that the warning is a PotentialIssue
                        if (warningElement.Descendants(MNamespace + "messageid").First().Value.StartsWith("W"))
                        {
                            //Create an instance of a WarningPotentialIssue
                            WarningPotentialIssue warningPotentialIssue = new WarningPotentialIssue();

                            //Store the messageid in the warningPotentialIssue object
                            warningPotentialIssue.messageid = warningElement.Descendants(MNamespace + "messageid").First().Value;
                            //If there is a message
                            if (warningElement.Descendants(MNamespace + "message").Count() > 0)
                                //Store the message in the warningPotentialIssue object
                                warningPotentialIssue.message = warningElement.Descendants(MNamespace + "message").First().Value;
                            ////Add the warningPotentialIssue to the list of warningPotentialIssues.
                            this.WarningPotentialIssues.Add(warningPotentialIssue);
                        }
                        //If the messageid not stars with a 'W'
                        else
                        {
                            //Store the messageid
                            warning.messageid = warningElement.Descendants(MNamespace + "messageid").First().Value;
                            //If there is a message
                            if (warningElement.Descendants(MNamespace + "message").Count() > 0)
                                //Store the message
                                warning.message = warningElement.Descendants(MNamespace + "message").First().Value;

                            //Add the warning to the list of warnings
                            this.Warnings.Add(warning);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Method to obtain all the errors as a result of the validation of the html
        /// </summary>
        /// <param name="htmlValid">Boolean that will indicate if the document is or not valid html.</param>
        /// <param name="urlDocument">Xml document that represents the result of the validation</param>
        /// <param name="mNamespace">Namespace using to obtain some data such as line, colum, etc.</param>
        /// <returns>Boolean that will indicate if the document is or not valid html.</returns>
        private Boolean HTMLErrors(Boolean htmlValid)
        {
            this.Errors = new Errors();

            //Obtaining the descendants of the elements labeled "errors". With this we obtain all the errors
            var errorsElements = from e in HtmlValidationResponse.Descendants(MNamespace + "errors")
                                 select e;
            //Obtaining the descendants of the elements labeled "errorcount". With this we can obtain the number of errors.
            var errorCountElement = from e in errorsElements.Descendants(MNamespace + "errorcount")
                                    select e;
            //Obtaining the descendants of the elements labeled "error". With this we can obtain information from each of the errors. 
            var errorListElements = from e in errorsElements.Descendants(MNamespace + "error")
                                    select e;

            //Iterate over the 'errorcount' variable to obtain the number of errors
            foreach (var element in errorCountElement)
            {
                //Store the value of the count
                this.Errors.errorsCount = element.Value;

                //If the number of errors is greater than 0
                if (int.Parse(this.Errors.errorsCount) > 0)
                    //The document is not a valid html document according to the doctype especified
                    htmlValid = false;

                //Iterate over the 'errorListElements' variable to obtain each error
                foreach (var errorElement in errorListElements)
                {
                    //Create a instance of an Error
                    Error error = new Error();
                    //If there is a number of line
                    if (errorElement.Descendants(MNamespace + "line").Count() > 0)
                        //Store the line
                        error.line = errorElement.Descendants(MNamespace + "line").First().Value;
                    //If there is a number of line
                    if (errorElement.Descendants(MNamespace + "col").Count() > 0)
                        //Store the col
                        error.col = errorElement.Descendants(MNamespace + "col").First().Value;
                    //If there is a number of line
                    if (errorElement.Descendants(MNamespace + "message").Count() > 0)
                        //Store the message
                        error.message = errorElement.Descendants(MNamespace + "message").First().Value;
                    //If there is a number of line
                    if (errorElement.Descendants(MNamespace + "messageid").Count() > 0)
                        //Store the messageid
                        error.messageId = errorElement.Descendants(MNamespace + "messageid").First().Value;
                    //If there is a number of line
                    if (errorElement.Descendants(MNamespace + "explanation").Count() > 0)
                        //Store the explanation
                        error.explanation = errorElement.Descendants(MNamespace + "explanation").First().Value;
                    //If there is a number of line
                    if (errorElement.Descendants(MNamespace + "source").Count() > 0)
                        //Store the source
                        error.source = errorElement.Descendants(MNamespace + "source").First().Value;

                    //Add the error to the list of errors that are stored in the 'errors' variable.
                    this.Errors.Add(error);
                }
            }
            return htmlValid;
        }

        /// <summary>
        /// Method to obtain all the faults as a result of the validation of the html
        /// </summary>
        /// <param name="htmlValid">Boolean that will indicate if the document is or not valid html.</param>
        /// <param name="urlDocument">Xml document that represents the result of the validation</param>
        /// <param name="envNamespace">Namespace using to obtain some data such as line, colum, etc.</param>
        /// <param name="mNamespace">Namespace using to obtain some data such as line, colum, etc.</param>
        private void HTMLFaults()
        {
            this.Faults = new Faults();

            //Obtaining the descendants of the elements labeled "Fault". With this we obtain all the faults
            var faultElement = from e in HtmlValidationResponse.Descendants(EnvNamespace + "Fault")
                               select e;

            //Obtaining the descendants of the elements labeled "Detail". With this we obtain the details of the fault 
            var faultDetail = from e in faultElement.Descendants(EnvNamespace + "Detail")
                              select e;

            //Obtaining the descendants of the elements labeled "Text". With this we obtain the reason text of the fault
            var faultReason = from e in faultElement.Descendants(EnvNamespace + "Text")
                              select e;

            //Iterate over the fault elements
            foreach (var element in faultElement)
            {
                //Create a new instance of the class Fault
                Fault fault = new Fault();

                //Iterate over the fault reason
                foreach (var reason in faultReason)
                {
                    //Store the value of the reason into de instance of the fault
                    fault.reason = reason.Value;
                }
                //Iterate over the fault detail
                foreach (var e in faultDetail)
                {
                    //Store the value of the messageid and errordetail
                    fault.messageid = e.Descendants(MNamespace + "messageid").First().Value;
                    fault.errorDetail = e.Descendants(MNamespace + "errordetail").First().Value;
                }

                //Insert the fault in the list of faults
                this.Faults.Add(fault);
            }
        }
    }
}
