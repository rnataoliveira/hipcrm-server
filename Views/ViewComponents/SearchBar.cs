using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;

namespace web.ViewComponents
{   
    // https://docs.microsoft.com/en-us/aspnet/core/mvc/views/view-components
    [ViewComponent]
    public class SearchBar : ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync() 
        {
            return View();
        }
    }
}