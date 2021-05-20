using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class AdminController : Controller
    {
        ICandyRepository repository;

        public AdminController(ICandyRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Candies);
        }
        public ViewResult Edit(int CandyId)
        {
            Candy candy = repository.Candies
                .FirstOrDefault(g => g.CandyId == CandyId);
            return View(candy);
        }
        public ViewResult Create()
        {
            return View("Edit", new Candy());
        }
        [HttpPost]
        public ActionResult Edit(Candy candy)
        {
            if (ModelState.IsValid)
            {
                repository.SaveChanges(candy);
                TempData["message"] = string.Format("Changes for \"{0}\" were saved", candy.Name);
                return RedirectToAction("Index");
            }
            else
            {
                // Что-то не так со значениями данных
                return View(candy);
            }
        }
        [HttpPost]
        public ActionResult Remove(int candyId)
        {
            Candy removeCandy = repository.Remove(candyId);
            if (removeCandy != null)
            {
                TempData["message"] = string.Format("Candy \"{0}\" was removed",
                    removeCandy.Name);
            }
            return RedirectToAction("Index");
        }
    }
}