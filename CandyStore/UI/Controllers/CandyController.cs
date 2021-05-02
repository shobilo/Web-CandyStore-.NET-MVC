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
        public ViewResult List(int page = 1)
        {
            CandiesListViewModel model = new CandiesListViewModel
            {
                Candies = repository.Candies
                .OrderBy(candy => candy.CandyId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = repository.Candies.Count()
                }
            };
            return View(model);
        }
    }
}