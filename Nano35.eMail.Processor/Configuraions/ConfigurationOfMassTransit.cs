using System;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Contracts;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.eMail.Processor.Consumers;

namespace Nano35.eMail.Processor.Configuraions
{
    public class MassTransitConfiguration : 
        IConfigurationOfService
    {
        public void AddToServices(
            IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.UseHealthCheck(provider);
                    cfg.Host(new Uri($"{ContractBase.RabbitMqLocation}/"), h =>
                    {
                        h.Username(ContractBase.RabbitMqUsername);
                        h.Password(ContractBase.RabbitMqPassword);
                    });
                    cfg.ReceiveEndpoint("ISendConfirmationUsersPasswordRequestContract", e =>
                    {
                        e.Consumer<SendConfirmationUsersPasswordConsumer>(provider);
                    });
                    cfg.ReceiveEndpoint("ISendConfirmationUsersPhoneRequestContract", e =>
                    {
                        e.Consumer<SendConfirmationUsersPhoneConsumer>(provider);
                    });
                    cfg.ReceiveEndpoint("ISendUserConfirmationRequestContract", e =>
                    {
                        e.Consumer<SendUserConfirmationConsumer>(provider);
                    });
                }));
                x.AddConsumer<SendConfirmationUsersPasswordConsumer>();
                x.AddConsumer<SendConfirmationUsersPhoneConsumer>();
                x.AddConsumer<SendUserConfirmationConsumer>();
                
                x.AddRequestClient<IGetUserByIdRequestContract>(new Uri($"{ContractBase.RabbitMqLocation}/IGetUserByIdRequestContract"));
            });
            services.AddMassTransitHostedService();
        }
    }
}