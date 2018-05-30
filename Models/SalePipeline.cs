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

        public string Code { get; set; }

        [Required]
        public Customer Customer { get; set; }

        public string CalendarId { get; set; }

        public string FolderId { get; set; }
    }
}