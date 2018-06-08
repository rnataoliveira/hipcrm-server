using System;
using System.ComponentModel.DataAnnotations;

namespace server.Models
{
    public class Customer
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public CustomerStatus Status { get; set; } = CustomerStatus.Prospect;

        [Required]
        public PersonalData PersonalData { get; set; }

        public string Notes { get; set; }

        public string Type => PersonalData.GetType().Name.ToString();
    }

    public enum CustomerStatus 
    {
        Prospect,
        Contract
    }
}