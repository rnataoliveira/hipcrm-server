using System;

namespace web.Models
{
    public class PhysicalPerson : Client
    {
        public string Holder { get; set; }
        public string DocumentNumber { get; set; }
        public string GeneralRegistration { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Sex { get; set; }
        public string MaritalState { get; set; }
        public string CellPhone { get; set; }
        public string MothersName { get; set; }
        public string GeneralManager { get; set; }
        public string SusNumber { get; set; }
        public string PlanPhone { get; set; }
    }
}