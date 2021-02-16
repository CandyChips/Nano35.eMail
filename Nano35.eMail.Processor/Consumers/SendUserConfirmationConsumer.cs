using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.email.artifacts;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.eMail.Processor.Requests.SendUserConfirmation;
using Nano35.eMail.Processor.Services;

namespace Nano35.eMail.Processor.Consumers
{
    public class SendUserConfirmationConsumer : 
        IConsumer<ISendUserConfirmationRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public SendUserConfirmationConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(ConsumeContext<ISendUserConfirmationRequestContract> context)
        {
            // Setup configuration of pipeline
            var logger = (ILogger<LoggedSendUserConfirmationRequest>) _services.GetService(typeof(ILogger<LoggedSendUserConfirmationRequest>));
            var mailService = (IMailService) _services.GetService(typeof(IMailService));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result = 
                await new LoggedSendUserConfirmationRequest(logger,  
                    new SendUserConfirmationRequest(mailService)
                ).Ask(message, context.CancellationToken);
            
            // Check response of create client request
            switch (result)
            {
                case ISendUserConfirmationSuccessResultContract:
                    await context.RespondAsync<ISendUserConfirmationSuccessResultContract>(result);
                    break;
                case ISendUserConfirmationErrorResultContract:
                    await context.RespondAsync<ISendUserConfirmationErrorResultContract>(result);
                    break;
            }
        }
    }
}