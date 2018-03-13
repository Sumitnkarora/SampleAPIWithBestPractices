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

namespace SampleAPI.v1.Controllers
{
    /// <summary>
    /// Handles CRUD operations for the Account dimension
    /// Sample API written by Dave Elston
    /// </summary>
    [SecurityFilter]
    [RoutePrefix("v1")]
    public class CountryDimensionsController : ControllerExtension
    {
        private DimensionsBLL _dimensionBLL;

        /// <summary>
        /// Create a new instance
        /// </summary>
        public CountryDimensionsController()
        {
            _dimensionBLL = new DimensionsBLL(Global.GlobalVariables.ConnectionString);
        }




        /// <summary>
        /// Gets a list of country dimensions
        /// </summary>
        /// <param name="Page">The page you're requesting</param>
        /// <param name="PageSize">Number of items requested per page</param>
        /// <param name="CountryName">A like query on the country name [Optional]</param>
        /// <returns>List of Account Dimension objects</returns>
        [SwaggerOperation("Get Country Dimensions", Tags = new[] { "Country Dimensions" })]
        [SwaggerResponse(HttpStatusCode.OK, "Success", typeof(CountryDimensionList))]
        [SwaggerResponse(HttpStatusCode.NoContent, "No Content", typeof(string))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid Request Format", typeof(string))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Access Denied", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Unknown Error", typeof(string))]
        [Route("dimensions/countries")]
        [HttpGet]
        public IHttpActionResult Get(int Page, int PageSize, string CountryName = "")
        {
            ValidateModel();

            try
            {
                CountryListDTO list = _dimensionBLL.GetCountryDimensions(Page, PageSize, CountryName);
                CountryDimensionList countryDimensions = new CountryDimensionList();

                foreach (CountryDTO c in list.Items)
                {
                    countryDimensions.Items.Add((CountryDimension)c);
                }

                if (countryDimensions.Items.Count > 0)
                {
                    countryDimensions.PageNumber = list.PageNumber;
                    countryDimensions.PageSize = list.PageSize;
                    countryDimensions.RecordCount = list.RecordCount;
                    countryDimensions.TotalPages = list.TotalPages;

                    return Ok(countryDimensions);
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
        /// Gets a single country dimension
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [SwaggerOperation("Get Country Dimension", Tags = new[] { "Country Dimensions" })]
        [SwaggerResponse(HttpStatusCode.OK, "Success", typeof(CountryDimension))]
        [SwaggerResponse(HttpStatusCode.NoContent, "No Content", typeof(string))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid Request Format", typeof(string))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Access Denied", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Unknown Error", typeof(string))]
        [Route("dimensions/countries/{id}")]
        [HttpGet]
        public IHttpActionResult GetSingle(int id)
        {
            ValidateModel();

            try
            {
                CountryDimension countryDimension = (CountryDimension)_dimensionBLL.GetCountryDimension(id);

                if (countryDimension != null && countryDimension.ID != 0)
                {
                    return Ok(countryDimension);
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
        /// Creates a new country dimension
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        [SwaggerOperation("Create Country Dimension", Tags = new[] { "Country Dimensions" })]
        [SwaggerResponse(HttpStatusCode.OK, "Success", typeof(string))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid Request Format", typeof(string))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Access Denied", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Unknown Error", typeof(string))]
        [Route("dimensions/countries")]
        [HttpPost]
        public IHttpActionResult CreateAccount([FromBody]CountryDimension country)
        {
            ValidateModel();

            try
            {
                _dimensionBLL.CreateCountryDimension(country.GetDTO());

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
        /// Updates an existing country dimension
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        [SwaggerOperation("Update Country Dimension", Tags = new[] { "Country Dimensions" })]
        [SwaggerResponse(HttpStatusCode.OK, "Success", typeof(string))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid Request Format", typeof(string))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Access Denied", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Unknown Error", typeof(string))]
        [Route("dimensions/countries")]
        [HttpPut]
        public IHttpActionResult Put([FromBody]CountryDimension country)
        {
            ValidateModel();

            try
            {
                _dimensionBLL.UpdateCountryDimension(country.GetDTO());

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
        /// Deletes a country dimension
        /// </summary>
        /// <param name="id">Pass in a valid country dimension ID to delete it</param>
        /// <returns></returns>
        [SwaggerOperation("Delete Account Dimension", Tags = new[] { "Country Dimensions" })]
        [SwaggerResponse(HttpStatusCode.OK, "Success", typeof(string))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid Request Format", typeof(string))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Access Denied", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Unknown Error", typeof(string))]
        [Route("dimensions/countries")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            ValidateModel();

            try
            {
                _dimensionBLL.DeleteCountryDimension(id);
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
