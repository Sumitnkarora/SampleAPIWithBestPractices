using System;
using System.Linq;
using System.Web.Http.Filters;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace SampleAPI.Filters
{
    /// <summary>
    /// The security filter enables consistent API key validation across all API methods
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class SecurityFilter : ActionFilterAttribute, IActionFilter
    {
        /// <summary>
        /// Triggered when an action is executing
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            try
            {
                var headers = actionContext.Request.Headers;

                string apiKey = headers.GetValues("api_key").First();



                Guid key = Guid.Empty;

                if (!Guid.TryParse(apiKey, out key))
                {
                    actionContext.Response = actionContext.Request.CreateResponse(
                                            HttpStatusCode.Unauthorized,
                                            new { Error = "Access Denied" },
                                            actionContext.ControllerContext.Configuration.Formatters.JsonFormatter);

                    base.OnActionExecuting(actionContext);
                    return;
                }

                // Add Security Logic here, check hashed api keys here against hashed keys in a data store etc
                if (key != Global.GlobalVariables.APIKey)
                {
                    actionContext.Response = actionContext.Request.CreateResponse(
                        HttpStatusCode.Unauthorized,
                        new { Error = "Access Denied" },
                        actionContext.ControllerContext.Configuration.Formatters.JsonFormatter);

                    base.OnActionExecuting(actionContext);
                    return;
                }
            }
            catch (Exception)
            {
                actionContext.Response = actionContext.Request.CreateResponse(
                        HttpStatusCode.Unauthorized,
                        new { Error = "Access Denied" },
                        actionContext.ControllerContext.Configuration.Formatters.JsonFormatter);

                base.OnActionExecuting(actionContext);
                return;
            }

            base.OnActionExecuting(actionContext);
        }
    }
}