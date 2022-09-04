using System.Threading.Tasks;
using WotPersonalDataCollector.Api.Http.RequestObjects;

namespace WotPersonalDataCollector.Workflow.Steps.Api.Http.RequestObjects
{
    internal class CreateUserPersonalDataRequestObjectStep : BaseStep
    {
        private readonly IUserPersonalDataRequestObjectFactory _userPersonalDataRequestObjectFactory;

        public CreateUserPersonalDataRequestObjectStep(IUserPersonalDataRequestObjectFactory userPersonalDataRequestObjectFactory)
        {
            _userPersonalDataRequestObjectFactory = userPersonalDataRequestObjectFactory;
        }
        public override async Task ExecuteInner(WorkflowContext context)
        {
            context.PersonalDataRequestObject = _userPersonalDataRequestObjectFactory.Create(context.UserIdData);
        }
    }
}
