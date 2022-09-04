﻿using System;
using WotPersonalDataCollector.Api;
using WotPersonalDataCollector.Api.Http;
using WotPersonalDataCollector.Api.Http.RequestObjects;
using WotPersonalDataCollector.Api.Services;
using WotPersonalDataCollector.Api.User;
using WotPersonalDataCollector.Workflow.Steps;
using WotPersonalDataCollector.Workflow.Steps.Api;
using WotPersonalDataCollector.Workflow.Steps.Api.Http;
using WotPersonalDataCollector.Workflow.Steps.Api.Http.RequestObjects;
using WotPersonalDataCollector.Workflow.Steps.Api.Services;
using WotPersonalDataCollector.Workflow.Steps.Api.User;

namespace WotPersonalDataCollector.Workflow.Factory
{
    internal sealed class WorkflowStepsFactory: IWorkflowStepsFactory
    {
        private readonly IUserInfoRequestObjectFactory _userInfoRequestObjectFactory;
        private readonly IUserRequestMessageFactory _userRequestMessageFactory;
        private readonly IWotService _wotService;
        private readonly IDeserializeUserIdHttpResponse _deserializeUserIdHttpResponse;
        private readonly IApiUriFactory _apiUriFactory;
        private readonly IUserPersonalDataRequestObjectFactory _userPersonalDataRequestObjectFactory;

        public WorkflowStepsFactory(IUserInfoRequestObjectFactory userInfoRequestObjectFactory,
            IUserRequestMessageFactory userRequestMessageFactory, IWotService wotService,
            IDeserializeUserIdHttpResponse deserializeUserIdHttpResponse,
            IApiUriFactory apiUriFactory,
            IUserPersonalDataRequestObjectFactory userPersonalDataRequestObjectFactory)
        {
            _userInfoRequestObjectFactory = userInfoRequestObjectFactory;
            _userRequestMessageFactory = userRequestMessageFactory;
            _wotService = wotService;
            _deserializeUserIdHttpResponse = deserializeUserIdHttpResponse;
            _apiUriFactory = apiUriFactory;
            _userPersonalDataRequestObjectFactory = userPersonalDataRequestObjectFactory;
        }
        
        public BaseStep CreateUserInfoRequestObject()
        {
            return new CreateUserInfoRequestObjectStep(_userInfoRequestObjectFactory);
        }

        public BaseStep CreateUserInfoHttpRequestMessage()
        {
            return new UserInfoRequestMessageStep(_userRequestMessageFactory);
        }

        public BaseStep CreateSendRequestForUserId()
        {
            return new SendRequestForUserIdStep(_wotService);
        }

        public BaseStep CreateDeserializeUserIdResponseMessage()
        {
            return new DeserializeUserIdHttpResponseStep(_deserializeUserIdHttpResponse);
        }

        public BaseStep CreateUserPersonalDataRequestObject()
        {
            return new CreateUserPersonalDataRequestObjectStep(_userPersonalDataRequestObjectFactory);
        }

        public BaseStep CreateUserPersonalDataHttpRequestMessage()
        {
            return new UserPersonalDataRequestMessageStep(_userRequestMessageFactory);
        }

        public BaseStep CreateUserInfoApiUri()
        {
            return new CreateUserInfoApiUriStep(_apiUriFactory);
        }

        public BaseStep CreateUserPersonalDataApiUri()
        {
            return new CreateUserPersonalDataUriStep(_apiUriFactory);
        }

        public BaseStep CreateSendRequestForUserPersonalDataStep()
        {
            return new SendRequestForUserPersonalDataStep(_wotService);
        }
    }
}
