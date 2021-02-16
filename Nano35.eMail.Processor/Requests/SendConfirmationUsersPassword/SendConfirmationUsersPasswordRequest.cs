using System;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.email.artifacts;
using Nano35.eMail.Processor.Services;

namespace Nano35.eMail.Processor.Requests.SendConfirmationUsersPassword
{
    public class SendConfirmationUsersPasswordRequest :
        IPipelineNode<
            ISendConfirmationUsersPasswordRequestContract, 
            ISendConfirmationUsersPasswordResultContract>
    {
        private readonly IMailService _mailService;
        
        public SendConfirmationUsersPasswordRequest(
            IMailService mailService)
        {
            _mailService = mailService;
        }
        
        private class SendConfirmationUsersPasswordSuccessResultContract : 
            ISendConfirmationUsersPasswordSuccessResultContract
        {
            
        }

        public async Task<ISendConfirmationUsersPasswordResultContract> Ask(
            ISendConfirmationUsersPasswordRequestContract request,
            CancellationToken cancellationToken)
        {
            var mail = new MailRequest()
            {

            };
            
            await _mailService.SendEmailAsync(mail);
            
            return new SendConfirmationUsersPasswordSuccessResultContract();
        }
    }
}