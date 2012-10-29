using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoctypeEncodingValidation
{
    /// <summary>
    /// Class with all the properties needed to represent an Error according to the xml returned by the W3C
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
    class Error
    {

        #region Properties
        /// <summary>
        /// Line of the document where happens the error
        /// </summary>
        public string line { get; set; }
        /// <summary>
        /// Column of the document where happens the error
        /// </summary>
        public string col { get; set; }
        /// <summary>
        /// Message error
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// Message identifier of the error
        /// </summary>
        public string messageId { get; set; }
        /// <summary>
        /// Explanation of the error
        /// </summary>
        public string explanation { get; set; }
        /// <summary>
        /// Source of the error
        /// </summary>
        public string source { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor without parameters
        /// </summary>
        public Error() { }
        /// <summary>
        /// Constructor with parameters
        /// </summary>
        /// <param name="line">Line of the error</param>
        /// <param name="col">Column of the error</param>
        /// <param name="message">Message of the error</param>
        /// <param name="messageId">Message identifier of the error</param>
        /// <param name="explanation">Explanation of the error</param>
        /// <param name="source">Source of the error</param>
        public Error(string line, string col, string message, string messageId, string explanation, string source)
        {
            this.line = line;
            this.col = col;
            this.message = message;
            this.messageId = messageId;
            this.explanation = explanation;
            this.source = source;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Method that returns a string with all the information of the error 
        /// </summary>
        /// <returns>String with all the information of the error </returns>
        public override string ToString()
        {
            string cadena = "Linea: " + this.line + " - Col: " + this.col + " - Message: " + this.message;
            return cadena;
        }
        #endregion
    }
}
