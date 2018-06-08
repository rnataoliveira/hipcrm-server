using System;
using System.ComponentModel.DataAnnotations;

namespace server.Models
{
    public abstract class PersonalData
    {
        public Guid Id { get; set; }
        public Address Address { get; set; }
    }
}