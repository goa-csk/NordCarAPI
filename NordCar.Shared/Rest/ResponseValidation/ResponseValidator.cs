using Ed.Shared.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NordCar.Shared.Rest.ResponseValidation
{
    public class ResponseValidator : IResponseValidator
    {
        private readonly IResponseFormatter _responseFormatter;
        //private static readonly ILogger Logger = LoggerManager.CreateLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public ResponseValidator(IResponseFormatter responseFormatter)
        {
            _responseFormatter = responseFormatter;
        }

        public async Task<TResponse> Invoke<TResponse>(IList<ValidationResponse> validationResponses, Func<Task<TResponse>> methodToInvoke)
            where TResponse : ResponseSimple, new()
        {
            TResponse response;

            if (Thread.CurrentThread.GetApartmentState() == ApartmentState.STA)
                throw new Exception(
                    "Attempted to Invoke REST Server method using GUI (STA) thread." + Environment.NewLine
                    + "ALWAYS use await Task.Run(async () => _appService.MyMethod()) in controller, so background thread is used or we will block entire GUI in case of Server lag.");

            try
            {
                response = await methodToInvoke();

                // We do not want to add validationResponse if call was successful as that is what the user expects, and it will be annoying with an Ok message every time we make a server call.

                if (!response.IsSuccess)
                {
                    var errorMessage = InvalidateValidationResponse(validationResponses, response);
                    //Logger.LogError(errorMessage);
                }
            }
            catch (Exception ex)
            {
                response = new TResponse { IsSuccess = false, Reason = "Error invoking server request", FailureContent = ex.ToString(), StatusCode = HttpStatusCode.ServiceUnavailable };
                //Logger.LogError(response.Reason, ex);
                InvalidateValidationResponse(validationResponses, response);
            }

            return response;
        }

        private string InvalidateValidationResponse<TResponse>(IList<ValidationResponse> validationResponses, TResponse response) where TResponse : ResponseSimple, new()
        {
            //TODO: JRJ: Once our UserMessageLogViewer has hyperlink with detail, we should change this line so we format the message and use detail for the real exception.
            var errorMessage = _responseFormatter.FormatResponse(response);
            validationResponses.Add(ValidationResponse.Invalidate(response.Reason, errorMessage));
            return errorMessage;
        }
    }
}
