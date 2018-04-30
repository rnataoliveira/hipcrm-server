using System;

namespace server.Models
{
    public abstract class Person
    {
        public Guid Id { get; set; }
        public Address Address { get; set; }
    }

    public class LegalPerson : Person
    {
        public string CompanyName { get; set; }
        public string CompanyRegistration { get; set; }
        public string StateRegistration { get; set; }
    }

    public class PhysicalPerson : Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string DocumentNumber { get; set; }
        public string GeneralRegistration { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Sex { get; set; }
        public string MaritalState { get; set; }
    }

    public class Address
    {
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

        public Person Person { get; set; }

        public string Notes { get; set; }
    }
}