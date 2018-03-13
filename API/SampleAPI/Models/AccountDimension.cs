using System;
using System.ComponentModel.DataAnnotations;
using SampleAPI.DTO;

namespace SampleAPI.Models
{
    /// <summary>
    /// Stores dimensional information regarding an account
    /// </summary>
    public class AccountDimension : BaseSingleRecordModel
    {
        #region Properties

        /// <summary>
        /// The account id for this account
        /// </summary>
        [Required(ErrorMessage = "AccountID is required")]
        public Guid AccountID { get; set; }

        /// <summary>
        /// The name of the account
        /// </summary>
        [StringLength(255), Required(ErrorMessage = "AccountName is required")]
        public string AccountName { get; set; }
        

        #endregion

        #region Constructors
        /// <summary>
        /// Create a new instance of an account object
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="accountName"></param>
        public AccountDimension(Guid accountId, string accountName)
        {
            AccountID = accountId;
            AccountName = accountName;
        }

        /// <summary>
        /// Create a new instance of an account object
        /// </summary>
        /// <param name="acctId"></param>
        /// <param name="accountId"></param>
        /// <param name="accountName"></param>
        public AccountDimension(int acctId, Guid accountId, string accountName)
        {
            ID = acctId;
            AccountID = accountId;
            AccountName = accountName;
        }

        /// <summary>
        /// Create a new instance of an empty Account Dimension
        /// </summary>
        public AccountDimension()
        {

        }

        #endregion

        #region Conversions
        /// <summary>
        /// Casts AccountDTO to AccountDimension
        /// </summary>
        /// <param name="a"></param>
        public static explicit operator AccountDimension(AccountDTO a)
        {
            AccountDimension ad = new AccountDimension();
            ad.AccountID = a.AccountID;
            ad.AccountName = a.AccountName;
            ad.ID = a.ID;

            return ad;
        }

        /// <summary>
        /// Returns the corresponding DTO object
        /// </summary>
        /// <returns></returns>
        public AccountDTO GetDTO()
        {
            AccountDTO ad = new AccountDTO();
            ad.AccountID = AccountID;
            ad.AccountName = AccountName;
            ad.ID = ID;

            return ad;
        }
        
        #endregion 
    }
}