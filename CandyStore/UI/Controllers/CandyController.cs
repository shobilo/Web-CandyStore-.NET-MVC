using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.ViewModels;

namespace UI.Controllers
{
    public class CandyController : Controller
    {
        // GET: Candy
        private ICandyRepository repository;
        public int pageSize = 4;
        public CandyController(ICandyRepository rep)
        {
            repository = rep;
        }
        public ViewResult List(string category, int page = 1)
        {
            CandiesListViewModel model = new CandiesListViewModel
            {
                Candies = repository.Candies
                .Where(candy => category == null || candy.Category == category)
                .OrderBy(candy => candy.CandyId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ?
                    repository.Candies.Count() :
                    repository.Candies.Where(candy => candy.Category == category).Count(),
                },
                CurrentCategory = category,
            };
            return View(model);
        }
        public FileContentResult GetImage(int candyId)
        {
            Candy candy = repository.Candies
                .FirstOrDefault(c => c.CandyId == candyId);

            if (candy != null)
            {
                return File(candy.ImageData, candy.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}