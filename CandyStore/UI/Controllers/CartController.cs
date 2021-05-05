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
        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(int candyId, string returnUrl)
        {
            Candy candy = repository.Candies
                .FirstOrDefault(c => c.CandyId == candyId);

            if (candy != null)
            {
                GetCart().AddItem(candy, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(int candyId, string returnUrl)
        {
            Candy candy = repository.Candies
                .FirstOrDefault(c => c.CandyId == candyId);

            if (candy != null)
            {
                GetCart().RemoveLine(candy);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
    }
}