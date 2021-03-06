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
    public class CartController : Controller
    {
        // GET: Cart
        private ICandyRepository repository;
        public CartController(ICandyRepository repo)
        {
            repository = repo;
        }
        public ViewResult Index(Cart cart,string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(Cart cart, int candyId, string returnUrl)
        {
            Candy candy = repository.Candies
                .FirstOrDefault(c => c.CandyId == candyId);

            if (candy != null)
            {
                cart.AddItem(candy, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int candyId, string returnUrl)
        {
            Candy candy = repository.Candies
                .FirstOrDefault(c => c.CandyId == candyId);

            if (candy != null)
            {
                cart.RemoveLine(candy);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }
        public ViewResult Checkout(Cart cart, string returnUrl)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Dear user your cart is empty!");
                return View(new CartIndexViewModel
                {
                    Cart = cart,
                    ReturnUrl = returnUrl
                });
            }
            else
            {
                return View(new ShippingInfo());
            }
        }


        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingInfo shippingInfo)
        {

            if (ModelState.IsValid)
            {
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingInfo);
            }
        }
    }
}