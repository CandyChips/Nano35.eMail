using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.email.artifacts;

namespace Nano35.eMail.Processor.Requests.SendConfirmationUsersPassword
{
    public class LoggedSendConfirmationUsersPasswordRequest :
        IPipelineNode<
            ISendConfirmationUsersPasswordRequestContract,
            ISendConfirmationUsersPasswordResultContract>
    {
        private readonly ILogger<LoggedSendConfirmationUsersPasswordRequest> _logger;
        
        private readonly IPipelineNode<
            ISendConfirmationUsersPasswordRequestContract, 
            ISendConfirmationUsersPasswordResultContract> _nextNode;

        public LoggedSendConfirmationUsersPasswordRequest(
            ILogger<LoggedSendConfirmationUsersPasswordRequest> logger,
            IPipelineNode<
                ISendConfirmationUsersPasswordRequestContract, 
                ISendConfirmationUsersPasswordResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<ISendConfirmationUsersPasswordResultContract> Ask(
            ISendConfirmationUsersPasswordRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"SendConfirmationUsersPhoneLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"SendConfirmationUsersPhoneLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}