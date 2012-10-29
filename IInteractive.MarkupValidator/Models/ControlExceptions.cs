using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoctypeEncodingValidation
{
    /// <summary>
    /// Exception control for the HTMLValidation class.
    /// </summary>
    /// <remarks>
    /// Author: María Eugenia Fernández Menéndez
    /// E-mail: mariae.fernandez.menendez@gmail.com
    /// Date created: 15-04-2009
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
    [global::System.Serializable]
    public class ControlException : Exception
    {
        #region variables
        /// <summary>
        /// Time stamp of the error
        /// </summary>       
        public DateTime errorTimeStamp { get; set; }
        #endregion

        /// <summary>
        /// Default constructor without parameters
        /// </summary>
        public ControlException() { }
        /// <summary>
        /// Constructor with parameters
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="time">Time stamp of the error</param>
        public ControlException(string message, DateTime time) : base(message) { this.errorTimeStamp = time;}
        /// <summary>
        /// Constructor with parameters
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="time">Time stamp of the error</param>
        /// <param name="inner">Inner exception</param>
        public ControlException(string message, DateTime time, Exception inner) : base(message, inner) { this.errorTimeStamp = time;}
        
        protected ControlException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
