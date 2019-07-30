using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        private readonly IRestaurantData data;
        private readonly IHtmlHelper htmlHelper;

        [BindProperty]
        public Restaurant Restaurant { get; set; }
        public IEnumerable<SelectListItem> Cuisines { get; set; }

        public EditModel(IRestaurantData data, IHtmlHelper htmlHelper)
        {
            this.data = data;
            this.htmlHelper = htmlHelper;
        }

        public IActionResult OnGet(int? id)
        {
            Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
            if (id.HasValue)
            {
                Restaurant = data.GetById(id.Value);
            }
            else
            {
                Restaurant = new Restaurant();
            }
            
            if (Restaurant == null) return RedirectToPage("../Shared/_NotFound");
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
                return Page();
            }

            if (Restaurant.Id > 0)
            {
                data.Update(Restaurant);
            }
            else
            {
                data.Add(Restaurant);
            }
            data.Commit();
            TempData["Message"] = "Restaurant saved!";
            return RedirectToPage("./Detail", new { id = Restaurant.Id });
        }
    }
}