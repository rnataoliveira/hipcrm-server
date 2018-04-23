using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace web.Models.ClientsViewModels
{
    public class ClientSearchResult
    {
        public string Name { get; set; }
        public string Note { get; set; }
        public string DocumentNumber { get; set; }
        public string ContractNumber { get; set; }

    }
}
