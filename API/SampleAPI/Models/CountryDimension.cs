using System.ComponentModel.DataAnnotations;
using SampleAPI.DTO;

namespace SampleAPI.Models
{
    /// <summary>
    /// Stores dimensional information regarding a country
    /// </summary>
    public class CountryDimension : BaseSingleRecordModel
    {
        #region Properties

        /// <summary>
        /// The two digit ISO standard country code
        /// </summary>
        [StringLength(2), Required]
        public string CountryCode { get; set; }

        /// <summary>
        /// The name of the country
        /// </summary>
        [StringLength(50), Required]
        public string CountryName { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of the DimCountry object
        /// </summary>
        /// <param name="countryCode"></param>
        /// <param name="countryName"></param>
        public CountryDimension(string countryCode, string countryName)
        {
            CountryCode = countryCode;
            CountryName = countryName;
        }

        /// <summary>
        /// Create a new instance of the DimCountry object
        /// </summary>
        /// <param name="countryId"></param>
        /// <param name="countryCode"></param>
        /// <param name="countryName"></param>
        public CountryDimension(int countryId, string countryCode, string countryName)
        {
            ID = countryId;
            CountryCode = countryCode;
            CountryName = countryName;
        }

        /// <summary>
        /// Create a new instance of an empty Country Dimension
        /// </summary>
        public CountryDimension()
        {

        }

        #endregion 

        #region Conversions

        /// <summary>
        /// Casts CountryDTO to CountryDimension
        /// </summary>
        /// <param name="a"></param>
        public static explicit operator CountryDimension(CountryDTO a)
        {
            CountryDimension ad = new CountryDimension();
            ad.CountryCode = a.CountryCode;
            ad.CountryName = a.CountryName;
            ad.ID = a.ID;

            return ad;
        }

        /// <summary>
        /// Returns the corresponding DTO object
        /// </summary>
        /// <returns></returns>
        public CountryDTO GetDTO()
        {
            CountryDTO ad = new CountryDTO();
            ad.CountryCode = CountryCode;
            ad.CountryName = CountryName;
            ad.ID = ID;

            return ad;
        }

        #endregion 
    }
}