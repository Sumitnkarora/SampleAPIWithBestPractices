using System;
using System.Collections.Generic;
using DAC.Cloud.Data.DataAccess;
using System.Data.SqlClient;
using SampleAPI.DTO;

namespace SampleAPI.DAL
{
    /// <summary>
    /// The Data Access Layer for Dimensional data
    /// Sample DAL written by Dave Elston
    /// </summary>
    public class Dimensions
    {
        #region Constructor

        private string _connectionString = string.Empty;

        /// <summary>
        /// Create an instance of the dimensional data access layer
        /// </summary>
        public Dimensions(string ConnectionString)
        {
            _connectionString = ConnectionString;
        }

        #endregion 

        #region Account Dimension

        /// <summary>
        /// Returns a list of accounts from the database
        /// </summary>
        /// <returns></returns>
        public AccountListDTO GetAccountDimensions(int Page, int PageSize, string AccountName = "")
        {
            AccountListDTO list = new AccountListDTO();

            SQLAzure.DbConnection dbConn = new SQLAzure.DbConnection(_connectionString);

            try
            {
                dbConn.Open();

                SQLAzure.RetryLogic.DbCommand dbComm = new SQLAzure.RetryLogic.DbCommand(dbConn);

                if (AccountName != string.Empty)
                {
                    dbComm.CommandText = String.Format("SELECT ID, AccountID, AccountName, TotalCount = COUNT(ID) OVER() FROM DimAccount WHERE AccountName LIKE '%' + @AccountName + '%' ORDER BY ID OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY", (Page * PageSize), PageSize);
                    dbComm.Parameters.Add(new SqlParameter("AccountName", AccountName));
                }
                else
                {
                    dbComm.CommandText = String.Format("SELECT ID, AccountID, AccountName, TotalCount = COUNT(ID) OVER() FROM DimAccount ORDER BY ID OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY", (Page * PageSize), PageSize);
                }

                System.Data.IDataReader rdr = dbComm.ExecuteReader(System.Data.CommandType.Text);

                while (rdr.Read())
                {
                    AccountDTO account = new AccountDTO();
                    account.ID = Convert.ToInt32(rdr["ID"]);
                    account.AccountID = Guid.Parse(rdr["AccountID"].ToString());
                    account.AccountName = rdr["AccountName"].ToString();

                    list.RecordCount = Convert.ToInt32(rdr["TotalCount"]);

                    list.Items.Add(account);
                }

                list.PageNumber = Page;
                list.PageSize = PageSize;
                list.TotalPages = GetNumberOfPagesAvailable(PageSize, list.RecordCount);
            }
            catch (SqlException)
            {
               throw;
            }
            finally
            {
                dbConn.Close();
            }

            return list;
        }

        /// <summary>
        /// Returns a single account dimension
        /// </summary>
        /// <returns></returns>
        public AccountDTO GetAccountDimension(int ID)
        {
            SQLAzure.DbConnection dbConn = new SQLAzure.DbConnection(_connectionString);
            AccountDTO account = new AccountDTO();

            try
            {
                dbConn.Open();

                SQLAzure.RetryLogic.DbCommand dbComm = new SQLAzure.RetryLogic.DbCommand(dbConn);
                dbComm.CommandText = "SELECT ID, AccountID, AccountName FROM DimAccount WHERE ID = @ID";

                dbComm.Parameters.Add(new SqlParameter("ID", ID));

                System.Data.IDataReader rdr = dbComm.ExecuteReader(System.Data.CommandType.Text);

                while (rdr.Read())
                {
                    account.ID = Convert.ToInt32(rdr["ID"]);
                    account.AccountID = Guid.Parse(rdr["AccountID"].ToString());
                    account.AccountName = rdr["AccountName"].ToString();
                }
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                dbConn.Close();
            }

            return account;
        }



        /// <summary>
        /// Creates an account record within the dimension table
        /// </summary>
        /// <param name="account"></param>
        public bool CreateAccountDimension(AccountDTO account)
        {
            bool Success = false;

            SQLAzure.DbConnection dbConn = new SQLAzure.DbConnection(_connectionString);

           
            try
            {
                string query = @"IF NOT EXISTS (SELECT AccountID from DimAccount WHERE AccountID = @AccountID)
                                    INSERT INTO DimAccount (AccountID, AccountName) VALUES (@AccountID, @AccountName)";

                dbConn.Open();

                SQLAzure.RetryLogic.DbCommand dbComm = new SQLAzure.RetryLogic.DbCommand(dbConn);
                dbComm.CommandText = query;

                dbComm.Parameters.Add(new SqlParameter("AccountID", account.AccountID));
                dbComm.Parameters.Add(new SqlParameter("AccountName", account.AccountName));

                dbComm.ExecuteNonQuery(System.Data.CommandType.Text);

                Success = true;
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                dbConn.Close();
            }

            return Success;
        }

        /// <summary>
        /// Updates an account record in the dimension table
        /// </summary>
        /// <param name="account"></param>
        public bool UpdateAccountDimension(AccountDTO account)
        {
            bool Success = false;
            SQLAzure.DbConnection dbConn = new SQLAzure.DbConnection(_connectionString);

            try
            {
                string query = @"UPDATE DimAccount SET AccountID = @AccountID, AccountName = @AccountName WHERE ID = @ID";

                dbConn.Open();

                SQLAzure.RetryLogic.DbCommand dbComm = new SQLAzure.RetryLogic.DbCommand(dbConn);
                dbComm.CommandText = query;

                dbComm.Parameters.Add(new SqlParameter("ID", account.ID));
                dbComm.Parameters.Add(new SqlParameter("AccountID", account.AccountID));
                dbComm.Parameters.Add(new SqlParameter("AccountName", account.AccountName));

                dbComm.ExecuteNonQuery(System.Data.CommandType.Text);
                Success = true;
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                dbConn.Close();
            }

            return Success;           
        }

        /// <summary>
        /// Deletes an Account Record by ID from the dimension table
        /// </summary>
        /// <param name="ID"></param>
        public bool DeleteAccountDimension(int ID)
        {
            bool Success = false;
            SQLAzure.DbConnection dbConn = new SQLAzure.DbConnection(_connectionString);

            try
            {
                string query = @"DELETE FROM DimAccount WHERE ID = @ID";

                dbConn.Open();

                SQLAzure.RetryLogic.DbCommand dbComm = new SQLAzure.RetryLogic.DbCommand(dbConn);
                dbComm.CommandText = query;

                dbComm.Parameters.Add(new SqlParameter("ID", ID));

                dbComm.ExecuteNonQuery(System.Data.CommandType.Text);

                Success = true;
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                dbConn.Close();
            }

            return Success;
        }


        #endregion

        #region Country Dimension

        /// <summary>
        /// Creates a country record within the dimension table
        /// </summary>
        /// <param name="country"></param>
        public bool CreateCountryDimension(CountryDTO country)
        {
            bool Success = false;

            SQLAzure.DbConnection dbConn = new SQLAzure.DbConnection(_connectionString);

            try
            {
                string query = @"IF NOT EXISTS (SELECT CountryCode from DimCountry WHERE CountryCode = @CountryCode)
                                    INSERT INTO DimCountry (CountryCode, CountryName) VALUES (@CountryCode, @CountryName)";
                dbConn.Open();

                SQLAzure.RetryLogic.DbCommand dbComm = new SQLAzure.RetryLogic.DbCommand(dbConn);
                dbComm.CommandText = query;

                dbComm.Parameters.Add(new SqlParameter("CountryCode", country.CountryCode));
                dbComm.Parameters.Add(new SqlParameter("CountryName", country.CountryName));

                dbComm.ExecuteNonQuery(System.Data.CommandType.Text);

                Success = true;
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                dbConn.Close();
            }

            return Success;
        }


        /// <summary>
        /// Updates a country record within the dimension table
        /// </summary>
        /// <param name="country"></param>
        public bool UpdateCountryDimension(CountryDTO country)
        {
            bool Success = false;

            SQLAzure.DbConnection dbConn = new SQLAzure.DbConnection(_connectionString);

            try
            {
                string query = @"UPDATE DimCountry SET CountryCode = @CountryCode, CountryName = @CountryName WHERE ID = @ID";
                dbConn.Open();

                SQLAzure.RetryLogic.DbCommand dbComm = new SQLAzure.RetryLogic.DbCommand(dbConn);
                dbComm.CommandText = query;

                dbComm.Parameters.Add(new SqlParameter("ID", country.ID));
                dbComm.Parameters.Add(new SqlParameter("CountryCode", country.CountryCode));
                dbComm.Parameters.Add(new SqlParameter("CountryName", country.CountryName));

                dbComm.ExecuteNonQuery(System.Data.CommandType.Text);

                Success = true;
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                dbConn.Close();
            }

            return Success;
        }


        /// <summary>
        /// Deletes a country Record by ID from the dimension table
        /// </summary>
        /// <param name="ID"></param>
        public bool DeleteCountryDimension(int ID)
        {
            bool Success = false;

            SQLAzure.DbConnection dbConn = new SQLAzure.DbConnection(_connectionString);

            try
            {
                string query = @"DELETE FROM DimCountry WHERE ID = @ID";

                dbConn.Open();

                SQLAzure.RetryLogic.DbCommand dbComm = new SQLAzure.RetryLogic.DbCommand(dbConn);
                dbComm.CommandText = query;

                dbComm.Parameters.Add(new SqlParameter("ID", ID));

                dbComm.ExecuteNonQuery(System.Data.CommandType.Text);

                Success = true;
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                dbConn.Close();
            }

            return Success;
        }

        /// <summary>
        /// Returns a list of accounts from the database
        /// </summary>
        /// <returns></returns>
        public CountryListDTO GetCountryDimensions(int Page, int PageSize, string CountryName = "")
        {
            CountryListDTO list = new CountryListDTO();

            SQLAzure.DbConnection dbConn = new SQLAzure.DbConnection(_connectionString);

            try
            {
                dbConn.Open();

                SQLAzure.RetryLogic.DbCommand dbComm = new SQLAzure.RetryLogic.DbCommand(dbConn);

                if (CountryName != string.Empty)
                {
                    dbComm.CommandText = String.Format("SELECT ID, CountryCode, CountryName, TotalCount = COUNT(ID) OVER() FROM DimCountry WHERE CountryName LIKE '%' + @CountryName + '%' ORDER BY ID OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY", (Page * PageSize), PageSize);
                    dbComm.Parameters.Add(new SqlParameter("CountryName", CountryName));
                }
                else
                {
                    dbComm.CommandText = String.Format("SELECT ID, CountryCode, CountryName, TotalCount = COUNT(ID) OVER() FROM DimCountry ORDER BY ID OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY", (Page * PageSize), PageSize);
                }

                System.Data.IDataReader rdr = dbComm.ExecuteReader(System.Data.CommandType.Text);

                while (rdr.Read())
                {
                    CountryDTO country = new CountryDTO();
                    country.ID = Convert.ToInt32(rdr["ID"]);
                    country.CountryCode = rdr["CountryCode"].ToString();
                    country.CountryName = rdr["CountryName"].ToString();

                    list.Items.Add(country);

                    list.RecordCount = Convert.ToInt32(rdr["TotalCount"]);
                }

                list.PageNumber = Page;
                list.PageSize = PageSize;
                list.TotalPages = GetNumberOfPagesAvailable(PageSize, list.RecordCount);
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                dbConn.Close();
            }

            return list;
        }


        /// <summary>
        /// Returns a single country dimension
        /// </summary>
        /// <returns></returns>
        public CountryDTO GetCountryDimension(int ID)
        {
            SQLAzure.DbConnection dbConn = new SQLAzure.DbConnection(_connectionString);
            CountryDTO country = new CountryDTO();

            try
            {
                dbConn.Open();

                SQLAzure.RetryLogic.DbCommand dbComm = new SQLAzure.RetryLogic.DbCommand(dbConn);
                dbComm.CommandText = "SELECT ID, CountryCode, CountryName FROM DimCountry WHERE ID = @ID";

                dbComm.Parameters.Add(new SqlParameter("ID", ID));

                System.Data.IDataReader rdr = dbComm.ExecuteReader(System.Data.CommandType.Text);

                while (rdr.Read())
                {
                    country.ID = Convert.ToInt32(rdr["ID"]);
                    country.CountryCode = rdr["CountryCode"].ToString();
                    country.CountryName = rdr["CountryName"].ToString();
                }
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                dbConn.Close();
            }

            return country;
        }

        #endregion

        #region Location Dimension


        /// <summary>
        /// Creates a location record within the dimension table
        /// </summary>
        /// <param name="location"></param>
        public bool CreateLocationDimension(LocationDTO location)
        {
            bool Success = false;

            SQLAzure.DbConnection dbConn = new SQLAzure.DbConnection(_connectionString);

            try
            {
                string query = @"IF NOT EXISTS (SELECT LocationID from DimLocation WHERE LocationID = @LocationID)
                                    INSERT INTO DimLocation (LocationID, LocationNumber) VALUES (@LocationID, @LocationNumber)";

                dbConn.Open();

                SQLAzure.RetryLogic.DbCommand dbComm = new SQLAzure.RetryLogic.DbCommand(dbConn);
                dbComm.CommandText = query;

                dbComm.Parameters.Add(new SqlParameter("LocationID", location.LocationID));
                dbComm.Parameters.Add(new SqlParameter("LocationNumber", location.LocationNumber));

                dbComm.ExecuteNonQuery(System.Data.CommandType.Text);

                Success = true;
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                dbConn.Close();
            }

            return Success;
        }


        /// <summary>
        /// Updates a location record within the dimension table
        /// </summary>
        /// <param name="location"></param>
        public bool UpdateLocationDimension(LocationDTO location)
        {
            bool Success = false;

            SQLAzure.DbConnection dbConn = new SQLAzure.DbConnection(_connectionString);

            try
            {
                string query = @"UPDATE DimLocation SET LocationID = @LocationID, LocationNumber = @LocationNumber WHERE ID = @ID";
                dbConn.Open();

                SQLAzure.RetryLogic.DbCommand dbComm = new SQLAzure.RetryLogic.DbCommand(dbConn);
                dbComm.CommandText = query;

                dbComm.Parameters.Add(new SqlParameter("ID", location.ID));
                dbComm.Parameters.Add(new SqlParameter("LocationID", location.LocationID));
                dbComm.Parameters.Add(new SqlParameter("LocationNumber", location.LocationNumber));

                dbComm.ExecuteNonQuery(System.Data.CommandType.Text);

                Success = true;
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                dbConn.Close();
            }

            return Success;
        }

        /// <summary>
        /// Deletes a location Record by ID from the dimension table
        /// </summary>
        /// <param name="ID"></param>
        public bool DeleteLocationDimension(int ID)
        {
            bool Success = false;

            SQLAzure.DbConnection dbConn = new SQLAzure.DbConnection(_connectionString);

            try
            {
                string query = @"DELETE FROM DimLocation WHERE ID = @ID";

                dbConn.Open();

                SQLAzure.RetryLogic.DbCommand dbComm = new SQLAzure.RetryLogic.DbCommand(dbConn);
                dbComm.CommandText = query;

                dbComm.Parameters.Add(new SqlParameter("ID", ID));

                dbComm.ExecuteNonQuery(System.Data.CommandType.Text);

                Success = true;
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                dbConn.Close();
            }

            return Success;
        }

        /// <summary>
        /// Returns a list of locations from the database
        /// </summary>
        /// <returns></returns>
        public LocationListDTO GetLocationDimensions(int Page, int PageSize)
        {
            LocationListDTO list = new LocationListDTO();

            SQLAzure.DbConnection dbConn = new SQLAzure.DbConnection(_connectionString);

            try
            {
                dbConn.Open();

                SQLAzure.RetryLogic.DbCommand dbComm = new SQLAzure.RetryLogic.DbCommand(dbConn);

                dbComm.CommandText = String.Format("SELECT ID, LocationID, LocationNumber, TotalCount = COUNT(ID) OVER() FROM DimLocation ORDER BY ID OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY", (Page * PageSize), PageSize);

                System.Data.IDataReader rdr = dbComm.ExecuteReader(System.Data.CommandType.Text);

                while (rdr.Read())
                {
                    LocationDTO location = new LocationDTO();
                    location.ID = Convert.ToInt32(rdr["ID"]);
                    location.LocationID = Guid.Parse(rdr["LocationID"].ToString());
                    location.LocationNumber = Convert.ToInt32(rdr["LocationNumber"].ToString());

                    list.Items.Add(location);

                    list.RecordCount = Convert.ToInt32(rdr["TotalCount"]);
                }

                list.PageNumber = Page;
                list.PageSize = PageSize;
                list.TotalPages = GetNumberOfPagesAvailable(PageSize, list.RecordCount);
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                dbConn.Close();
            }

            return list;
        }

        /// <summary>
        /// Returns a single location dimension
        /// </summary>
        /// <returns></returns>
        public LocationDTO GetLocationDimension(int ID)
        {
            SQLAzure.DbConnection dbConn = new SQLAzure.DbConnection(_connectionString);
            LocationDTO location = new LocationDTO();

            try
            {
                dbConn.Open();

                SQLAzure.RetryLogic.DbCommand dbComm = new SQLAzure.RetryLogic.DbCommand(dbConn);
                dbComm.CommandText = "SELECT ID, AccountID, AccountName FROM DimAccount WHERE ID = @ID";

                dbComm.Parameters.Add(new SqlParameter("ID", ID));

                System.Data.IDataReader rdr = dbComm.ExecuteReader(System.Data.CommandType.Text);

                while (rdr.Read())
                {
                    location.ID = Convert.ToInt32(rdr["ID"]);
                    location.LocationID = Guid.Parse(rdr["LocationID"].ToString());
                    location.LocationNumber = Convert.ToInt32(rdr["LocationNumber"].ToString());                }
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                dbConn.Close();
            }

            return location;
        }


        #endregion

        #region Region Dimension

        /// <summary>
        /// Creates a region within the dimension table
        /// </summary>
        /// <param name="region"></param>

        public bool CreateRegionDimension(RegionDTO region)
        {
            bool Success = false;

            SQLAzure.DbConnection dbConn = new SQLAzure.DbConnection(_connectionString);

            try
            {
                string query = @"IF NOT EXISTS (SELECT Region from DimRegion WHERE Region = @Region)
                                    INSERT INTO DimRegion (Region) VALUES (@Region)";

                dbConn.Open();

                SQLAzure.RetryLogic.DbCommand dbComm = new SQLAzure.RetryLogic.DbCommand(dbConn);
                dbComm.CommandText = query;

                dbComm.Parameters.Add(new SqlParameter("Region", region.Region));

                dbComm.ExecuteNonQuery(System.Data.CommandType.Text);

                Success = true;
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                dbConn.Close();
            }

            return Success;
        }

        /// <summary>
        /// Updates a region record within the dimension table
        /// </summary>
        /// <param name="region"></param>
        public bool UpdateRegionDimension(RegionDTO region)
        {
            bool Success = true;

            SQLAzure.DbConnection dbConn = new SQLAzure.DbConnection(_connectionString);

            try
            {
                string query = @"UPDATE DimRegion SET Region = @Region WHERE ID = @ID";
                dbConn.Open();

                SQLAzure.RetryLogic.DbCommand dbComm = new SQLAzure.RetryLogic.DbCommand(dbConn);
                dbComm.CommandText = query;

                dbComm.Parameters.Add(new SqlParameter("ID", region.ID));
                dbComm.Parameters.Add(new SqlParameter("Region", region.Region));

                dbComm.ExecuteNonQuery(System.Data.CommandType.Text);

                Success = true;
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                dbConn.Close();
            }

            return Success;
        }

        /// <summary>
        /// Deletes a location Record by ID from the dimension table
        /// </summary>
        /// <param name="ID"></param>
        public bool DeleteRegionDimension(int ID)
        {
            bool Success = false;

            SQLAzure.DbConnection dbConn = new SQLAzure.DbConnection(_connectionString);

            try
            {
                string query = @"DELETE FROM DimRegion WHERE ID = @ID";

                dbConn.Open();

                SQLAzure.RetryLogic.DbCommand dbComm = new SQLAzure.RetryLogic.DbCommand(dbConn);
                dbComm.CommandText = query;

                dbComm.Parameters.Add(new SqlParameter("ID", ID));

                dbComm.ExecuteNonQuery(System.Data.CommandType.Text);

                Success = true;
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                dbConn.Close();
            }

            return Success;
        }


        /// <summary>
        /// Returns a list of regions from the database
        /// </summary>
        /// <returns></returns>
        public RegionListDTO GetRegionDimensions(int Page, int PageSize, string Region = "")
        {
            RegionListDTO list = new RegionListDTO();

            SQLAzure.DbConnection dbConn = new SQLAzure.DbConnection(_connectionString);

            try
            {
                dbConn.Open();

                SQLAzure.RetryLogic.DbCommand dbComm = new SQLAzure.RetryLogic.DbCommand(dbConn);

                if (Region != string.Empty)
                {
                    dbComm.CommandText = String.Format("SELECT ID, Region, TotalCount = COUNT(ID) OVER() FROM DimRegion WHERE Region LIKE '%' + @Region + '%' ORDER BY ID OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY", (Page * PageSize), PageSize);

                    dbComm.Parameters.Add(new SqlParameter("Region", Region));
                }
                else
                {
                    dbComm.CommandText = String.Format("SELECT ID, Region, TotalCount = COUNT(ID) OVER() FROM DimRegion ORDER BY ID OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY", (Page * PageSize), PageSize);
                }

                System.Data.IDataReader rdr = dbComm.ExecuteReader(System.Data.CommandType.Text);

                while (rdr.Read())
                {
                    RegionDTO region = new RegionDTO();
                    region.ID = Convert.ToInt32(rdr["ID"]);
                    region.Region = rdr["Region"].ToString();

                    list.Items.Add(region);

                    list.RecordCount = Convert.ToInt32(rdr["TotalCount"]);
                }

                list.PageNumber = Page;
                list.PageSize = PageSize;
                list.TotalPages = GetNumberOfPagesAvailable(PageSize, list.RecordCount);
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                dbConn.Close();
            }

            return list;
        }


        /// <summary>
        /// Returns a single country dimension
        /// </summary>
        /// <returns></returns>
        public RegionDTO GetRegionDimension(int ID)
        {
            SQLAzure.DbConnection dbConn = new SQLAzure.DbConnection(_connectionString);
            RegionDTO region = new RegionDTO();

            try
            {
                dbConn.Open();

                SQLAzure.RetryLogic.DbCommand dbComm = new SQLAzure.RetryLogic.DbCommand(dbConn);
                dbComm.CommandText = "SELECT ID, Region FROM DimRegion WHERE ID = @ID";

                dbComm.Parameters.Add(new SqlParameter("ID", ID));

                System.Data.IDataReader rdr = dbComm.ExecuteReader(System.Data.CommandType.Text);

                while (rdr.Read())
                {
                    region.ID = Convert.ToInt32(rdr["ID"]);
                    region.Region = rdr["Region"].ToString();
                }
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                dbConn.Close();
            }

            return region;
        }


        #endregion

        #region CommonMethods

        /// <summary>
        /// Returns the number of available pages
        /// </summary>
        /// <param name="RecordsPerPage"></param>
        /// <param name="TotalNumberOfRecords"></param>
        /// <returns></returns>
        public int GetNumberOfPagesAvailable(int RecordsPerPage, int TotalNumberOfRecords)
        {
            // If the total number of records in the remainder is zero, 
            // then don't add +1 to the number of pages
            if (TotalNumberOfRecords % RecordsPerPage == 0)
            {
                return (TotalNumberOfRecords / RecordsPerPage);
            }
            else
            {
                return (TotalNumberOfRecords / RecordsPerPage) + 1;
            }
        }

        #endregion 
    }
}