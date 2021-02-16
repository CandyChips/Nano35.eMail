using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.email.artifacts;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.eMail.Processor.Requests.SendConfirmationUsersPhone;
using Nano35.eMail.Processor.Services;

namespace Nano35.eMail.Processor.Consumers
{
    public class SendConfirmationUsersPhoneConsumer : 
        IConsumer<ISendConfirmationUsersPhoneRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public SendConfirmationUsersPhoneConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(ConsumeContext<ISendConfirmationUsersPhoneRequestContract> context)
        {
            // Setup configuration of pipeline
            var logger = (ILogger<LoggedSendConfirmationUsersPhoneRequest>) _services.GetService(typeof(ILogger<LoggedSendConfirmationUsersPhoneRequest>));
            var mailService = (IMailService) _services.GetService(typeof(IMailService));
            
            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result = 
                await new LoggedSendConfirmationUsersPhoneRequest(logger,  
                    new SendConfirmationUsersPhoneRequest(mailService)
                ).Ask(message, context.CancellationToken);
            
            // Check response of create client request
            switch (result)
            {
                case ISendConfirmationUsersPhoneSuccessResultContract:
                    await context.RespondAsync<ISendConfirmationUsersPhoneSuccessResultContract>(result);
                    break;
                case ISendConfirmationUsersPhoneErrorResultContract:
                    await context.RespondAsync<ISendConfirmationUsersPhoneErrorResultContract>(result);
                    break;
            }
        }
    }
}