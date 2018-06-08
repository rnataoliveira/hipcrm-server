using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace server.Models
{
    public class Agreement
    {
        public Guid Id { get; set; }

        public SalePipeline Sale { get; set; }

        public string Number { get; set; }

        public string Notes { get; set; }

        public PaymentInfo Payment { get; set; }
    }

    public class Beneficiary
    {
        public LegalPersonAgreement Agreement { get; set; }

        public int Id { get; set; }

        public string Number { get; set; }

        public string Plan { get; set; }
    }

    public enum Modality
    {
        WithoutParticipation = 0,
        WithParticipation = 1,
    }

    public class DentalCare
    {
        public bool Has => string.IsNullOrEmpty(Plan);

        public string Plan { get; set; }
    }

    public class PaymentInfo
    {
        public decimal EntranceFee { get; set; }

        public decimal TotalValue { get; set; }

        public int InstallmentsCount { get; set; }

        public decimal InstallmentValue => (TotalValue - EntranceFee) / InstallmentsCount;

        public decimal Comission { get; set; }
    }

    public class LegalPersonAgreement : Agreement
    {
        public PhoneNumber Phone { get; set; }

        public string Email { get; set; }

        public string Contact { get; set; }

        public Address MailingAddress { get; set; }

        public ICollection<Beneficiary> Beneficiaries { get; set; } = new Collection<Beneficiary>();

        public Modality Modality { get; set; }

        public DentalCare DentalCare { get; set; }
    }
}