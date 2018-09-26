using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ch24ShoppingCartMVC.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ch24ShoppingCartMVC.Controllers
{
    public class CartController : Controller
    {
        private CartModel cart = new CartModel();

        [HttpGet]
        public RedirectToRouteResult Index()
        {
            return RedirectToAction("List/");
        }
        [HttpGet]
        public ViewResult List()
        {
            CartViewModel model = (CartViewModel)TempData["cart"];
            //if the model is null, then call the method GetCart
            if(model == null)
            {
                model = cart.GetCart();
            }

            //Passing model to View
            return View(model);
        }
        [HttpPost]
        public RedirectToRouteResult List(OrderViewModel order)
        {
            CartViewModel model = cart.GetCart(order.SelectedProduct.ProductID);
            //Assign the quantity of the selected product to the quantity of the added product
            model.AddedProduct.Quantity = order.SelectedProduct.Quantity;
            //Call the method AddtoCart
            cart.AddToCart(model);
            //Assign model to the TempData
            model = (CartViewModel)TempData["list"];
            return RedirectToAction("List", "Cart");
        }

        public ViewResult Checkout()
        {
            CartViewModel model = (CartViewModel)TempData["cart"];
            //if the model is null, then call the method GetCart
            if (model == null)
            {
                model = cart.GetCart();
            }

            //Passing model to View
            return View(model);
        }

        public static bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            string ccValue = value as string;
            if (ccValue == null)
            {
                return false;
            }
            ccValue = ccValue.Replace("-", "");
            ccValue = ccValue.Replace(" ", "");

            int checksum = 0;
            bool evenDigit = false;

            // http://www.beachnet.com/~hstiles/cardtype.html
            foreach (char digit in ccValue.Reverse())
            {
                if (digit < '0' || digit > '9')
                {
                    return false;
                }

                int digitValue = (digit - '0') * (evenDigit ? 2 : 1);
                evenDigit = !evenDigit;

                while (digitValue > 0)
                {
                    checksum += digitValue % 10;
                    digitValue /= 10;
                }
            }

            return (checksum % 10) == 0;
        }
    }
}
