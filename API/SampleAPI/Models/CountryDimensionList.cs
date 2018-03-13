using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleAPI.Models
{
    /// <summary>
    /// Country dimension list model
    /// </summary>
    public class CountryDimensionList : BasePaginationModel
    {
        #region Properties

        #endregion

        #region Constructor

        /// <summary>
        /// Create a new instance of the country dimension list
        /// </summary>
        public CountryDimensionList()
        {
            Items = new List<CountryDimension>();
        }

        #endregion 

        /// <summary>
        /// Stores the list of items
        /// </summary>
        public List<CountryDimension> Items = null;
    }
}