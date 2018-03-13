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
    public class RegionDimensionsController : ControllerExtension
    {
        private DimensionsBLL _dimensionBLL;

        /// <summary>
        /// Create a new instance
        /// </summary>
        public RegionDimensionsController()
        {
            _dimensionBLL = new DimensionsBLL(Global.GlobalVariables.ConnectionString);
        }

        /// <summary>
        /// Gets a list of region dimensions
        /// </summary>
        /// <param name="Page">The page you're requesting</param>
        /// <param name="PageSize">Number of items requested per page</param> \
        /// <param name="Region">A like query on the Region [Optional]</param>
        /// <returns>List of Account Dimension objects</returns>
        [SwaggerOperation("Get Region Dimensions", Tags = new[] { "Region Dimensions" })]
        [SwaggerResponse(HttpStatusCode.OK, "Success", typeof(RegionDimensionList))]
        [SwaggerResponse(HttpStatusCode.NoContent, "No Content", typeof(string))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid Request Format", typeof(string))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Access Denied", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Unknown Error", typeof(string))]
        [Route("dimensions/regions")]
        [HttpGet]
        public IHttpActionResult Get(int Page, int PageSize, string Region = "")
        {
            ValidateModel();

            try
            {
                RegionListDTO list = _dimensionBLL.GetRegionDimensions(Page, PageSize, Region);
                
                RegionDimensionList regionDimensions = new RegionDimensionList();

                foreach (RegionDTO c in list.Items)
                {
                    regionDimensions.Items.Add((RegionDimension)c);
                }

                if (regionDimensions.Items.Count > 0)
                {
                    regionDimensions.PageNumber = list.PageNumber;
                    regionDimensions.PageSize = list.PageSize;
                    regionDimensions.RecordCount = list.RecordCount;
                    regionDimensions.TotalPages = list.TotalPages;

                    return Ok(regionDimensions);
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
        /// Gets a single region dimension
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [SwaggerOperation("Get Region Dimension", Tags = new[] { "Region Dimensions" })]
        [SwaggerResponse(HttpStatusCode.OK, "Success", typeof(RegionDimension))]
        [SwaggerResponse(HttpStatusCode.NoContent, "No Content", typeof(string))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid Request Format", typeof(string))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Access Denied", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Unknown Error", typeof(string))]
        [Route("dimensions/regions/{id}")]
        [HttpGet]
        public IHttpActionResult GetSingle(int id)
        {
            ValidateModel();

            try
            {
                RegionDimension regionDimension = (RegionDimension)_dimensionBLL.GetRegionDimension(id);

                if (regionDimension != null && regionDimension.ID != 0)
                {
                    return Ok(regionDimension);
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
        /// Creates a new region dimension
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        [SwaggerOperation("Create Account Dimension", Tags = new[] { "Region Dimensions" })]
        [SwaggerResponse(HttpStatusCode.OK, "Success", typeof(string))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid Request Format", typeof(string))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Access Denied", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Unknown Error", typeof(string))]
        [Route("dimensions/regions")]
        [HttpPost]
        public IHttpActionResult CreateAccount([FromBody]RegionDimension region)
        {
            ValidateModel();

            try
            {
                _dimensionBLL.CreateRegionDimension(region.GetDTO());

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
        /// Updates an existing region dimension
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        [SwaggerOperation("Update Region Dimension", Tags = new[] { "Region Dimensions" })]
        [SwaggerResponse(HttpStatusCode.OK, "Success", typeof(string))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid Request Format", typeof(string))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Access Denied", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Unknown Error", typeof(string))]
        [Route("dimensions/regions")]
        [HttpPut]
        public IHttpActionResult Put([FromBody]RegionDimension region)
        {
            ValidateModel();

            try
            {
                _dimensionBLL.UpdateRegionDimension(region.GetDTO());

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
        /// Deletes a region dimension
        /// </summary>
        /// <param name="id">Pass in a valid account dimension ID to delete it</param>
        /// <returns></returns>
        [SwaggerOperation("Delete Region Dimension", Tags = new[] { "Region Dimensions" })]
        [SwaggerResponse(HttpStatusCode.OK, "Success", typeof(string))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid Request Format", typeof(string))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Access Denied", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Unknown Error", typeof(string))]
        [Route("dimensions/regions")]
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
