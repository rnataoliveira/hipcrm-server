using System;
using System.ComponentModel.DataAnnotations;

namespace server.Models
{
    public class LegalPerson : PersonalData
    {
        [Required]
        public string CompanyName { get; set; }

        [Required]
        public string CompanyRegistration { get; set; }
        
        public string StateRegistration { get; set; }

        public PhoneNumber Phone { get; set; }

        public string Email { get; set; }
    }
}