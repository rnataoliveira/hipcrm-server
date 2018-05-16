using System;
using System.Threading.Tasks;
using MediatR;
using Google.Apis.Services;
using Google.Apis.Calendar.v3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;

namespace server.Features.SalesPipelines
{
    public class CreateSaleCalendar
    {
        public class Command : IRequest
        {
            public Guid SaleId { get; set; }
        }

        public class Handler : AsyncRequestHandler<Command>
        {
            protected override Task Handle(Command request)
            {
                // var flow = new GoogleAuthorizationCodeFlow(
                // new GoogleAuthorizationCodeFlow.Initializer
                // {
                //     ClientSecrets = new ClientSecrets
                //     {
                //         ClientId = "",
                //         ClientSecret = ""
                //     },
                //     Scopes = new[] { CalendarService.Scope.Calendar }
                // });

                // var token = new TokenResponse()
                // {
                //     AccessToken = "",
                //     IdToken = "",
                //     ExpiresInSeconds = 3000
                // };

                // var credential = new UserCredential(flow, "renatabels", token);
                // var initializer = new BaseClientService.Initializer
                // {
                //     ApplicationName = "Corretora Lopes",
                //     HttpClientInitializer = credential
                // };
                // var calendarService = new CalendarService(initializer);

                return Task.FromResult(0);
            }
        }
    }
}