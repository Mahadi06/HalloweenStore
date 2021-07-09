using Halloween.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Halloween.Controllers
{
    public class HomeController : Controller
    {
        HalloweenContext hlw = new HalloweenContext();

        

       [HttpGet]
        public ActionResult Index()
        {
            if(Session["username"] != null)
            {
                return View();
            }
            return RedirectToAction("Index", "User");

        }

        public ActionResult Order()
        {
            if(Session["username"] != null)
            {
                if (TempData["cart"] != null)
                {
                    float x = 0;
                    List<Cart> li2 = TempData["cart"] as List<Cart>;
                    foreach (var item in li2)
                    {
                        x += item.Bill;

                    }

                    TempData["total"] = x;
                }
                TempData.Keep();

                return View(hlw.Products.OrderByDescending(x => x.ProductId).ToList());
            }

            return RedirectToAction("Index", "User");

        }
        [HttpGet]
        public ActionResult Cart(int? Id)
        {
            if(Session["username"] != null)
            {
                Product p = hlw.Products.Where(x => x.ProductId == Id).SingleOrDefault();
                return View(p);
            }
            return RedirectToAction("Index", "User");
        }

        List<Cart> li = new List<Cart>();
        [HttpPost]
        public ActionResult Cart(Product pi, string qty, int Id)
        {

            Product p = hlw.Products.Where(x => x.ProductId == Id).SingleOrDefault();

            Cart c = new Cart();
            c.ProductId = p.ProductId;
            c.Price = (float)p.Price;
            c.Quantity = Convert.ToInt32(qty);
            c.Bill = c.Price * c.Quantity;
            c.productName = p.ProductName;
            if (TempData["cart"] == null)
            {
                li.Add(c);
                TempData["cart"] = li;

            }
            else
            {
                List<Cart> li2 = TempData["cart"] as List<Cart>;
                int count = 0;
                foreach(var item in li2)
                {
                    if(item.ProductId == c.ProductId)
                    {
                        item.Quantity += c.Quantity;
                        item.Bill += c.Bill;
                        count = 1;
                    }
                    
                }
                if(count == 0)
                {
                    li2.Add(c);
                }
                TempData["cart"] = li2;
            }

            TempData.Keep();




            return RedirectToAction("Order");
        }


        public ActionResult Checkout()
        {
            TempData.Keep();

            if (Session["username"] != null)
            {
                return View();

            }

            return RedirectToAction("Index", "User");

        }
        public ActionResult Logout()
        {
            Session["username"] = null;
            TempData["cart"] = null;
            TempData["total"] = null;
            TempData["regSuc"] = null;
            TempData["logerror"] = null;


            return RedirectToAction("Index","User");
        }

        //[HttpPost, ActionName("Checkout")]
        //public ActionResult Check()
        //{

        //    List<Cart> li = TempData["cart"] as List<Cart>;
        //    Receipt r = new Receipt();
        //    r.UserId = Convert.ToInt32(Session["uid"].ToString());
        //    r.IssueDate = System.DateTime.Now;
        //    r.

        //    return View();
        //}
        public ActionResult ContactUs()
        {
            return View();
        }


    }
}