using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoctypeEncodingValidation
{
    /// <summary>
    /// Class with all the properties needed to represent a WarningPotentialIssue according to the xml returned by the W3C
    /// </summary>
    /// <remarks>
    /// Author: María Eugenia Fernández Menéndez
    /// E-mail: mariae.fernandez.menendez@gmail.com
    /// Date created: 14-04-2009
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
    public class WarningPotentialIssue
    {
        #region Properties
        /// <summary>
        /// Message of the WarningPotentialIssue
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// Message identifier of the WarningPotentialIssue
        /// </summary>
        public string messageid { get; set; }
        /// <summary>
        /// Explanation of the WarningPotentialIssue
        /// </summary>
        public string explanation { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor without parameters
        /// </summary>
        public WarningPotentialIssue() { }
        /// <summary>
        /// Constructor with parameters
        /// </summary>
        /// <param name="message">Message of the WarningPotentialIssue</param>
        /// <param name="messageid">Message identifier of the WarningPotentialIssue</param>
        /// <param name="explanation">Explanation of the WarningPotentialIssue</param>
        public WarningPotentialIssue(string message, string messageid, string explanation)
        {
            this.message = message;
            this.messageid = messageid;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Method that returns a string with all the information of the warning 
        /// </summary>
        /// <returns>String with all the information of the warning </returns>
        public override string ToString()
        {
            string cadena = "Message id: " + this.messageid + " - Message: " + this.message;
            return cadena;
        }
        #endregion
    }
}
