using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web2.Models;

namespace web2.Controllers
{
    public class AboutUsController : Controller
    {
        // GET: AboutUs/Index
        public ActionResult Index()
        {
            var u = new User();
            u.FirstName = "Keith";
            u.LastName = "Brock";
            u.Email = "kbrock@loho.com"; 
            return View(u);
        }

        // POST: AboutUs/Index
        [HttpPost]
        public ActionResult Index(FormCollection col)
        {
            if (col["btnSubmit"] == "more")
                return RedirectToAction("More");
            return RedirectToAction("Index", "Home");
        }

        // GET: AboutUs/More
        public ActionResult More()
        {
            var u = new User();
            u.FirstName = "Keith";
            u.LastName = "Brock";
            return View(u);
        }

        // POST: AboutUs/More
        [HttpPost]
        public ActionResult More(FormCollection col)
        {
            return RedirectToAction("Index");
        }

    }

}