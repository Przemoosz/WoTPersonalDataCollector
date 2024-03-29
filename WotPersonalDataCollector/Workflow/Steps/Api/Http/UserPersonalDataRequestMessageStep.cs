﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WotPersonalDataCollector.Api.Http;

namespace WotPersonalDataCollector.Workflow.Steps.Api.Http
{
    internal class UserPersonalDataRequestMessageStep: BaseStep
    {
        private readonly IUserRequestMessageFactory _userRequestMessageFactory;
        private bool _createdRequestMessage = true;

        public UserPersonalDataRequestMessageStep(IUserRequestMessageFactory userRequestMessageFactory)
        {
            _userRequestMessageFactory = userRequestMessageFactory;
        }
        public override Task ExecuteInner(WorkflowContext context)
        {
            try
            {
                context.UserPersonalDataRequestMessage = _userRequestMessageFactory.Create(context.UserPersonalDataApiUrlWithParameters);
                return Task.CompletedTask;
            }
            catch (Exception exception)
            {
                context.Logger.LogError(
                    $"Unexpected error occurred when creating http requestMessage. Message: {exception.Message}\n At: {exception.StackTrace}");
                _createdRequestMessage = false;
                context.UnexpectedException = true;
                return Task.CompletedTask;
            }
        }

        public override bool SuccessfulStatus() => _createdRequestMessage;
    }
}
