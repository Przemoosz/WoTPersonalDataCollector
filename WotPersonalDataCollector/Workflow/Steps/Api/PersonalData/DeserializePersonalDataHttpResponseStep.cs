using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WotPersonalDataCollector.Api.PersonalData;
using WotPersonalDataCollector.Exceptions;

namespace WotPersonalDataCollector.Workflow.Steps.Api.PersonalData
{
    internal sealed class DeserializePersonalDataHttpResponseStep: BaseStep
    {
        private readonly IDeserializePersonalDataHttpResponse _deserializePersonalDataHttpResponse;

        public DeserializePersonalDataHttpResponseStep(IDeserializePersonalDataHttpResponse deserializePersonalDataHttpResponse)
        {
            _deserializePersonalDataHttpResponse = deserializePersonalDataHttpResponse;
        }
        private bool _deserializedSuccessful = true;

        public override async Task ExecuteInner(WorkflowContext context)
        {
            try
            {
                context.AccountDto = await _deserializePersonalDataHttpResponse.Deserialize(context.UserPersonalDataResponseMessage, context.ContractResolver);
            }
            catch (DeserializeJsonException exception)
            {
                context.Logger.LogError(
                    $"Error occurred during deserializing personal data received from WOT API, aborting further processing - {exception}");
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
