using System;
using System.ComponentModel.DataAnnotations;

namespace server.Models
{
    public abstract class PersonalData
    {
        public Guid Id { get; set; }
        public Address Address { get; set; }
    }

    public class LegalPerson : PersonalData
    {
        [Required]
        public string CompanyName { get; set; }

        [Required]
        public string CompanyRegistration { get; set; }
        public string StateRegistration { get; set; }
    }

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
        [Required]
        public string Sex { get; set; }
        [Required]
        public string MaritalState { get; set; }
    }

    public class Address
    {
        public int Id { get; set; }

        public PersonalData Person { get; set; }
        public Guid PersonId { get; set; }

        public string ZipCode { get; set; }

        public string Street { get; set; }

        public string Number { get; set; }

        public string Complement { get; set; }

        public string Neighborhood { get; set; }

        public string City { get; set; }

        public string State { get; set; }
    }

    public class Customer
    {
        public Guid Id { get; set; }

        [Required]
        public PersonalData PersonalData { get; set; }

        public string Notes { get; set; }
    }
}