using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.email.artifacts;
using Nano35.Contracts.Identity.Artifacts;

namespace Nano35.eMail.Processor.Requests.SendConfirmationUsersPhone
{
    public class LoggedSendConfirmationUsersPhoneRequest :
        IPipelineNode<
            ISendConfirmationUsersPhoneRequestContract,
            ISendConfirmationUsersPhoneResultContract>
    {
        private readonly ILogger<LoggedSendConfirmationUsersPhoneRequest> _logger;
        private readonly IPipelineNode<
            ISendConfirmationUsersPhoneRequestContract,
            ISendConfirmationUsersPhoneResultContract> _nextNode;

        public LoggedSendConfirmationUsersPhoneRequest(
            ILogger<LoggedSendConfirmationUsersPhoneRequest> logger,
            IPipelineNode<
                ISendConfirmationUsersPhoneRequestContract,
                ISendConfirmationUsersPhoneResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<ISendConfirmationUsersPhoneResultContract> Ask(
            ISendConfirmationUsersPhoneRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"SendConfirmationUsersPhone starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"SendConfirmationUsersPhone ends on: {DateTime.Now}");
            return result;
        }
    }
}