using System;
using System.ComponentModel.DataAnnotations;

namespace web.Models
{
    public class Client
    {
        [Key]
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string Neighborhood { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string DDD { get; set; }
        public string HealthInsurancePlan { get; set;}
        public string ClientStatus { get; set; }
        public double EntryPrice { get; set; }
        public int NumberOfInstallments { get; set; }
        public string Notes { get; set; } 
        public string AttachFiles { get; set; }
        public string ContractNumber { get; set; }
    }
}