using System;

namespace SampleAPI.Global
{
    /// <summary>
    /// Stores globally used static variables that are used throughout the application
    /// </summary>
    public static class GlobalVariables
    {
        /// <summary>
        /// The main database connection
        /// </summary>
        public static string ConnectionString = string.Empty;

        /// <summary>
        /// The valid API key that is allowed to process requests
        /// </summary>
        public static Guid APIKey = Guid.Empty;

        /// <summary>
        /// The maximum number of records returned by the API
        /// </summary>
        public static int MaximumRecords = 1000;
    }
}