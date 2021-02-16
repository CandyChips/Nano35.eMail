using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.email.artifacts;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.eMail.Processor.Requests.SendConfirmationUsersPassword;
using Nano35.eMail.Processor.Services;

namespace Nano35.eMail.Processor.Consumers
{
    public class SendConfirmationUsersPasswordConsumer : 
        IConsumer<ISendConfirmationUsersPasswordRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public SendConfirmationUsersPasswordConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(ConsumeContext<ISendConfirmationUsersPasswordRequestContract> context)
        {
            // Setup configuration of pipeline
            var logger = (ILogger<LoggedSendConfirmationUsersPasswordRequest>) _services.GetService(typeof(ILogger<LoggedSendConfirmationUsersPasswordRequest>));
            var mailService = (IMailService) _services.GetService(typeof(IMailService));
            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result = 
                await new LoggedSendConfirmationUsersPasswordRequest(logger,  
                    new SendConfirmationUsersPasswordRequest(mailService)
                ).Ask(message, context.CancellationToken);
            
            // Check response of create client request
            switch (result)
            {
                case ISendConfirmationUsersPasswordSuccessResultContract:
                    await context.RespondAsync<ISendConfirmationUsersPasswordSuccessResultContract>(result);
                    break;
                case ISendConfirmationUsersPasswordErrorResultContract:
                    await context.RespondAsync<ISendConfirmationUsersPasswordErrorResultContract>(result);
                    break;
            }
        }
    }
}