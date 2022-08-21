using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WotPersonalDataCollector.Api.User;
using WotPersonalDataCollector.Exceptions;

namespace WotPersonalDataCollector.Workflow.Steps.Api.User
{
    internal class DeserializeUserIdHttpResponseStep: BaseStep
    {
        private readonly IDeserializeUserIdHttpResponse _deserializeUserIdHttpResponse;
        private bool _deserializedSuccessful;

        public DeserializeUserIdHttpResponseStep(IDeserializeUserIdHttpResponse deserializeUserIdHttpResponse)
        {
            _deserializeUserIdHttpResponse = deserializeUserIdHttpResponse;
        }
        public override async Task ExecuteInner(WorkflowContext context)
        {
            try
            {
                context.UserIdData = await _deserializeUserIdHttpResponse.Deserialize(context.UserIdResponseMessage);
            }
            catch (DeserializeJsonException exception)
            {
                context.Logger.LogError(
                    $"Error occurred during deserializing data received from WOT API, aborting further processing - {exception}");
                _deserializedSuccessful = false;
            }
            catch (WotApiResponseException exception)
            {
                context.Logger.LogError(
                    $"WOT API returned error message inside 200 OK response, check if input data are correct, aborting further processing - {exception}");
                _deserializedSuccessful = false;
            }
            catch (MoreThanOneUserException exception)
            {
                context.Logger.LogError(
                    $"WOT API returned more than one user with this nickname, provide userId by yourself in local variables - {exception}");
                _deserializedSuccessful = false;
            }
            catch (Exception exception)
            {
                context.Logger.LogError(
                    $"Unexpected error occurred during connecting with WOT API. Message: {exception.Message}\n At: {exception.StackTrace} ");
                _deserializedSuccessful = false;
                context.UnexpectedException = true;
            }
        }

        public override bool SuccessfulStatus() => _deserializedSuccessful;
    }
}
