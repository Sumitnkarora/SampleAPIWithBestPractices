using System.ComponentModel.DataAnnotations;

namespace SampleAPI.Models
{

    /// <summary>
    /// Dimension base class
    /// </summary>
    public class BaseSingleRecordModel
    {
        #region Properties

        /// <summary>
        /// The ID of the dimension object
        /// </summary>
        [Required]
        public int ID { get; set; }

        /// <summary>
        /// Additional information about this dimension object [Not yet implemented]
        /// </summary>
        public string DimensionInformation { get; set; }

        #endregion 
    }
}