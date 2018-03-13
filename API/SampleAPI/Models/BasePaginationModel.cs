using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SampleAPI.Models
{
    /// <summary>
    /// Multi page pagination model, includes consistent properties for paging
    /// </summary>
    public class BasePaginationModel
    {
        #region Properties

        /// <summary>
        /// The current page number
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// The total number of pages available
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// The number of records in total
        /// </summary>
        public int RecordCount { get; set; }

        /// <summary>
        /// The size of the current page
        /// </summary>
        public int PageSize { get; set; }

        #endregion 


    }
}