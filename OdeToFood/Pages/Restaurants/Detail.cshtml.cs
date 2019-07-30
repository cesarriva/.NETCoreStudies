using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class DetailModel : PageModel
    {
        private readonly IRestaurantData data;

        public Restaurant Restaurant { get; set; }

        [TempData]
        public string Message { get; set; }

        public DetailModel(IRestaurantData data)
        {
            this.data = data;
        }

        public IActionResult OnGet(int id)
        {
            Restaurant = data.GetById(id);
            if (Restaurant == null) return RedirectToPage("../Shared/_NotFound");
            return Page();
        }
    }
}