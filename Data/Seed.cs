using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using web.Data;
using web.Models;

namespace web.Models 
{
    public static class Seed 
    {
        public static void SeedDatabase(this ApplicationDbContext context)
        {
            var clients = new List<PhysicalPerson>();

            var cli = new PhysicalPerson();
            cli.Id = Guid.NewGuid();
            cli.Address = "Rua das Amélias";
            cli.ZipCode = "04639221";
            cli.Neighborhood = "Vila Amália";
            cli.Number = "135";
            cli.Complement = "Condomínio Jardim das Perdizes";
            cli.State = "SP";
            cli.City = "São Paulo";
            cli.Email = "mario_andrade@gmail.com";
            cli.PhoneNumber = "11987430299";
            cli.DDD = "11";
            cli.HealthInsurancePlan = "Amil";
            cli.ClientStatus = "Ativo";
            cli.EntryPrice = 150.0;
            cli.NumberOfInstallments = 10;
            cli.Notes = "";
            cli.AttachFiles = "";
            cli.Holder = "Mario de Andrade Millano";
            cli.DocumentNumber = "00013465290";
            cli.GeneralRegistration = "1883000";
            cli.DateOfBirth = new DateTime(1990/09/30);
            cli.Sex = "M";
            cli.MaritalState = "Casado";
            cli.CellPhone = "";
            cli.MothersName = "Amália Millano";
            cli.GeneralManager = "";
            cli.SusNumber = "0002119889003111";
            cli.ContractNumber = "18345V";
            cli.PlanPhone = "1136557826";

            clients.Add(cli);

            clients.ForEach(c =>
            {
                if (context.Clients.Any(cl => cl.Id == c.Id))
                    context.Clients.Update(c);
                else
                    context.Clients.Add(c);
            });

            context.SaveChanges();
        }
    }
}