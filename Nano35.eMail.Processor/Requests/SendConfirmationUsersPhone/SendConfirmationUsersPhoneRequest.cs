using System;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.email.artifacts;
using Nano35.eMail.Processor.Services;

namespace Nano35.eMail.Processor.Requests.SendConfirmationUsersPhone
{
    public class SendConfirmationUsersPhoneRequest :
        IPipelineNode<
            ISendConfirmationUsersPhoneRequestContract, 
            ISendConfirmationUsersPhoneResultContract>
    {
        private readonly IMailService _mailService;
        
        public SendConfirmationUsersPhoneRequest(
            IMailService mailService)
        {
            _mailService = mailService;
        }
        
        private class SendConfirmationUsersPhoneSuccessResultContract : 
            ISendConfirmationUsersPhoneSuccessResultContract
        {
            
        }

        public async Task<ISendConfirmationUsersPhoneResultContract> Ask(
            ISendConfirmationUsersPhoneRequestContract request,
            CancellationToken cancellationToken)
        {
            var mail = new MailRequest()
            {

            };
            
            await _mailService.SendEmailAsync(mail);
            
            return new SendConfirmationUsersPhoneSuccessResultContract();
        }
    }
}