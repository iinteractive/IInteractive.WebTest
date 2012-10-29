using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace DoctypeEncodingValidation
{
    /// <summary>
    /// Class to validate an url against a doctype, an encoding, none of them (only validate the url) or both.
    /// </summary>
    /// <remarks>
    /// Author: María Eugenia Fernández Menéndez
    /// E-mail: mariae.fernandez.menendez@gmail.com
    /// Date created: 08-04-2009
    /// Last modified: 02-05-2009
    /// Version: 0.1
    /// License:
    /// 
    ///     This file is part of DoctypeEncodingValidation.
    ///     
    ///     DoctypeEncodingValidation is free software: you can redistribute
    ///     it and/or modify it under the terms of the GNU General Public 
    ///     License as published by the Free Software Foundation, either 
    ///     version 3 of the License, or (at your option) any later version.
    ///     
    ///     DoctypeEncodingValidation is distributed in the hope that it 
    ///     will be useful, but WITHOUT ANY WARRANTY; without even the 
    ///     implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR
    ///     PURPOSE. See the GNU General Public License for more details.
    ///  
    ///     You should have received a copy of the GNU General Public License
    ///     along with DoctypeEncodingValidation. If not, see <http://www.gnu.org/licenses/>.
    ///     
    /// </remarks>
    public class HTMLValidate
    {
        #region Constants

        #region doctypes
        /// <summary>
        /// Constant that represents the empty Doctype as described in the W3C Web Service reference
        /// </summary>
        public const string emptyDoctype = "";
        /// <summary>
        /// Constant that represents the HTML 5 Doctype as described in the W3C Web Service reference
        /// </summary>
        public const string HTML5 = "HTML5";
        /// <summary>
        /// Constant that represents the XHTML 1.0 Strict Doctype as described in the W3C Web Service reference
        /// </summary>
        public const string XHTML_10_STRICT = "XHTML+1.0+Strict";
        /// <summary>
        /// Constant that represents the XHTML 1.0 Transitional Doctype as described in the W3C Web Service reference
        /// </summary>
        public const string XHTML_10_TRANSITIONAL = "XHTML+1.0+Transitional";
        /// <summary>
        /// Constant that represents the XHTML 1.0 Frameset Doctype as described in the W3C Web Service reference
        /// </summary>
        public const string XHTML_10_FRAMESET = "XHTML+1.0+Frameset";
        /// <summary>
        /// Constant that represents the HTML 4.01 Strict Doctype as described in the W3C Web Service reference
        /// </summary>
        public const string HTML_401_STRICT = "HTML+4.01+Strict";
        /// <summary>
        /// Constant that represents the HTML 4.01 Transitional Doctype as described in the W3C Web Service reference
        /// </summary>
        public const string HTML_401_TRANSITIONAL = "HTML+4.01+Transitional";
        /// <summary>
        /// Constant that represents the HTML 4.01 Frameset Doctype as described in the W3C Web Service reference
        /// </summary>
        public const string HTML_401_FRAMESET = "HTML+4.01+Frameset";
        /// <summary>
        /// Constant that represents the HTML 3.2 Doctype as described in the W3C Web Service reference
        /// </summary>
        public const string HTML_32 = "HTML+3.2";
        /// <summary>
        /// Constant that represents the HTML 2.0 Doctype as described in the W3C Web Service reference
        /// </summary>
        public const string HTML_20 = "HTML+2.0";
        /// <summary>
        /// Constant that represents the SO/IEC 15445:2000 ("ISO HTML") Doctype as described in the W3C Web Service reference
        /// </summary>
        public const string SO_IEC_15445_2000 = "SO/IEC 15445:2000 (\"ISO HTML\")";
        /// <summary>
        /// Constant that represents the XHTML 1.1 Doctype as described in the W3C Web Service reference
        /// </summary>
        public const string XHTML_11 = "XHTML+1.1";
        /// <summary>
        /// Constant that represents the XHTML + RDFa Doctype as described in the W3C Web Service reference
        /// </summary>
        public const string XHTML_RDFa = "XHTML+%2B+RDFa";
        /// <summary>
        /// Constant that represents the XHTML BASIC 1.0 Doctype as described in the W3C Web Service reference
        /// </summary>
        public const string XHTML_BASIC_10 = "XHTML+Basic+1.0";
        /// <summary>
        /// Constant that represents the XHTML BASIC 1.1 Doctype as described in the W3C Web Service reference
        /// </summary>
        public const string XHTML_BASIC_11 = "XHTML+Basic+1.1";
        /// <summary>
        /// Constant that represents the XHTML-MP 1.2 Doctype as described in the W3C Web Service reference 
        /// </summary>
        public const string XHTML_MP_12 = "XHTML+Mobile+Profile+1.2";
        /// <summary>
        /// Constant that represents the XHTML-Print+1.0 Doctype as described in the W3C Web Service reference
        /// </summary>
        public const string XHTML_Print_10 = "XHTML-Print+1.0";
        /// <summary>
        /// Constant that represents the XHTML 1.1 plus MathML 2.0 Doctype as described in the W3C Web Service reference
        /// </summary>
        public const string XHTML_11_PLUS_MATHML_20 = "XHTML+1.1+plus+MathML+2.0";
        /// <summary>
        /// Constant that represents the XHTML 1.1 plus MathML 2.0 plus SVG 1.1 Doctype as described in the W3C Web Service reference
        /// </summary>
        public const string XHTML_11_PLUS_MATHML_20_PLUS_SVG_11 = "XHTML+1.1+plus+MathML+2.0+plus+SVG+1.1";
        /// <summary>
        /// Constant that represents the MathML 2.0 Doctype as described in the W3C Web Service reference
        /// </summary>
        public const string MATHML_20 = "MathML+2.0";
        /// <summary>
        /// Constant that represents the SVG 1.0 Doctype as described in the W3C Web Service reference
        /// </summary>
        public const string SVG_10 = "SVG+1.0";
        /// <summary>
        /// Constant that represents the SVG 1.1 Doctype as described in the W3C Web Service reference
        /// </summary>
        public const string SVG_11 = "SVG+1.1";
        /// <summary>
        /// Constant that represents the SVG 1.1 Tiny Doctype as described in the W3C Web Service reference
        /// </summary>
        public const string SVG_11_Tiny = "SVG+1.1+Tiny";
        /// <summary>
        /// Constant that represents the SVG 1.1 Basic Doctype as described in the W3C Web Service reference
        /// </summary>
        public const string SVG_11_Basic = "SVG+1.1+Basic";
        /// <summary>
        /// Constant that represents the SMIL 1.0 Doctype as described in the W3C Web Service reference
        /// </summary>
        public const string SMIL_10 = "SMIL+1.0";
        /// <summary>
        /// Constant that represents the SMIL 2.0 Doctype as described in the W3C Web Service reference
        /// </summary>
        public const string SMIL_20 = "SMIL+2.0";
        #endregion
        #region encoding
        /// <summary>
        /// Constant that represents the empty Encoding as described in the W3C Web Service reference
        /// </summary>
        public const string emptyEncoding = "";
        /// <summary>
        /// Constant that represents the UTF-8 Encoding as described in the W3C Web Service reference
        /// </summary>
        public const string UTF8 = "utf-8";
        /// <summary>
        /// Constant that represents the UTF-16 Encoding as described in the W3C Web Service reference
        /// </summary>
        public const string UTF16 = "utf-16";
        /// <summary>
        /// Constant that represents the iso-8859-1 (Western Europe) Encoding as described in the W3C Web Service reference
        /// </summary>
        public const string iso_8859_1 = "iso-8859-1";
        /// <summary>
        /// Constant that represents the iso-8859-2 (Central Europe) Encoding as described in the W3C Web Service reference
        /// </summary>
        public const string iso_8859_2 = "iso-8859-2";
        /// <summary>
        /// Constant that represents the iso-8859-3 (Southern Europe) Encoding as described in the W3C Web Service reference
        /// </summary>
        public const string iso_8859_3 = "iso-8859-3";
        /// <summary>
        /// Constant that represents the iso-8859-4 (North European) Encoding as described in the W3C Web Service reference
        /// </summary>
        public const string iso_8859_4 = "iso-8859-4";
        /// <summary>
        /// Constant that represents the iso-8859-5 (Cyrillic) Encoding as described in the W3C Web Service reference
        /// </summary>
        public const string iso_8859_5 = "iso-8859-5";
        /// <summary>
        /// Constant that represents the iso-8859-6-i (Arabic) Encoding as described in the W3C Web Service reference
        /// </summary>
        public const string iso_8859_6_i = "iso-8859-6-i";
        /// <summary>
        /// Constant that represents the iso-8859-7 (Greek) Encoding as described in the W3C Web Service reference
        /// </summary>
        public const string iso_8859_7 = "iso-8859-7";
        /// <summary>
        /// Constant that represents the iso-8859-8 (Hebrew, visual) Encoding as described in the W3C Web Service reference
        /// </summary>
        public const string iso_8859_8 = "iso-8859-8";
        /// <summary>
        /// Constant that represents the iso-8859-8-i (Hebrew, logical) Encoding as described in the W3C Web Service reference
        /// </summary>
        public const string iso_8859_8_i = "iso-8859-8-i";
        /// <summary>
        /// Constant that represents the iso-8859-9 (Turkish) Encoding as described in the W3C Web Service reference
        /// </summary>
        public const string iso_8859_9 = "iso-8859-9";
        /// <summary>
        /// Constant that represents the iso-8859-10 (Latin 6) Encoding as described in the W3C Web Service reference
        /// </summary>
        public const string iso_8859_10 = "iso-8859-10";
        /// <summary>
        /// Constant that represents the iso-8859-11 (Latin/Thai) Encoding as described in the W3C Web Service reference
        /// </summary>
        public const string iso_8859_11 = "iso-8859-11";
        /// <summary>
        /// Constant that represents the iso-8859-13 (Latin 7, Baltic Rim) Encoding as described in the W3C Web Service reference
        /// </summary>
        public const string iso_8859_13 = "iso-8859-13";
        /// <summary>
        /// Constant that represents the iso-8859-14 (Latin 8, Celtic) Encoding as described in the W3C Web Service reference
        /// </summary>
        public const string iso_8859_14 = "iso-8859-14";
        /// <summary>
        /// Constant that represents the iso-8859-15 (Latin 9)Encoding as described in the W3C Web Service reference
        /// </summary>
        public const string iso_8859_15 = "iso-8859-15";
        /// <summary>
        /// Constant that represents the iso-8859-16 (Latin 10) Encoding as described in the W3C Web Service reference
        /// </summary>
        public const string iso_8859_16 = "iso-8859-16";
        /// <summary>
        /// Constant that represents the us-ascii (basic English) Encoding as described in the W3C Web Service reference
        /// </summary>
        public const string us_ascii = "us-ascii";
        /// <summary>
        /// Constant that represents the euc-jp (Japanese Unix) Encoding as described in the W3C Web Service reference
        /// </summary>
        public const string euc_jp = "euc-jp";
        /// <summary>
        /// Constant that represents the shift_jis (Japanese Win/Mac) Encoding as described in the W3C Web Service reference
        /// </summary>
        public const string shift_jis = "shift_jis";
        /// <summary>
        /// Constant that represents the iso-2022-jp (Japanese email) Encoding as described in the W3C Web Service reference
        /// </summary>
        public const string iso_2022_jp = "iso-2022-jp";
        /// <summary>
        /// Constant that represents the euc-kr (Korean) Encoding as described in the W3C Web Service reference
        /// </summary>
        public const string euc_kr = "euc-kr";
        /// <summary>
        /// Constant that represents the gb2312 (Chinese simplified) Encoding as described in the W3C Web Service reference
        /// </summary>
        public const string gb2312 = "gb2312";
        /// <summary>
        /// Constant that represents the gb18030 (Chinese simplified) Encoding as described in the W3C Web Service reference
        /// </summary>
        public const string gb18030 = "gb18030";
        /// <summary>
        /// Constant that represents the big5 (Chonese traditional) Encoding as described in the W3C Web Service reference
        /// </summary>
        public const string big5 = "big5";
        /// <summary>
        /// Constant that represents the big5-hkscs (Chinese, Hong Kong) Encoding as described in the W3C Web Service reference
        /// </summary>
        public const string big5_hkscs = "big5-HKSCS";
        /// <summary>
        /// Constant that represents the tis-620 (Thai) Encoding as described in the W3C Web Service reference
        /// </summary>
        public const string tis_620 = "tis-620";
        /// <summary>
        /// Constant that represents the koi8-r (Russian) Encoding as described in the W3C Web Service reference
        /// </summary>
        public const string koi8_r = "koi8-r";
        /// <summary>
        /// Constant that represents the koi8-u (Ukranian) Encoding as described in the W3C Web Service reference
        /// </summary>
        public const string koi8_u = "koi8-u";
        /// <summary>
        /// Constant that represents the iso-ir-111 (Cyrillic KOI-8) Encoding as described in the W3C Web Service reference
        /// </summary>
        public const string iso_ir_111 = "iso-ir-111";
        /// <summary>
        /// Constant that represents the macintosh (MacRoman) Encoding as described in the W3C Web Service reference
        /// </summary>
        public const string macintosh = "macintosh";
        /// <summary>
        /// Constant that represents the windows-1250 (Central Europe) Encoding as described in the W3C Web Service reference
        /// </summary>
        public const string windows_1250 = "windows-1250";
        /// <summary>
        /// Constant that represents the windows-1251 (Cyrillic) Encoding as described in the W3C Web Service reference
        /// </summary>
        public const string windows_1251 = "windows-1251";
        /// <summary>
        /// Constant that represents the windows-1252 (Western Europe) Encoding as described in the W3C Web Service reference
        /// </summary>
        public const string windows_1252 = "windows-1252";
        /// <summary>
        /// Constant that represents the windows-1253 (Greek) Encoding as described in the W3C Web Service reference
        /// </summary>
        public const string windows_1253 = "windows-1253";
        /// <summary>
        /// Constant that represents the windows-1254 (Turkish) Encoding as described in the W3C Web Service reference
        /// </summary>
        public const string windows_1254 = "windows-1254";
        /// <summary>
        /// Constant that represents the windows-1255 (Hebrew) Encoding as described in the W3C Web Service reference
        /// </summary>
        public const string windows_1255 = "windows-1255";
        /// <summary>
        /// Constant that represents the windows-1256 (Arabic) Encoding as described in the W3C Web Service reference
        /// </summary>
        public const string windows_1256 = "windows-1256";
        /// <summary>
        /// Constant that represents the windows-1257 (Baltic Rim) Encoding as described in the W3C Web Service reference
        /// </summary>
        public const string windows_1257 = "windows-1257";
        #endregion

        #endregion

        #region variables
        /// <summary>
        /// URL to validate
        /// </summary>
        private string URL = string.Empty;
        /// <summary>
        /// Errors class instance
        /// </summary>
        private Errors errors;
        /// <summary>
        /// Warnings class instance
        /// </summary>
        private Warnings warnings;
        /// <summary>
        /// WarningPotentialIssues class instance
        /// </summary>
        private WarningPotentialIssues warningPotentialIssues;
        /// <summary>
        /// Faults class instance
        /// </summary>
        private Faults faults;
        /// <summary>
        /// Creating the XNamespace for the "env" namespace used in the xml document that we obtain.
        /// </summary>
        XNamespace envNamespace = "http://www.w3.org/2003/05/soap-envelope";
        /// <summary>
        /// Creating the XNamespace for the "m" namespace used in the xml document that we obtain.
        /// </summary>
        XNamespace mNamespace = "http://www.w3.org/2005/10/markup-validator";


        #endregion

        #region Constructor
        /// <summary>
        /// Constructor with parameters
        /// </summary>
        public HTMLValidate(string url)
        {
            errors = new Errors();
            warnings = new Warnings();
            warningPotentialIssues = new WarningPotentialIssues();
            faults = new Faults();
            this.URL = url;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Method for validating a url, taking into account both the encoding and the doctype
        /// </summary>
        /// <param name="encoding">Encoding to validate against. You can select an encoding from the constants of the class.</param>
        /// <param name="doctype">Doctype to validate against. You can select a doctype from the constants of the class</param>
        /// <returns>Boolean that indicates if the url is or not valid</returns>
        public Boolean Validate(string encoding, string doctype)
        {
            Boolean valid = false;
            //Validate if the url validates the doctype and encoding
            valid = this.ValidationOfDoctype(doctype) && this.ValidationOfEncoding(encoding);
            return valid;
        }

        /// <summary>
        /// Method to verify that the URL is well formed and exists 
        /// </summary>
        /// <exception cref="ControlException">The exception is thrown if the URL cannot be found by the W3C webservice or the URL scheme is not supported</exception>
        public void ValidationOfURL()
        {
            //Create the url to validate de original url.
            string url = "http://validator.w3.org/check?uri=" + this.URL + "&charset=utf-8&output=soap12";

            //Loading the XML document that obtain as response to the url.
            XDocument urlDocument = XDocument.Load(url);

            //Call the method to obtain the document faults, if any.
            HTMLFaults(urlDocument, envNamespace, mNamespace);

            //Iterate over the list of faults
            foreach (Fault fault in faults)
            {
                //This message identifier indicates not found url
                if (fault.messageid == "fatal_http_error")
                {
                    throw new ControlException("Sorry! This document can not be checked. URL not found", DateTime.Now);
                }
                //This message identifier indicates that the url is not well formed
                else if (fault.messageid == "fatal_uri_error")
                {
                    throw new ControlException("URL scheme not supported", DateTime.Now);
                }
            }
        }

        /// <summary>
        /// Method to verify that the document pointed to by the URL is valid according to the especific encoding.        
        /// </summary>
        /// <returns>Boolean indicating whether the document is or not valid according to the encoding</returns>
        /// <exception cref="ControlException">The exception is thrown if the URL cannot be found by the W3C webservice or the URL scheme is not supported or if an error occurs when the webservice is executed.</exception>
        public Boolean ValidationOfEncoding(string encoding)
        {
            //Create a variable to store if the document is or not valid
            Boolean encodingValid = true;
            //String representing the url with which we carry out validation and get the resulting xml file
            string url = string.Empty;

            try
            {
                //Verify that the url is well formed and exist
                ValidationOfURL();

                try
                {
                    //If encoding is specified
                    if (!encoding.Equals(""))
                    {
                        //Create the url to validate the original url, with encoding.
                        url = "http://validator.w3.org/check?uri=" + this.URL + "&charset=" + encoding + "&output=soap12";
                    }
                    //If encoding is not specified
                    else
                    {
                        //Create the url to validate de original url.
                        url = "http://validator.w3.org/check?uri=" + this.URL + "&output=soap12";
                    }

                    //Loading the XML document that obtain as response to the url.
                    XDocument urlDocument = XDocument.Load(url);

                    //Call the method to obtain the document faults, if any.
                    HTMLFaults(urlDocument, envNamespace, mNamespace);

                    //Iterate over the list of faults
                    foreach (Fault fault in faults)
                    {
                        //Note: I only tested with UTF-8 and I assume that the message 
                        //received in response to other encodings are the same. If 
                        //someone tests with other encodings, and detects that the message
                        //is another, please send me an email with the information to make 
                        //the necessary changes. E-mail: mariae.fernandez.menendez [at] gmail.com

                        //If any of the messageid is "fatal_byte_error"·
                        if (fault.messageid == "fatal_byte_error")
                        {
                            //The document is not a valid document. for that encoding
                            encodingValid = false;
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new ControlException(e.Message, DateTime.Now, e);
                }

            }
            catch (ControlException e)
            {
                throw e;
            }

            return encodingValid;
        }

        /// <summary>
        /// Method to verify that the document pointed to by the URL is valid according to the especific doctype.
        /// </summary>
        /// <returns>Boolean indicating whether or not the document is valid for a specified doctype</returns>
        /// <exception cref="ControlException">The exception is thrown if the URL cannot be found by the W3C webservice or the URL scheme is not supported or if an error occurs when the webservice is executed.</exception>
        public Boolean ValidationOfDoctype(string doctype)
        {
            //Boolean that indicate if the document is or not valid
            Boolean htmlValid = true;
            //String representing the url with which we carry out validation and get the resulting xml file
            string url = string.Empty;

            try
            {
                //Verify that the url is well formed and exist
                ValidationOfURL();
                try
                {
                    //If encoding is specified
                    if (!doctype.Equals(""))
                    {
                        //Create the url to validate the original url, with doctype.
                        url = "http://validator.w3.org/check?uri=" + this.URL + "&doctype=" + doctype + "&output=soap12";
                    }
                    //If encoding is not specified
                    else
                    {
                        //Create the url to validate de original url.
                        url = "http://validator.w3.org/check?uri=" + this.URL + "&output=soap12";
                    }

                    //Loading the XML document that obtain as response to the url.
                    XDocument urlDocument = XDocument.Load(url);

                    //Call the method to obtain the errors of the validation, if any, and if the document it's valid or not
                    htmlValid = HTMLErrors(htmlValid, urlDocument, mNamespace);

                    //Call the method to obtain the warnings of the validation, if any
                    HTMLWarnings(urlDocument, mNamespace);
                }
                catch (Exception e)
                {
                    throw new ControlException(e.Message, DateTime.Now, e);
                }
            }
            catch (ControlException e)
            {
                throw e;
            }
            //If there are errors then the document is no valid.
            return htmlValid;
        }

        /// <summary>
        /// Method to obtain all the warnings as a result of the validation of the html
        /// </summary>
        /// <param name="urlDocument">Xml document that represents the result of the validation</param>
        /// <param name="mNamespace">Namespace used to obtain some data such as line, colum, etc.</param>
        private void HTMLWarnings(XDocument urlDocument, XNamespace mNamespace)
        {
            warnings = new Warnings();

            //Obtaining the descendants of the elements labeled "warnings". With this we obtain all the warnings
            var warningsElements = from e in urlDocument.Descendants(mNamespace + "warnings")
                                   select e;
            //Obtaining the descendants of the elements labeled "warningcount". With this we can obtain the number of warnings.
            var warningCountElement = from e in warningsElements.Descendants(mNamespace + "warningcount")
                                      select e;
            //Obtaining the descendants of the elements labeled "warning". With this we can obtain information from each of the warnings. 
            var warningListElements = from e in warningsElements.Descendants(mNamespace + "warning")
                                      select e;

            //Iterate over the 'warningaccount' variable to obtain the number of warnings
            foreach (var element in warningCountElement)
            {
                //Store the value of the count
                warnings.warningCount = element.Value;

                //Iterate over the 'warningListElements' variable to obtain each error
                foreach (var warningElement in warningListElements)
                {
                    //Create an instance of a Warning
                    Warning warning = new Warning();

                    //If there is a number of line
                    if (warningElement.Descendants(mNamespace + "line").Count() > 0)
                        //Store all the información of the warning.
                        warning.line = warningElement.Descendants(mNamespace + "line").First().Value;
                    //If there is a number of column
                    if (warningElement.Descendants(mNamespace + "col").Count() > 0)
                        //Store all the información of the warning.
                        warning.col = warningElement.Descendants(mNamespace + "col").First().Value;
                    //If there is an explnation
                    if (warningElement.Descendants(mNamespace + "explanation").Count() > 0)
                        //Store all the información of the warning.
                        warning.explanation = warningElement.Descendants(mNamespace + "explanation").First().Value;
                    //If there is a source
                    if (warningElement.Descendants(mNamespace + "source").Count() > 0)
                        //Store all the información of the warning.
                        warning.source = warningElement.Descendants(mNamespace + "source").First().Value;
                    //If there is a messageid
                    if (warningElement.Descendants(mNamespace + "messageid").Count() > 0)
                    {
                        //If the messageid stars with a 'W' it means that the warning is a PotentialIssue
                        if (warningElement.Descendants(mNamespace + "messageid").First().Value.StartsWith("W"))
                        {
                            //Create an instance of a WarningPotentialIssue
                            WarningPotentialIssue warningPotentialIssue = new WarningPotentialIssue();

                            //Store the messageid in the warningPotentialIssue object
                            warningPotentialIssue.messageid = warningElement.Descendants(mNamespace + "messageid").First().Value;
                            //If there is a message
                            if (warningElement.Descendants(mNamespace + "message").Count() > 0)
                                //Store the message in the warningPotentialIssue object
                                warningPotentialIssue.message = warningElement.Descendants(mNamespace + "message").First().Value;
                            ////Add the warningPotentialIssue to the list of warningPotentialIssues.
                            warningPotentialIssues.Add(warningPotentialIssue);
                        }
                        //If the messageid not stars with a 'W'
                        else
                        {
                            //Store the messageid
                            warning.messageid = warningElement.Descendants(mNamespace + "messageid").First().Value;
                            //If there is a message
                            if (warningElement.Descendants(mNamespace + "message").Count() > 0)
                                //Store the message
                                warning.message = warningElement.Descendants(mNamespace + "message").First().Value;

                            //Add the warning to the list of warnings
                            warnings.Add(warning);
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
        private Boolean HTMLErrors(Boolean htmlValid, XDocument urlDocument, XNamespace mNamespace)
        {
            errors = new Errors();

            //Obtaining the descendants of the elements labeled "errors". With this we obtain all the errors
            var errorsElements = from e in urlDocument.Descendants(mNamespace + "errors")
                                 select e;
            //Obtaining the descendants of the elements labeled "errorcount". With this we can obtain the number of errors.
            var errorCountElement = from e in errorsElements.Descendants(mNamespace + "errorcount")
                                    select e;
            //Obtaining the descendants of the elements labeled "error". With this we can obtain information from each of the errors. 
            var errorListElements = from e in errorsElements.Descendants(mNamespace + "error")
                                    select e;

            //Iterate over the 'errorcount' variable to obtain the number of errors
            foreach (var element in errorCountElement)
            {
                //Store the value of the count
                errors.errorsCount = element.Value;

                //If the number of errors is greater than 0
                if (int.Parse(errors.errorsCount) > 0)
                    //The document is not a valid html document according to the doctype especified
                    htmlValid = false;

                //Iterate over the 'errorListElements' variable to obtain each error
                foreach (var errorElement in errorListElements)
                {
                    //Create a instance of an Error
                    Error error = new Error();
                    //If there is a number of line
                    if (errorElement.Descendants(mNamespace + "line").Count() > 0)
                        //Store the line
                        error.line = errorElement.Descendants(mNamespace + "line").First().Value;
                    //If there is a number of line
                    if (errorElement.Descendants(mNamespace + "col").Count() > 0)
                        //Store the col
                        error.col = errorElement.Descendants(mNamespace + "col").First().Value;
                    //If there is a number of line
                    if (errorElement.Descendants(mNamespace + "message").Count() > 0)
                        //Store the message
                        error.message = errorElement.Descendants(mNamespace + "message").First().Value;
                    //If there is a number of line
                    if (errorElement.Descendants(mNamespace + "messageid").Count() > 0)
                        //Store the messageid
                        error.messageId = errorElement.Descendants(mNamespace + "messageid").First().Value;
                    //If there is a number of line
                    if (errorElement.Descendants(mNamespace + "explanation").Count() > 0)
                        //Store the explanation
                        error.explanation = errorElement.Descendants(mNamespace + "explanation").First().Value;
                    //If there is a number of line
                    if (errorElement.Descendants(mNamespace + "source").Count() > 0)
                        //Store the source
                        error.source = errorElement.Descendants(mNamespace + "source").First().Value;

                    //Add the error to the list of errors that are stored in the 'errors' variable.
                    errors.Add(error);
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
        private void HTMLFaults(XDocument urlDocument, XNamespace envNamespace, XNamespace mNamespace)
        {
            faults = new Faults();

            //Obtaining the descendants of the elements labeled "Fault". With this we obtain all the faults
            var faultElement = from e in urlDocument.Descendants(envNamespace + "Fault")
                               select e;

            //Obtaining the descendants of the elements labeled "Detail". With this we obtain the details of the fault 
            var faultDetail = from e in faultElement.Descendants(envNamespace + "Detail")
                              select e;

            //Obtaining the descendants of the elements labeled "Text". With this we obtain the reason text of the fault
            var faultReason = from e in faultElement.Descendants(envNamespace + "Text")
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
                    fault.messageid = e.Descendants(mNamespace + "messageid").First().Value;
                    fault.errorDetail = e.Descendants(mNamespace + "errordetail").First().Value;
                }

                //Insert the fault in the list of faults
                faults.Add(fault);
            }
        }

        /// <summary>
        /// Method to write to a file all the errors of the document
        /// </summary>
        /// <exception cref="ControlException">Maybe something wrong with this method can occur when attempting to write the file(printErrorsToFile)</exception>
        public void printErrorsToFile()
        {
            try
            {
                //Open the file to append information
                System.IO.StreamWriter file = new System.IO.StreamWriter("errors.txt", true);

                file.WriteLine("\n****** BEGIN " + this.URL.Substring(7) + "******\n");

                //Iterate over the list of errors
                foreach (Error error in errors)
                {
                    //Write each error on the file
                    file.WriteLine(error.ToString());
                }

                file.WriteLine("\n****** END " + this.URL.Substring(7) + "******\n");

                //Close the file
                file.Close();
            }
            catch (Exception e)
            {
                throw new ControlException(e.Message, DateTime.Now, e);
            }
        }

        /// <summary>
        /// Method to write to file all the warnings of the document
        /// </summary>
        /// <exception cref="ControlException">Maybe something wrong with this method can occur when attempting to write the file (printWarningsToFile)</exception>
        public void printWarningsToFile()
        {
            try
            {
                //Open the file to append information
                System.IO.StreamWriter file = new System.IO.StreamWriter("warnings.txt", true);

                file.WriteLine("\n****** BEGIN " + this.URL.Substring(7) + "******\n");

                //Iterate over the list of warningPotentialIssues
                foreach (WarningPotentialIssue warning in warningPotentialIssues)
                {
                    //Write the warningPotentialIssue to the file
                    file.WriteLine(warning.ToString());
                }

                //Iterate over the list of warnings
                foreach (Warning warning in warnings)
                {
                    //Write the warning to the file
                    file.WriteLine(warning.ToString());
                }

                file.WriteLine("\n****** END " + this.URL.Substring(7) + "******\n");

                //Close the file
                file.Close();
            }
            catch (Exception e)
            {
                throw new ControlException(e.Message, DateTime.Now, e);
            }
        }
        #endregion

        static void Main(string[] args)
        {
            //*********************************//
            //**Example of use of the library**//
            //*********************************//

            //Not found ourl
            //HTMLValidate htmlValidate = new HTMLValidate("http://www.walidator.org");
            //Valid XHTML Basic 1.0 URL
            HTMLValidate htmlValidate = new HTMLValidate("http://mariaefm.net/docs_pfc/xhtml_10.html");
            //Invalid URL
            //HTMLValidate htmlValidate = new HTMLValidate("http://www.google.es");

            try
            {
                if (!htmlValidate.Validate(HTMLValidate.emptyEncoding, HTMLValidate.emptyDoctype))
                {
                    Console.WriteLine("Invalid Markup validation of the document");
                }
                else
                {
                    Console.WriteLine("Valid Markup validation of the document");
                }
            }
            catch (ControlException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.errorTimeStamp.ToString());
            }

            try
            {
                if (!htmlValidate.ValidationOfEncoding(HTMLValidate.UTF8))
                {
                    Console.WriteLine("This document is NOT a valid utf-8 document");
                    foreach (Fault element in htmlValidate.faults)
                    {
                        if (element.messageid == "fatal_byte_error")
                            Console.WriteLine(element.ToString());
                    }
                }
                else
                    Console.WriteLine("This document is a valid utf-8 document");
            }
            catch (ControlException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.errorTimeStamp.ToString());
            }

            try
            {
                if (!htmlValidate.ValidationOfDoctype(HTMLValidate.XHTML_BASIC_10))
                {
                    Console.WriteLine("This document is NOT a valid html document");
                    foreach (Error element in htmlValidate.errors)
                    {
                        Console.WriteLine(element.ToString());
                    }
                    htmlValidate.printErrorsToFile();

                    foreach (WarningPotentialIssue warningPotentialIssue in htmlValidate.warningPotentialIssues)
                    {
                        Console.WriteLine(warningPotentialIssue.ToString());
                    }

                    foreach (Warning warning in htmlValidate.warnings)
                    {
                        Console.WriteLine(warning.ToString());
                    }
                    htmlValidate.printWarningsToFile();
                }
                else
                    Console.WriteLine("This document is a valid html document");
            }
            catch (ControlException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.errorTimeStamp.ToString());
            }
        }
    }
}


