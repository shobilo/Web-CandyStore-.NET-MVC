using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class CandyController : Controller
    {
        // GET: Candy
        private ICandyRepository repository;
        public CandyController(ICandyRepository rep)
        {
            repository = rep;
        }
        public ViewResult List()
        {
            return View(repository.Candies);
        }
    }
}