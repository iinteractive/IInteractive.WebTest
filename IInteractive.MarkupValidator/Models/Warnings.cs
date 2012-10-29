using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoctypeEncodingValidation
{
    /// <summary>
    /// List of the warnings that can appear on a document
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
    class Warnings : IEnumerable<Warning>
    {
        #region Variables
        /// <summary>
        /// Variable that represents the list of warnings
        /// </summary>
        public List<Warning> warningList;
        #endregion

        #region properties
        /// <summary>
        /// Number of warnings of the document
        /// </summary>
        public string warningCount { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor without parameters
        /// </summary>
        public Warnings()
        {
            warningList = new List<Warning>();
        }
        #endregion

        #region Methods
        #region IEnumerable<Warning> Members
        /// <summary>
        /// Method that return an iterator of the list
        /// </summary>
        /// <returns>Iterator of the list</returns>
        public IEnumerator<Warning> GetEnumerator()
        {
            return warningList.GetEnumerator();
        }
        #endregion

        #region IEnumerable Members
        /// <summary>
        /// Method that return an iterator of the list
        /// </summary>
        /// <returns>Iterator of the list</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return warningList.GetEnumerator();
        }
        #endregion
        /// <summary>
        /// Method to add an item to the list
        /// </summary>
        /// <param name="item">Element to add</param>
        public void Add(Warning item)
        {
            warningList.Add(item);
        }
        #endregion
    }
}
