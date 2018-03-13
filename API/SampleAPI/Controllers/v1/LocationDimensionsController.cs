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
    public class LocationDimensionsController : ControllerExtension
    {
        private DimensionsBLL _dimensionBLL;

        /// <summary>
        /// Create a new instance
        /// </summary>
        public LocationDimensionsController()
        {
            _dimensionBLL = new DimensionsBLL(Global.GlobalVariables.ConnectionString);
        }




        /// <summary>
        /// Gets a list of location dimensions
        /// </summary>
        /// <param name="Page">The page you're requesting</param>
        /// <param name="PageSize">Number of items requested per page</param>
        /// <returns>List of Location dimension objects</returns>
        [SwaggerOperation("Get Location Dimensions", Tags = new[] { "Location Dimensions" })]
        [SwaggerResponse(HttpStatusCode.OK, "Success", typeof(LocationDimensionList))]
        [SwaggerResponse(HttpStatusCode.NoContent, "No Content", typeof(string))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid Request Format", typeof(string))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Access Denied", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Unknown Error", typeof(string))]
        [Route("dimensions/locations")]
        [HttpGet]
        public IHttpActionResult Get(int Page, int PageSize)
        {
            ValidateModel();

            try
            {
                LocationListDTO list = _dimensionBLL.GetLocationDimensions(Page, PageSize);

                LocationDimensionList locationDimensions = new LocationDimensionList();

                foreach (LocationDTO l in list.Items)
                {
                    locationDimensions.Items.Add((LocationDimension)l);
                }

                if (locationDimensions.Items.Count > 0)
                {
                    locationDimensions.PageNumber = list.PageNumber;
                    locationDimensions.PageSize = list.PageSize;
                    locationDimensions.RecordCount = list.RecordCount;
                    locationDimensions.TotalPages = list.TotalPages;

                    return Ok(locationDimensions);
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
        /// Gets a single location dimension
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [SwaggerOperation("Get Location Dimension", Tags = new[] { "Location Dimensions" })]
        [SwaggerResponse(HttpStatusCode.OK, "Success", typeof(LocationDimension))]
        [SwaggerResponse(HttpStatusCode.NoContent, "No Content", typeof(string))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid Request Format", typeof(string))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Access Denied", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Unknown Error", typeof(string))]
        [Route("dimensions/locations/{id}")]
        [HttpGet]
        public IHttpActionResult GetSingle(int id)
        {
            ValidateModel();

            try
            {
                LocationDimension locationDimension = (LocationDimension)_dimensionBLL.GetLocationDimension(id);

                if (locationDimension != null && locationDimension.ID != 0)
                {
                    return Ok(locationDimension);
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
        /// Creates a new location dimension
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        [SwaggerOperation("Create Location Dimension", Tags = new[] { "Location Dimensions" })]
        [SwaggerResponse(HttpStatusCode.OK, "Success", typeof(string))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid Request Format", typeof(string))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Access Denied", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Unknown Error", typeof(string))]
        [Route("dimensions/locations")]
        [HttpPost]
        public IHttpActionResult CreateLocation([FromBody]LocationDimension location)
        {
            ValidateModel();

            try
            {
                _dimensionBLL.CreateLocationDimension(location.GetDTO());

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
        /// Updates an existing location dimension
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        [SwaggerOperation("Update Location Dimension", Tags = new[] { "Location Dimensions" })]
        [SwaggerResponse(HttpStatusCode.OK, "Success", typeof(string))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid Request Format", typeof(string))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Access Denied", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Unknown Error", typeof(string))]
        [Route("dimensions/location")]
        [HttpPut]
        public IHttpActionResult Put([FromBody]LocationDimension location)
        {
            ValidateModel();

            try
            {
                _dimensionBLL.UpdateLocationDimension(location.GetDTO());

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
        /// Deletes a location dimension
        /// </summary>
        /// <param name="id">Pass in a valid account dimension ID to delete it</param>
        /// <returns></returns>
        [SwaggerOperation("Delete Location Dimension", Tags = new[] { "Location Dimensions" })]
        [SwaggerResponse(HttpStatusCode.OK, "Success", typeof(string))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid Request Format", typeof(string))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Access Denied", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Unknown Error", typeof(string))]
        [Route("dimensions/locations")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            ValidateModel();

            try
            {
                _dimensionBLL.DeleteLocationDimension(id);
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
