using System;
using System.Threading.Tasks;
using MediatR;
using Google.Apis.Services;
using Google.Apis.Calendar.v3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using server.Facades.Google;
using server.Facades.Google.Models;
using server.Data;
using server.Models;
using Refit;
using Newtonsoft.Json.Linq;

namespace server.Features.SalesPipelines
{
    public class CreateSaleFolder
    {
        public class Command : IRequest
        {
            public Guid SaleId { get; set; }

            public string AccessToken { get; set; }
        }

        public class Handler : AsyncRequestHandler<Command>
        {
            readonly IDriveApi _driveApi;
            readonly ApplicationDbContext _context;

            public Handler(IDriveApi driveApi, ApplicationDbContext context)
            {
                _driveApi = driveApi;
                _context = context;
            }

            protected override async Task Handle(Command request)
            {
                SalePipeline sale = await _context.SalesPipelines.FindAsync(request.SaleId);

                var folderData = new File
                {
                    Name = sale.Code,
                    MimeType = File.FolderMimeType
                };

                var folder = await _driveApi.Create(folderData, request.AccessToken);
                sale.FolderId = folder.Id;

                _context.Update(sale);
                await _context.SaveChangesAsync();
            }
        }
    }
}