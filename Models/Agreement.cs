using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace server.Models
{
    public class Agreement
    {
        public Guid Id { get; set; }

        public SalePipeline Sale { get; set; }

        public string Number { get; set; }

        public string Notes { get; set; }

        public PaymentInfo Payment { get; set; }

        public AgreementData PersonalData { get; set; }
    }

    public class Beneficiary
    {
        [JsonIgnore]
        public LegalPersonAgreement Agreement { get; set; }

        public int Id { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        public string Plan { get; set; }
    }

    public enum Modality
    {
        WithoutParticipation = 0,
        WithParticipation = 1,
    }

    public class DentalCare
    {
        public bool Has => !string.IsNullOrEmpty(Plan);

        [Required]
        public string Plan { get; set; }
    }

    public class PaymentInfo
    {
        [Required]
        [Range(1, int.MaxValue)]
        public decimal EntranceFee { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public decimal TotalValue { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int InstallmentsCount { get; set; }

        public decimal InstallmentValue => InstallmentsCount > 0 ? (TotalValue - EntranceFee) / InstallmentsCount : 0;

        public decimal Comission { get; set; } = 0;
    }

    public abstract class AgreementData
    {
        public Guid Id { get; set; }
    }

    public class LegalPersonAgreement : AgreementData
    {
        public PhoneNumber Phone { get; set; }

        public string Email { get; set; }

        public string Contact { get; set; }

        public Address MailingAddress { get; set; }

        public ICollection<Beneficiary> Beneficiaries { get; set; } = new Collection<Beneficiary>();

        public Modality Modality { get; set; }

        public DentalCare DentalCare { get; set; }
    }

    public class PhysicalPersonAgreement : AgreementData
    {
        public string Plan { get; set; }

        public ICollection<Dependent> Dependents { get; set; } = new Collection<Dependent>();
    }

    public class Dependent
    {
        [JsonIgnore]
        public PhysicalPersonAgreement Agreement { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public string DocumentNumber { get; set; }

        public string GeneralRegistration { get; set; }

        public DateTime BirthDate { get; set; }

        public string MothersName { get; set; }

        public string MaritalState { get; set; }

        public string Relationship { get; set; }

    }
}