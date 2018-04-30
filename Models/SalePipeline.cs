using System;
using System.ComponentModel.DataAnnotations;

namespace server.Models
{
    public class SalePipeline
    {
        public SalePipeline(Customer customer)
        {
            Customer = customer;
        }

        public SalePipeline() { }

        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Customer Customer { get; set; }
    }
}