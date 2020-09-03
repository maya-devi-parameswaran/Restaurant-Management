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
        public readonly IRestaurantData restaurantData;
        [BindProperty]
        public Restaurant Restaurant{ get; set; }

        public readonly IEnumerable<SelectListItem> Cuisines;
        public string Mode { get; set; }
        public EditModel(IRestaurantData restaurantData, IHtmlHelper htmlHelper)
        {
            this.restaurantData = restaurantData;
            this.Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
        }
        public IActionResult OnGet(int? restaurantId)
        {
            if (restaurantId.HasValue)
            {
                Restaurant = restaurantData.GetRestaurantById(restaurantId);
                Mode = "Edit";
            }
               
            else
            {
                Restaurant = new Restaurant();
                Mode = "Create";
            }
               
           
            //if (Restaurant is null) return RedirectToPage("./NotFound");

            return Page();

        }

        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            if (Restaurant.Id > 0)
            {
                restaurantData.Update(Restaurant);
                TempData["Message"] = "Restaurant updated!";
            }
               
            else
            {
                restaurantData.Add(Restaurant);
                TempData["Message"] = "Restaurant added!";
            }
              

            
            restaurantData.Commit();
            return RedirectToPage("./Detail", new { restaurantId = Restaurant.Id });

           
        }
    }
}