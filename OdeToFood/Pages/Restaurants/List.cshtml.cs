using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class ListModel : PageModel
    {
        private readonly IConfiguration config;
        private readonly IRestaurantData restaurant;
        [TempData]
        public string Message { get; set; }
        [BindProperty(SupportsGet=true)]
        public string SearchTerm { get; set; }
        public IEnumerable<Restaurant> Restaurants { get; set; }
        private readonly ILogger<ListModel> logger;

        public ListModel(IConfiguration config, IRestaurantData restaurant, ILogger<ListModel> logger)
        {
            this.config = config;
            this.restaurant = restaurant;
            this.logger = logger;
        }
        public void OnGet()
        {
            //Message = "Hello, World!";
            Message = config["Message"];
            logger.LogError("Executing List Model.");
            this.Restaurants = restaurant.GetRestaurantByName(SearchTerm);
        }
    }
}