
using NordCar.Server.Rest.Api.Dto.Common;
using NordCar.Shared.Domain.UniqueMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NordCar.Shared.Rest.ResponseValidation
{
    public class DefaultResponseFormatter : IResponseFormatter
    {
        //private readonly ILogger _logger = LoggerManager.CreateLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// An IResponseFormatter that performs some rudimentary unwrapping of known FailureContent structures to return only the error text.
        /// Once we get a little more control over return values on errors, this class can be a lot smarter.
        /// </summary>
        /// <remarks>In time, clients are meant to use their own implementation of IResponseFormatter with localization etc. instead of this</remarks>
        /// <param name="response"></param>
        /// <returns>Human readable parts of the response's FailureContent</returns>
        public string FormatResponse(ResponseSimple response)
        {
            if (response.IsSuccess)
                return string.Empty;

            try
            {
                //ErrorResponse is for errors returned 
                var errorResponse = response.FailureContent.TryParseJson<ErrorResponse>();
                //if (errorResponse?.Errors != null && errorResponse.Errors.Any())
                //{
                //    var formattedResponse = errorResponse.Errors.Select(FormatSingleFailure).Aggregate((cu, nx) => $"{cu}, {nx}");
                //    return formattedResponse;
                //}
            }
            catch (Exception e)
            {
                // Suppress exceptions - if we somehow fail formatting we just fallback to the raw value
                //_logger.LogWarning("FormatResponse failed", e);
            }

            //Fallback, just return it raw
            return response.FailureContent;
        }

        private static string FormatSingleFailure(UniqueMessage message)
        {
            if (message == null) throw new ArgumentNullException(message.ToString());

            var errorMessage = message.GetFormatedText(); //TODO TTA add localizable error message formater that lookups the Id and calls string.Format with returned string and message params

            return errorMessage;
        }
    }
}
