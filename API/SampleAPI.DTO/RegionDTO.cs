namespace SampleAPI.DTO
{
    /// <summary>
    /// Region Data Transfer Object
    /// </summary>
    public class RegionDTO : BaseDTO
    {
        #region Properties

        /// <summary>
        /// The name of the region / province / state
        /// </summary>
        public string Region { get; set; }

        #endregion
    }
}