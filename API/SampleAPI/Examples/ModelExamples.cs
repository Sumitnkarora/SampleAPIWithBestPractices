using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleAPI.Examples
{
    /// <summary>
    /// Allows to display an input model example in swagger
    /// </summary> 
    [AttributeUsage(AttributeTargets.Method)]
    public class SwaggerInputExamplesAttribute : Attribute
    {
        /// <summary>
        /// ctor
        /// </summary>
        public SwaggerInputExamplesAttribute(Type responseType, Type exampleProviderType)
        {
            ResponseType = responseType;
            ExampleProviderType = exampleProviderType;
        }

        /// <summary>
        /// The type of response
        /// </summary>
        public Type ResponseType { get; private set; }

        /// <summary>
        /// The example provider type
        /// </summary>
        public Type ExampleProviderType { get; private set; }
    }

    /// <summary>
    /// Interface for defining an example model
    /// </summary>
    public interface ISwaggerModelExample
    {
        /// <summary>
        /// Returns the example model
        /// </summary>
        /// <returns></returns>
        object GetExample();
    }

    /// <summary>
    /// An example of the account dimension model for showing in swagger
    /// </summary>
    public class AccountDimensionExample : ISwaggerModelExample
    {
        /// <summary>
        /// Gets the account dimension example
        /// </summary>
        /// <returns></returns>
        public object GetExample()
        {
            return new Models.AccountDimension
            {
                AccountID = Guid.NewGuid(),
                AccountName = "Test Account"
            };
        }
    }

    /// <summary>
    /// An example of the country dimension model for showing in swagger
    /// </summary>
    public class CountryDimensionExample : ISwaggerModelExample
    {
        /// <summary>
        /// Gets the country dimension example
        /// </summary>
        /// <returns></returns>
        public object GetExample()
        {
            return new Models.CountryDimension
            {
                CountryCode = "TC",
                CountryName = "Test Country"
            };
        }
    }
}