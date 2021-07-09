using Halloween.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Halloween.Controllers
{
    public class UserController : Controller
    {
        HalloweenContext hlw = new HalloweenContext();

        // GET: User
        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Index(User u)
        {
            if(ModelState.IsValid)
            {
                var user = hlw.Users.Where(x => x.UserName == u.UserName && x.Password == u.Password).FirstOrDefault();
                if (user != null)
                {
                    Session["username"] = u.UserName;
                    Session["contact"] = user.Contact ;

                    return RedirectToAction("Index","Home");
                }
                else
                {
                    TempData["logerror"] = "Invalid UserName or Passowrd";
                    return View();

                }
            }
            return View();
                
            
            
        }
        public ActionResult Output()
        {
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Register(User u)
        {
            hlw.Users.Add(u);
            hlw.SaveChanges();
            TempData["rs"] = "Registration is Successfull";
            return RedirectToAction("Index");
        } 
    }
}