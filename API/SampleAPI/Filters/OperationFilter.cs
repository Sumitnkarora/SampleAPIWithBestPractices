using System.Collections.Generic;
using Swashbuckle.Swagger;
using System.Web.Http.Description;

namespace SampleAPI.Filters
{
    /// <summary>
    /// Applies the same header parameter to every operation
    /// </summary>
    public class HeaderKeyParameterOperationFilter : IOperationFilter
    {
        /// <summary>
        /// Creates a header parameter to each operation
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="schemaRegistry"></param>
        /// <param name="apiDescription"></param>
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.parameters == null) operation.parameters = new List<Parameter>();

            operation.parameters.Add(new Parameter
            {
                name = "api_key",
                @in = "header",
                description = "API key",
                required = true,
                type = "string",
#if DEBUG // In debug mode, set the default api key to the valid API key specified in the web.config
                @default = Global.GlobalVariables.APIKey
#endif          
            });
        }
    }



}