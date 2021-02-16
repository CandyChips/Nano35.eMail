using System;
using Nano35.Contracts.email.artifacts;

namespace Nano35.eMail.Processor.Configuraions
{
    public class MailRequest : 
        ISendUserConfirmationRequestContract
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string UniqueToken { get; set; }
    }
}