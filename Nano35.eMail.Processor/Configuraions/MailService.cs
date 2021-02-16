using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Nano35.eMail.Processor.Configuraions
{
    public class MailServiceConfiguration
    {
        public string Mail { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }
}