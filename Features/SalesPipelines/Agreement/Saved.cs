using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using server.Data;
using server.Models;
using server.Shared;

namespace server.Features.SalesPipelines.Agreement
{
    public class Saved : INotification
    {
        public Guid AgreementId { get; set; }

        public string AccessToken { get; set; }
    }
}