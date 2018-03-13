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

namespace SampleAPI.v2.Controllers
{
    /// <summary>
    /// Handles CRUD operations for the Account dimension
    /// Sample API written by Dave Elston
    /// </summary>
    [SecurityFilter]
    [RoutePrefix("v2")]
    public class AccountDimensionsControllerV2 : SampleAPI.v1.Controllers.AccountDimensionsController
    {
        private DimensionsBLL _dimensionBLL;

        /// <summary>
        /// Create a new instance
        /// </summary>
        public AccountDimensionsControllerV2()
        {
            _dimensionBLL = new DimensionsBLL(Global.GlobalVariables.ConnectionString);
        }

        ///// <summary>
        ///// Gets a list of account dimensions
        ///// </summary>
        ///// <param name="Page">The page you're requesting</param>
        ///// <param name="PageSize">Number of items requested per page</param>
        ///// <returns>List of Account Dimension objects</returns>
        //[SwaggerOperation("Get Account Dimensions",Tags = new[] { "Account Dimensions v2" })]
        //[SwaggerResponse(HttpStatusCode.OK, "Success", typeof(List<AccountDimension>))]
        //[SwaggerResponse(HttpStatusCode.NoContent, "No Content", typeof(string))]
        //[SwaggerResponse(HttpStatusCode.BadRequest, "Invalid Request Format", typeof(string))]
        //[SwaggerResponse(HttpStatusCode.Unauthorized, "Access Denied", typeof(string))]
        //[SwaggerResponse(HttpStatusCode.InternalServerError, "Unknown Error", typeof(string))]
        //[Route("/dimensions/accounts")]
        //[HttpGet]
        //public new IHttpActionResult Get(int Page, int PageSize)
        //{
        //    ValidateModel();

        //    try
        //    {
        //        List<AccountDTO> list = _dimensionBLL.GetAccountDimensions(Page, PageSize);
        //        List<AccountDimension> accountDimensions = new List<AccountDimension>();

        //        foreach (AccountDTO l in list)
        //        {
        //            accountDimensions.Add((AccountDimension)l);
        //        }

        //        if (accountDimensions.Count > 0)
        //        {
        //            return Ok(accountDimensions);
        //        }
        //        else
        //        {
        //            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NoContent);
        //            response.Content = new StringContent("No Content");

        //            return ResponseMessage(response);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
        //        response.Content = new StringContent("Unknown Error");

        //        // Insert Logging here
        //        Console.WriteLine(ex);
        //        return ResponseMessage(response);
        //    }
        //}

        /// <summary>
        /// Gets a single account dimension
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [SwaggerOperation("Get Account Dimension 2", Tags = new[] { "Account Dimensions v2" })]
        [SwaggerResponse(HttpStatusCode.OK, "Success", typeof(AccountDimension))]
        [SwaggerResponse(HttpStatusCode.NoContent, "No Content", typeof(string))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid Request Format", typeof(string))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Access Denied", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Unknown Error", typeof(string))]
        [Route("dimensions/accounts2/{id}")]
        [HttpGet]
        public IHttpActionResult GetSingle2(int id)
        {
            //ValidateModel();

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
        /// Gets a single account dimension
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [SwaggerOperation("Get Account Dimension",Tags = new[] { "Account Dimensions v2" })]
        [SwaggerResponse(HttpStatusCode.OK, "Success", typeof(AccountDimension))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid Request Format", typeof(string))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Access Denied", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Unknown Error", typeof(string))]
        [Route("dimensions/accounts/{id}")]
        [HttpGet]
        public new IHttpActionResult GetSingle(int id)
        {
            //ValidateModel();

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
        [SwaggerOperation("Create Account Dimension", Tags = new[] { "Account Dimensions v2" })]
        [SwaggerResponse(HttpStatusCode.OK, "Success", typeof(string))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid Request Format", typeof(string))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Access Denied", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Unknown Error", typeof(string))]
        [Route("dimensions/accounts")]
        [HttpPost]
        public new IHttpActionResult CreateAccount([FromBody]AccountDimension account)
        {
            //ValidateModel();

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
        [SwaggerOperation("Update Account Dimension", Tags = new[] { "Account Dimensions v2" })]
        [SwaggerResponse(HttpStatusCode.OK, "Success", typeof(string))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid Request Format", typeof(string))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Access Denied", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Unknown Error", typeof(string))]
        [Route("dimensions/accounts")]
        [HttpPut]
        public new IHttpActionResult Put([FromBody]AccountDimension account)
        {
            //ValidateModel();

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
        [SwaggerOperation("Delete Account Dimension", Tags = new[] { "Account Dimensions v2" })]
        [SwaggerResponse(HttpStatusCode.OK, "Success", typeof(string))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid Request Format", typeof(string))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Access Denied", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Unknown Error", typeof(string))]
        [Route("dimensions/accounts")]
        [HttpDelete]
        public new IHttpActionResult Delete(int id)
        {
            //ValidateModel();

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
