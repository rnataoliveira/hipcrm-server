using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace web.ViewComponents
{   
    // https://docs.microsoft.com/en-us/aspnet/core/mvc/views/view-components
    [ViewComponent]
    public class MainMenu : ViewComponent
    {
        private readonly IList<MenuItem> Items = new List<MenuItem> {
            new MenuItem("Realizar Cadastro"),
            new MenuItem("Realizar Pesquisa", "/clients/search"),
            new MenuItem("Realizar Cotação"),
            new MenuItem("Realizar Agendamento"),
            new MenuItem("Planos"),
            new MenuItem("Financeiro")
        };

        public async Task<IViewComponentResult> InvokeAsync() 
        {
            return View(Items);
        }
        
        //Model que representa um item do componente menu principal
        public class MenuItem 
        {
            public MenuItem(string title, string url = "")
            {
                Url = url;
                Title = title;
            }

            public string Url { get; set; }
            
            public string Title { get; set; }
        }
    }
}

