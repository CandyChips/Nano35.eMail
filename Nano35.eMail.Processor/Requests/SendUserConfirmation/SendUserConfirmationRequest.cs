using System;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.email.artifacts;
using Nano35.eMail.Processor.Services;

namespace Nano35.eMail.Processor.Requests.SendUserConfirmation
{
    public class SendUserConfirmationRequest :
        IPipelineNode<
            ISendUserConfirmationRequestContract, 
            ISendUserConfirmationResultContract>
    {
        private readonly IMailService _mailService;
        
        public SendUserConfirmationRequest(
            IMailService mailService)
        {
            _mailService = mailService;
        }
        
        private class SendUserConfirmationSuccessResultContract : 
            ISendUserConfirmationSuccessResultContract
        {
            
        }

        public async Task<ISendUserConfirmationResultContract> Ask(
            ISendUserConfirmationRequestContract request,
            CancellationToken cancellationToken)
        {
            var mail = new MailRequest()
            {
                ToEmail = request.Email,
                Attachments = null,
                Body = "123123"
            };
            
            await _mailService.SendEmailAsync(mail);
            
            return new SendUserConfirmationSuccessResultContract();
        }
    }
}