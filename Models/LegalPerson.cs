using System;

namespace web.Models
{
    public class LegalPerson : Client
    {
        public string Dealer { get; set;}
        public string BrokersDocumentNumber { get; set; }        
        public string BrokersName { get; set;}
        public string BrokersPhone { get; set;}
        public string BrokersEmail { get; set;}
        public string CompanyName { get; set;}
        public string CompanyRegistration { get; set;}
        public string StateRegistration { get; set;}
        public string CompanyContact { get; set;}
        public string ReferencePoint { get; set;}
        public string CompanyEmail { get; set;}
        public string NumberOfBeneficiaries { get; set;}
        public bool Modality { get; set; }

    }
}