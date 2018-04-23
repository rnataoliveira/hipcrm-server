using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web.Models;
using web.Models.ClientsViewModels;
using web.Data;

namespace web.Controllers
{
    [Route("clients")]
    public class ClientsController : Controller
    {
        readonly ApplicationDbContext _dbContext;
        public ClientsController(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        [Route("search")]
        public IActionResult Search(string q)
        {
            IEnumerable<PhysicalPerson> resultPhysicalPerson = _dbContext.PhysicalPerson
                                    .Where(p => 
                                        p.Holder.Contains(q) ||
                                        p.DocumentNumber == q ||
                                        p.Notes.Contains(q) || 
                                        p.ContractNumber == q);

            IEnumerable<LegalPerson> resultLegalPerson = _dbContext.LegalPerson
                                    .Where(lp => 
                                        lp.CompanyName.Contains(q) ||
                                        lp.CompanyRegistration == (q) ||
                                        lp.Notes.Contains(q));

            var result = new List<ClientSearchResult>();

            foreach (PhysicalPerson p in resultPhysicalPerson)
            {
                var clientSearch = new ClientSearchResult();
                clientSearch.ContractNumber = p.ContractNumber;
                clientSearch.DocumentNumber = p.DocumentNumber;
                clientSearch.Name = p.Holder;
                clientSearch.Note = p.Notes;
                result.Add(clientSearch);
            }

            foreach (LegalPerson p in resultLegalPerson)
            {
                var LegalPersonSearch = new ClientSearchResult();
                LegalPersonSearch.Name = p.CompanyName;
                LegalPersonSearch.DocumentNumber = p.CompanyRegistration;
                LegalPersonSearch.Note = p.Notes;
                LegalPersonSearch.ContractNumber = p.ContractNumber;
                result.Add(LegalPersonSearch);
            }
            
            return View(result.OrderBy(x => x.Name));
        }
    }
}
