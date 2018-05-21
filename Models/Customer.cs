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

    public PhoneNumber Phone { get; set; }

    public PhoneNumber CellPhone { get; set; }
  }

  public class PhoneNumber
  {
    public string AreaCode { get; set; }

    public string Number { get; set; }
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

    [Required]
    public PersonalData PersonalData { get; set; }

    public Guid PersonalDataId { get; set; }

    public string Notes { get; set; }
  }
}