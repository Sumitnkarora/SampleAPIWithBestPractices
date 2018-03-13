using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleAPI.Models
{
    /// <summary>
    /// Region dimension list model
    /// </summary>
    public class RegionDimensionList : BasePaginationModel
    {
        #region Properties

        #endregion

        #region Constructor

        /// <summary>
        /// Create a new instance of the region dimension list
        /// </summary>
        public RegionDimensionList()
        {
            Items = new List<RegionDimension>();
        }

        #endregion 

        /// <summary>
        /// Stores the list of items
        /// </summary>
        public List<RegionDimension> Items = null;
    }
}