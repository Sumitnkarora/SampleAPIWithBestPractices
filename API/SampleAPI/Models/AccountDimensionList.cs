using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleAPI.Models
{
    /// <summary>
    /// Account dimension list model
    /// </summary>
    public class AccountDimensionList : BasePaginationModel
    {
        #region Properties

        #endregion

        #region Constructor

        /// <summary>
        /// Create a new instance of the account dimension list
        /// </summary>
        public AccountDimensionList()
        {
            Items = new List<AccountDimension>();
        }

        #endregion 

        /// <summary>
        /// Stores the list of items
        /// </summary>
        public List<AccountDimension> Items = null;
    }
}