using System;
using System.Collections.Generic;
using SampleAPI.DTO;

namespace SampleAPI.BLL
{
    /// <summary>
    /// The dimension business logic layer, provides access to dimensional data from the data access layer.
    /// </summary>
    public class DimensionsBLL
    {
        #region Constructor

        /// <summary>
        /// A private reference to the dimension DAL object that will be used for this instance of the business logic layer.
        /// </summary>
        private DAL.Dimensions DimensionDAL { get; set; }

        /// <summary>
        /// Create a new instance of the business logic layer for dimensional data
        /// </summary>
        /// <param name="ConnectionString">Provide the database connection string for the DAL</param>
        public DimensionsBLL(string ConnectionString)
        {
            // Initialize the data access layer for dimensional data
            DimensionDAL = new DAL.Dimensions(ConnectionString);
        }

        #endregion 

        #region Accounts

        /// <summary>
        /// Returns an account dimension
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public AccountDTO GetAccountDimension(int ID)
        {
            try
            {
                AccountDTO accountDTO = DimensionDAL.GetAccountDimension(ID);

                return accountDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Returns a list of account dimensions
        /// </summary>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public AccountListDTO GetAccountDimensions(int Page, int PageSize, string AccountName = "")
        {
            try
            {
                AccountListDTO list = DimensionDAL.GetAccountDimensions(Page, PageSize, AccountName);

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Creates a new account dimension
        /// </summary>
        public void CreateAccountDimension(AccountDTO account)
        {
            try
            {
                DimensionDAL.CreateAccountDimension(account);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates an account
        /// </summary>
        public void UpdateAccountDimension(AccountDTO account)
        {
            try
            {
                DimensionDAL.UpdateAccountDimension(account);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes an account dimension
        /// </summary>
        public void DeleteAccountDimension(int ID)
        {
            try
            {
                DimensionDAL.DeleteAccountDimension(ID);
            }
            catch (Exception)
            {
                throw;
            }
        }


        #endregion

        #region Countries


        /// <summary>
        /// Returns a country dimension
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public CountryDTO GetCountryDimension(int ID)
        {
            try
            {
                CountryDTO countryDTO = DimensionDAL.GetCountryDimension(ID);

                return countryDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Returns a list of country dimensions
        /// </summary>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public CountryListDTO GetCountryDimensions(int Page, int PageSize, string CountryName = "")
        {
            try
            {
                CountryListDTO list = DimensionDAL.GetCountryDimensions(Page, PageSize, CountryName);

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Creates a new country dimension
        /// </summary>
        public void CreateCountryDimension(CountryDTO country)
        {
            try
            {
                DimensionDAL.CreateCountryDimension(country);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates a country dimension
        /// </summary>
        public void UpdateCountryDimension(CountryDTO country)
        {
            try
            {
                DimensionDAL.UpdateCountryDimension(country);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes a country dimension
        /// </summary>
        public void DeleteCountryDimension(int ID)
        {
            try
            {
                DimensionDAL.DeleteCountryDimension(ID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Locations

        /// <summary>
        /// Returns a list of location dimensions
        /// </summary>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        public LocationListDTO GetLocationDimensions(int Page, int PageSize)
        {
            try
            {
                LocationListDTO list = DimensionDAL.GetLocationDimensions(Page, PageSize);
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Returns a location dimension
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public LocationDTO GetLocationDimension(int ID)
        {
            try
            {
                LocationDTO locationDTO = DimensionDAL.GetLocationDimension(ID);

                return locationDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Creates a new country dimension
        /// </summary>
        public void CreateLocationDimension(LocationDTO location)
        {
            try
            {
                DimensionDAL.CreateLocationDimension(location);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates a location
        /// </summary>
        public void UpdateLocationDimension(LocationDTO location)
        {
            try
            {
                DimensionDAL.UpdateLocationDimension(location);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes a location dimension
        /// </summary>
        public void DeleteLocationDimension(int ID)
        {
            try
            {
                DimensionDAL.DeleteLocationDimension(ID);
            }
            catch (Exception)
            {
                throw;
            }
        }


        #endregion

        #region Regions

        /// <summary>
        /// Returns a list of region dimensions
        /// </summary>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public RegionListDTO GetRegionDimensions(int Page, int PageSize, string Region = "")
        {
            try
            {
                RegionListDTO list = DimensionDAL.GetRegionDimensions(Page, PageSize, Region);
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Returns a region dimension
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public RegionDTO GetRegionDimension(int ID)
        {
            try
            {
                RegionDTO locationDTO = DimensionDAL.GetRegionDimension(ID);

                return locationDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Creates a new region dimension
        /// </summary>

        public void CreateRegionDimension(RegionDTO region)
        {
            try
            {
                DimensionDAL.CreateRegionDimension(region);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates a region dimension
        /// </summary>
        /// <param name="region"></param>
        public void UpdateRegionDimension(RegionDTO region)
        {
            try
            {
                DimensionDAL.UpdateRegionDimension(region);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes a region dimension
        /// </summary>
        public void DeleteRegionDimension(int ID)
        {
            try
            {
                DimensionDAL.DeleteLocationDimension(ID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}