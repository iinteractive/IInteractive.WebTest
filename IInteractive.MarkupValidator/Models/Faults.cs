using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoctypeEncodingValidation
{
    /// <summary>
    /// List of the faults that can appear on a document
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
    class Faults:IEnumerable<Fault>
    {
        #region Variables
        /// <summary>
        /// Variable that represents the list of faults
        /// </summary>
        private List<Fault> faultList;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor without parameters
        /// </summary>
        public Faults()
        {
            faultList = new List<Fault>();
        }
        #endregion

        #region Methods
        #region IEnumerable<Fault> Members
        /// <summary>
        /// Method that return an iterator of the list
        /// </summary>
        /// <returns>Iterator of the list</returns>
        public IEnumerator<Fault> GetEnumerator()
        {
            return faultList.GetEnumerator();
        }
        #endregion

        #region IEnumerable Members
        /// <summary>
        /// Method that return an iterator of the list
        /// </summary>
        /// <returns>Iterator of the list</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return faultList.GetEnumerator();
        }
        #endregion
        /// <summary>
        /// Method to add an item to the list
        /// </summary>
        /// <param name="item">Element to add</param>
        public void Add(Fault item)
        {
            faultList.Add(item);
        }
        #endregion
    }
}
