using System;
namespace SampleAPI.DTO
{
    /// <summary>
    /// Account Data Transfer Object
    /// </summary>
    public class AccountDTO : BaseDTO
    {
        #region Properties

        /// <summary>
        /// The account id for this account
        /// </summary>
        public Guid AccountID { get; set; }

        /// <summary>
        /// The name of the account
        /// </summary>
        public string AccountName { get; set; }

        #endregion
    }
}