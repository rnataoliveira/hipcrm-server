using System;
using System.ComponentModel.DataAnnotations;

namespace server.Models
{
    public class PhoneNumber
    {
        public string AreaCode { get; set; }

        public string Number { get; set; }
    }
}