using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.email.artifacts;

namespace Nano35.eMail.Processor.Requests.SendUserConfirmation
{
    public class LoggedSendUserConfirmationRequest :
        IPipelineNode<
            ISendUserConfirmationRequestContract,
            ISendUserConfirmationResultContract>
    {
        private readonly ILogger<LoggedSendUserConfirmationRequest> _logger;
        private readonly IPipelineNode<
            ISendUserConfirmationRequestContract, 
            ISendUserConfirmationResultContract> _nextNode;

        public LoggedSendUserConfirmationRequest(
            ILogger<LoggedSendUserConfirmationRequest> logger,
            IPipelineNode<
                ISendUserConfirmationRequestContract, 
                ISendUserConfirmationResultContract> nextNode)
        {
            _nextNode = nextNode;
            _logger = logger;
        }

        public async Task<ISendUserConfirmationResultContract> Ask(
            ISendUserConfirmationRequestContract input,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetAllRolesLogger starts on: {DateTime.Now}");
            var result = await _nextNode.Ask(input, cancellationToken);
            _logger.LogInformation($"GetAllRolesLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}