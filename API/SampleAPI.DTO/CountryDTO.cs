namespace SampleAPI.DTO
{
    /// <summary>
    /// Country Data Transfer Object
    /// </summary>
    public class CountryDTO : BaseDTO
    {
        #region Properties

        /// <summary>
        /// The two digit ISO standard country code
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// The name of the country
        /// </summary>
        public string CountryName { get; set; }

        #endregion
    }

}