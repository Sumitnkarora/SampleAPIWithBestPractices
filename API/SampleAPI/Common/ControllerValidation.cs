using System.Web.Http;

namespace SampleAPI.Common
{
    /// <summary>
    /// The controller extension class contains commonly used properties and methods across many controllers
    /// </summary>
    public class ControllerExtension: ApiController
    {

        /// <summary>
        /// Checks to see if the Page Size is greater than the limit and throws a Request Entity too large exception
        /// NOTE: Extension methods must be protected to prevent swagger from generating anything
        /// </summary>
        /// <param name="PageSize"></param>
        protected void PageSizeCheck(int PageSize)
        {
            if (PageSize > Global.GlobalVariables.MaximumRecords)
            {
                System.Net.Http.HttpResponseMessage message = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.RequestEntityTooLarge);
                message.Content = new System.Net.Http.StringContent("Your request is too large. Please reduce your page size.");
                throw new HttpResponseException(message);
            }
        }


        /// <summary>
        /// Invalid Model throws an invalid model exception when the model is invalid with a bad request (401) status code
        /// NOTE: Extension methods must be protected to prevent swagger from generating anything
        /// </summary>
        protected void ValidateModel()
        {
            if (ModelState.IsValid == false)
            {
                System.Net.Http.HttpResponseMessage message = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                message.Content = new System.Net.Http.StringContent("The data submitted is invalid, please correct the model and try again.");
                throw new HttpResponseException(message);
            }
            else
            {
                return;
            }
        }
    }
}