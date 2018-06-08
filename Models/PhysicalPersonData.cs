using System;
using System.ComponentModel.DataAnnotations;

namespace server.Models
{
    public class PhysicalPerson : PersonalData
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string DocumentNumber { get; set; }

        public string GeneralRegistration { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        public int Age => DateTime.UtcNow.Year - BirthDate.Year;

        [Required]
        public string Sex { get; set; }

        [Required]
        public string MaritalState { get; set; }

        public PhoneNumber Phone { get; set; }

        public PhoneNumber CellPhone { get; set; }

        public string Email { get; set; }
    }
}