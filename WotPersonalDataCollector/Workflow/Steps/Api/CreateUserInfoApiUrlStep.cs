using System.Threading.Tasks;
using WotPersonalDataCollector.Api;

namespace WotPersonalDataCollector.Workflow.Steps.Api
{
    internal class CreateUserInfoApiUrlStep: BaseStep
    {
        private readonly IApiUriFactory _apiUriFactory;

        public CreateUserInfoApiUrlStep(IApiUriFactory apiUriFactory)
        {
            _apiUriFactory = apiUriFactory;
        }
        public override async Task ExecuteInner(WorkflowContext context)
        {
            context.UserInfoApiUrlWithParameters =
                _apiUriFactory.Create(context.UserInfoApiUrl, context.UserInfoRequestObject);
        }
    }
}
