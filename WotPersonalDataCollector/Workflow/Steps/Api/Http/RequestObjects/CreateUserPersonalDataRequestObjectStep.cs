﻿using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WotPersonalDataCollector.Api.Http.RequestObjects;

namespace WotPersonalDataCollector.Workflow.Steps.Api.Http.RequestObjects
{
    internal class CreateUserPersonalDataRequestObjectStep : BaseStep
    {
        private readonly IUserPersonalDataRequestObjectFactory _userPersonalDataRequestObjectFactory;
        private bool _createdUserPersonalDataRequestObject = true;

        public CreateUserPersonalDataRequestObjectStep(IUserPersonalDataRequestObjectFactory userPersonalDataRequestObjectFactory)
        {
            _userPersonalDataRequestObjectFactory = userPersonalDataRequestObjectFactory;
        }
        public override Task ExecuteInner(WorkflowContext context)
        {
            try
            {
                context.UserPersonalDataRequestObject = _userPersonalDataRequestObjectFactory.Create(context.UserIdData);
                return Task.CompletedTask;
            }
            catch (Exception exception)
            {
                context.Logger.LogError(
                    $"Unexpected error occurred during creating userPersonalDataRequestObject. Message: {exception.Message}\n At: {exception.StackTrace} ");
                _createdUserPersonalDataRequestObject = false;
                context.UnexpectedException = true;
                return Task.CompletedTask;
            }
        }

        public override bool SuccessfulStatus() => _createdUserPersonalDataRequestObject;
    }
}
