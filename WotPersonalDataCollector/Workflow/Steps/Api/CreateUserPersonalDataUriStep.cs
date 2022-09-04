using System.Threading.Tasks;
using WotPersonalDataCollector.Api;

namespace WotPersonalDataCollector.Workflow.Steps.Api
{
    internal class CreateUserPersonalDataUriStep: BaseStep
    {
        private readonly IApiUriFactory _apiUriFactory;

        public CreateUserPersonalDataUriStep(IApiUriFactory apiUriFactory)
        {
            _apiUriFactory = apiUriFactory;
        }
        public override async Task ExecuteInner(WorkflowContext context)
        {
            context.UserPersonalDataApiUrlWithParameters =
                _apiUriFactory.Create(context.UserPersonalDataApiUrl, context.UserPersonalDataRequestObject);
        }
    }
}
