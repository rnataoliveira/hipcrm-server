using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;

namespace web.ViewComponents
{   
    // https://docs.microsoft.com/en-us/aspnet/core/mvc/views/view-components
    [ViewComponent]
    public class DayAppointments : ViewComponent
    {
        private readonly IList<Appointment> AllDayAppointments = new List<Appointment> {
            new Appointment("Fechar Contrato", "Alterar", "Excluir", new DateTime(2017, 11, 10, 10, 00, 00)),
            new Appointment("Fechar Contrato", "Alterar", "Excluir", new DateTime(2017, 11, 10, 11, 00, 00)),           
            new Appointment("Fechar Contrato", "Alterar", "Excluir", new DateTime(2017, 11, 10, 12, 00, 00))            
        };

        public async Task<IViewComponentResult> InvokeAsync() 
        {
            return View(AllDayAppointments);
        }
        
        //Model que representa um item do componente menu principal
        public class Appointment
        {
            public Appointment(string title, string updateButton, string deleteButton, DateTime appointmentTime)
            {
                Title = title;
                UpdateButton = updateButton;
                DeleteButton = deleteButton;
                AppointmentTime = appointmentTime;
            }

            public string Title { get; set; }

            public string UpdateButton { get; set; }

            public string DeleteButton { get; set; }

            public DateTime AppointmentTime { get; set; }
        }
    }
}