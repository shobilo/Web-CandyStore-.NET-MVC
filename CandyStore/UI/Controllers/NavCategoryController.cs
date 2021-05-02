using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.ViewModels;

namespace UI.Controllers
{
    public class NavCategoryController : Controller
    {
        private ICandyRepository repository;
        public NavCategoryController(ICandyRepository rep)
        {
            repository = rep;
        }
        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;

            IEnumerable<string> categories = repository.Candies
                .Select(candy => candy.Category)
                .Distinct()
                .OrderBy(candy => candy);

            return PartialView(categories);
        }
    }
}