using System.ComponentModel.DataAnnotations;
using SampleAPI.DTO;

namespace SampleAPI.Models
{
    /// <summary>
    /// Stores dimensional data regarding a region
    /// </summary>
    public class RegionDimension : BaseSingleRecordModel
    {
        #region Properties

        /// <summary>
        /// The name of the region / province / state
        /// </summary>
        [Required, StringLength(255)]
        public string Region { get; set; }
        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of the RegionDimension object
        /// </summary>
        /// <param name="regionID"></param>
        /// <param name="region"></param>
        public RegionDimension(int regionID, string region)
        {
            ID = regionID;
            Region = region;
        }

        /// <summary>
        /// Create a new instance of the RegionDimension object
        /// </summary>
        /// <param name="region"></param>
        public RegionDimension(string region)
        {
            Region = region;
        }

        /// <summary>
        /// Create a new instance of an empty RegionDimension object
        /// </summary>
        public RegionDimension()
        {

        }

        #endregion

        #region Conversions
        /// <summary>
        /// Casts RegionDTO to RegionDimension
        /// </summary>
        /// <param name="a"></param>
        public static explicit operator RegionDimension(RegionDTO a)
        {
            RegionDimension ad = new RegionDimension();
            ad.ID = a.ID;
            ad.Region = a.Region;

            return ad;
        }


        /// <summary>
        /// Returns the corresponding DTO object
        /// </summary>
        /// <returns></returns>
        public RegionDTO GetDTO()
        {
            RegionDTO ad = new RegionDTO();
            ad.Region = Region;
            ad.ID = ID;

            return ad;
        }
        #endregion 
    }
}