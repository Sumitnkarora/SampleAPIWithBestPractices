using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using SampleAPI.Filters;
using SampleAPI.Models;
using SampleAPI.Common;
using SampleAPI.BLL;
using Microsoft.Web.Http;
using SampleAPI.DTO;
using SampleAPI.Examples;

namespace SampleAPI.v1.Controllers
{
    /// <summary>
    /// Handles CRUD operations for the Account dimension
    /// Sample API written by Dave Elston
    /// </summary>
    [SecurityFilter]
    [RoutePrefix("v1")]
    public class AccountDimensionsController : ControllerExtension
    {
        private DimensionsBLL _dimensionBLL;

        /// <summary>
        /// Create a new instance
        /// </summary>
        public AccountDimensionsController()
        {
            _dimensionBLL = new DimensionsBLL(Global.GlobalVariables.ConnectionString);
        }

        /// <summary>
        /// Gets a list of account dimensions
        /// </summary>
        /// <param name="Page">The page you're requesting</param>
        /// <param name="PageSize">Number of items requested per page</param>
        /// <param name="AccountName">A like query on the account name [Optional]</param>
        /// <returns>List of Account Dimension objects</returns>
        [SwaggerOperation("Get Account Dimensions",Tags = new[] { "Account Dimensions" })]
        [SwaggerResponse(HttpStatusCode.OK, "Success", typeof(AccountDimensionList))]
        [SwaggerResponse(HttpStatusCode.NoContent, "No Content", typeof(string))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid Request Format", typeof(string))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Access Denied", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Unknown Error", typeof(string))]
        [SwaggerInputExamples(typeof(AccountDimension), typeof(AccountDimensionList))]
        [Route("dimensions/accounts")]
        [HttpGet]
        public IHttpActionResult Get(int Page, int PageSize, string AccountName = "")
        {
            ValidateModel();

            try
            {
                AccountListDTO list = _dimensionBLL.GetAccountDimensions(Page, PageSize, AccountName);
                AccountDimensionList accountDimensions = new AccountDimensionList();

                foreach (AccountDTO l in list.Items)
                {
                    accountDimensions.Items.Add((AccountDimension)l);
                }

                if (accountDimensions.Items.Count > 0)
                {
                    accountDimensions.PageNumber = list.PageNumber;
                    accountDimensions.PageSize = list.PageSize;
                    accountDimensions.RecordCount = list.RecordCount;
                    accountDimensions.TotalPages = list.TotalPages;

                    return Ok(accountDimensions);
                }
                else
                {
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NoContent);
                    response.Content = new StringContent("No Content");

                    return ResponseMessage(response);
                }

            }
            catch (Exception ex)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                response.Content = new StringContent("Unknown Error");

                // Insert Logging here
                Console.WriteLine(ex);
                return ResponseMessage(response);
            }
        }


        /// <summary>
        /// Gets a single account dimension
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [SwaggerOperation("Get Account Dimension",Tags = new[] { "Account Dimensions" })]
        [SwaggerResponse(HttpStatusCode.OK, "Success", typeof(AccountDimension))]
        [SwaggerResponse(HttpStatusCode.NoContent, "No Content", typeof(string))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid Request Format", typeof(string))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Access Denied", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Unknown Error", typeof(string))]
        [SwaggerInputExamples(typeof(AccountDimension), typeof(AccountDimensionExample))]
        [Route("dimensions/accounts/{id}")]
        [HttpGet]
        public IHttpActionResult GetSingle(int id)
        {
            ValidateModel();

            try
            {
                AccountDimension accountDimension = (AccountDimension)_dimensionBLL.GetAccountDimension(id);

                if (accountDimension != null && accountDimension.ID != 0)
                {
                    return Ok(accountDimension);
                }
                else
                {
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NoContent);
                    response.Content = new StringContent("No Content");

                    return ResponseMessage(response);
                }
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                response.Content = new StringContent("Unknown Error");

                // Insert Logging here
                Console.WriteLine(ex);

                return ResponseMessage(response);
            }
        }

        /// <summary>
        /// Creates a new account dimension
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [SwaggerOperation("Create Account Dimension", Tags = new[] { "Account Dimensions" })]
        [SwaggerResponse(HttpStatusCode.OK, "Success", typeof(string))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid Request Format", typeof(string))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Access Denied", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Unknown Error", typeof(string))]
        [SwaggerInputExamples(typeof(AccountDimension), typeof(AccountDimensionExample))]
        [Route("dimensions/accounts")]
        [HttpPost]
        public IHttpActionResult CreateAccount([FromBody]AccountDimension account)
        {
            ValidateModel();

            try
            {
                _dimensionBLL.CreateAccountDimension(account.GetDTO());

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent("Success");
                return ResponseMessage(response);
            }
            catch (Exception ex)
            {
                // Insert Logging here
                Console.WriteLine(ex);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                response.Content = new StringContent("Unknown Error");
                return ResponseMessage(response);
            }
        }


        /// <summary>
        /// Updates an existing account dimension
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [SwaggerOperation("Update Account Dimension", Tags = new[] { "Account Dimensions" })]
        [SwaggerResponse(HttpStatusCode.OK, "Success", typeof(string))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid Request Format", typeof(string))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Access Denied", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Unknown Error", typeof(string))]
        [SwaggerInputExamples(typeof(AccountDimension), typeof(AccountDimensionExample))]
        [Route("dimensions/accounts")]
        [HttpPut]
        public IHttpActionResult Put([FromBody]AccountDimension account)
        {
            ValidateModel();

            try
            {
                _dimensionBLL.UpdateAccountDimension(account.GetDTO());

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent("Success");

                return ResponseMessage(response);
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                response.Content = new StringContent("Unknown Error");

                // Insert Logging here
                Console.WriteLine(ex);
                return ResponseMessage(response);
            }
        }

        /// <summary>
        /// Deletes an account dimension
        /// </summary>
        /// <param name="id">Pass in a valid account dimension ID to delete it</param>
        /// <returns></returns>
        [SwaggerOperation("Delete Account Dimension", Tags = new[] { "Account Dimensions" })]
        [SwaggerResponse(HttpStatusCode.OK, "Success", typeof(string))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid Request Format", typeof(string))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Access Denied", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Unknown Error", typeof(string))]
        [Route("dimensions/accounts")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            ValidateModel();

            try
            {
                _dimensionBLL.DeleteAccountDimension(id);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent("Success");
                return ResponseMessage(response);
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                response.Content = new StringContent("Unknown Error");

                // Insert Logging here
                Console.WriteLine(ex);
                return ResponseMessage(response);
            }
        }
    }
}
