using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleAPI.Models
{
    /// <summary>
    /// Location dimension list model
    /// </summary>
    public class LocationDimensionList : BasePaginationModel
    {
        #region Properties

        #endregion

        #region Constructor

        /// <summary>
        /// Create a new instance of the location dimension list
        /// </summary>
        public LocationDimensionList()
        {
            Items = new List<LocationDimension>();
        }

        #endregion 

        /// <summary>
        /// Stores the list of items
        /// </summary>
        public List<LocationDimension> Items = null;
    }
}