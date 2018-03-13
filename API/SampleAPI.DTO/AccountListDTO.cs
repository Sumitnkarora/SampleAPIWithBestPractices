using System.Collections.Generic;

namespace SampleAPI.DTO
{
    /// <summary>
    /// Contains pagination data and the list of account DTO objects
    /// </summary>
    public class AccountListDTO
    {
        /// <summary>
        /// The list of items
        /// </summary>
        public List<AccountDTO> Items = new List<AccountDTO>();

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
    }
}
