﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WotPersonalDataCollector.Api.Http;

namespace WotPersonalDataCollector.Workflow.Steps.Api.Http
{
    internal class UserInfoRequestMessageStep: BaseStep
    {
        private readonly IUserRequestMessageFactory _userRequestMessageFactory;
        private bool _createdRequestMessage = true;

        public UserInfoRequestMessageStep(IUserRequestMessageFactory userRequestMessageFactory)
        {
            _userRequestMessageFactory = userRequestMessageFactory;
        }
        public override Task ExecuteInner(WorkflowContext context)
        {
            try
            {
                context.UserInfoRequestMessage = _userRequestMessageFactory.Create(context.UserInfoApiUriWithParameters);
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
