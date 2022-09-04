using System.Threading.Tasks;
using WotPersonalDataCollector.Api;

namespace WotPersonalDataCollector.Workflow.Steps.Api
{
    internal class CreateUserInfoApiUriStep: BaseStep
    {
        private readonly IApiUriFactory _apiUriFactory;

        public CreateUserInfoApiUriStep(IApiUriFactory apiUriFactory)
        {
            _apiUriFactory = apiUriFactory;
        }
        public override async Task ExecuteInner(WorkflowContext context)
        {
            context.UserInfoApiUriWithParameters =
                _apiUriFactory.Create(context.UserInfoApiUrl, context.UserInfoRequestObject);
        }
    }
}
