using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace web2Blank.Controllers
{
    public class ProfileController : Controller {

        public ActionResult HTML()
        {

            return View();
        }
        public ActionResult Index()
        {
            ViewData["IDList"] = GetIDLists();
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection col)
        {
            string strID = col["lstIDs"].ToString();

            if (strID != "" && col["btnSubmit"].ToString() == "set" ) //the set button was pressed
			{
                return RedirectToAction("Properties", new { id = strID }); //Profile/Properties/2
            }
            if (strID != "" && col["btnSubmit"].ToString() == "address") 
            {
                return RedirectToAction("UserAddress", new { id = strID });
            }
            return View();
        }

        public ActionResult AnyThing()
        {
            Models.Database db = new Models.Database();
            Models.User u = new Models.User();

            if(RouteData.Values["id"] != null)
			{
                string strID = RouteData.Values["id"].ToString();
                u = db.GetUser(Int16.Parse(strID));
			}
            return View(u);
        }

        public ActionResult Properties()
        {
            Models.Database db = new Models.Database();
            Models.User u = new Models.User();

            if (RouteData.Values["id"] != null)
            {
                string strID = RouteData.Values["id"].ToString();
                u = db.GetUser(Int16.Parse(strID));
            }
            return View(u);
        }

        public ActionResult UserAddress()
        {
            Models.Database db = new Models.Database();
            Models.User u = new Models.User();

            if (RouteData.Values["id"] != null)
            {
                string strID = RouteData.Values["id"].ToString();
                u = db.GetUser(Int16.Parse(strID));
            }

            return View(u);
        }


        [HttpPost]
        public ActionResult Properties(FormCollection col)
        {
            Models.User u = new Models.User();

            if (col["btnSubmit"].ToString() == "home") { return RedirectToAction("Index"); }

            if (col["btnSubmit"].ToString() == "submit") {  //the save button was pressed
                string strUserID = RouteData.Values["id"].ToString();

                u.FirstName = col["FirstName"].ToString();
                u.LastName = col["LastName"].ToString();
                u.UserID = col["UserID"].ToString();
                u.Password = col["Password"].ToString();

                if (col["HomeAddress.Address1"] != null)
				{
                    u.HomeAddress = new Models.Address();
                    u.HomeAddress.Address1 = col["HomeAddress.Address1"].ToString();
                    u.HomeAddress.Address2 = col["HomeAddress.Address2"].ToString();
                    u.HomeAddress.City = col["HomeAddress.City"].ToString();
                    u.HomeAddress.State = col["HomeAddress.State"].ToString();
                    u.HomeAddress.Zip = col["HomeAddress.Zip"].ToString();
                }
                if (col["WorkAddress.Address1"] != null)
                {
                    u.WorkAddress = new Models.Address();
                    u.WorkAddress.Address1 = col["WorkAddress.Address1"].ToString();
                    u.WorkAddress.Address2 = col["WorkAddress.Address2"].ToString();
                    u.WorkAddress.City = col["WorkAddress.City"].ToString();
                    u.WorkAddress.State = col["WorkAddress.State"].ToString();
                    u.WorkAddress.Zip = col["WorkAddress.Zip"].ToString();
                }
                if (col["Home2Address.Address1"] != null)
                {
                    u.Home2Address = new Models.Address();
                    u.Home2Address.Address1 = col["Home2Address.Address1"].ToString();
                    u.Home2Address.Address2 = col["Home2Address.Address2"].ToString();
                    u.Home2Address.City = col["Home2Address.City"].ToString();
                    u.Home2Address.State = col["Home2Address.State"].ToString();
                    u.Home2Address.Zip = col["Home2Address.Zip"].ToString();
                }

                // Get the cell phone data
                u.CellPhone = new Models.PhoneNumber();
                u.CellPhone.AreaCode = col["CellPhone.AreaCode"].ToString();
                u.CellPhone.Prefix = col["CellPhone.Prefix"].ToString();
                u.CellPhone.Suffix = col["CellPhone.Suffix"].ToString();

                // Get the work phone data
                u.WorkPhone = new Models.PhoneNumber();
                u.WorkPhone.AreaCode = col["WorkPhone.AreaCode"].ToString();
                u.WorkPhone.Prefix = col["WorkPhone.Prefix"].ToString();
                u.WorkPhone.Suffix = col["WorkPhone.Suffix"].ToString();

                // Get the home phone data
                u.HomePhone = new Models.PhoneNumber();
                u.HomePhone.AreaCode = col["HomePhone.AreaCode"].ToString();
                u.HomePhone.Prefix = col["HomePhone.Prefix"].ToString();
                u.HomePhone.Suffix = col["HomePhone.Suffix"].ToString();

                // Get the emergency phone data
                u.EmergencyPhone = new Models.PhoneNumber();
                u.EmergencyPhone.AreaCode = col["EmergencyPhone.AreaCode"].ToString();
                u.EmergencyPhone.Prefix = col["EmergencyPhone.Prefix"].ToString();
                u.EmergencyPhone.Suffix = col["EmergencyPhone.Suffix"].ToString();

                ViewData["DataSubmitted"] = "true";
            }
            return View(u);
        }
        [HttpPost]
        public ActionResult UserAddress(FormCollection col)
        {
            Models.User u = new Models.User();

            if (col["btnSubmit"].ToString() == "home") { return RedirectToAction("Index"); }

            if (col["btnSubmit"].ToString() == "submit")
            {  //the save button was pressed
                string strUserID = RouteData.Values["id"].ToString();

                if (col["HomeAddress.Address1"] != null)
                {
                    u.HomeAddress = new Models.Address();
                    u.HomeAddress.Address1 = col["HomeAddress.Address1"].ToString();
                    u.HomeAddress.Address2 = col["HomeAddress.Address2"].ToString();
                    u.HomeAddress.City = col["HomeAddress.City"].ToString();
                    u.HomeAddress.State = col["HomeAddress.State"].ToString();
                    u.HomeAddress.Zip = col["HomeAddress.Zip"].ToString();
                }
                if (col["WorkAddress.Address1"] != null)
                {
                    u.WorkAddress = new Models.Address();
                    u.WorkAddress.Address1 = col["WorkAddress.Address1"].ToString();
                    u.WorkAddress.Address2 = col["WorkAddress.Address2"].ToString();
                    u.WorkAddress.City = col["WorkAddress.City"].ToString();
                    u.WorkAddress.State = col["WorkAddress.State"].ToString();
                    u.WorkAddress.Zip = col["WorkAddress.Zip"].ToString();
                }
                if (col["Home2Address.Address1"] != null)
                {
                    u.Home2Address = new Models.Address();
                    u.Home2Address.Address1 = col["Home2Address.Address1"].ToString();
                    u.Home2Address.Address2 = col["Home2Address.Address2"].ToString();
                    u.Home2Address.City = col["Home2Address.City"].ToString();
                    u.Home2Address.State = col["Home2Address.State"].ToString();
                    u.Home2Address.Zip = col["Home2Address.Zip"].ToString();
                }

                ViewData["DataSubmitted"] = "true";
            }
            return View(u);
        }

        private List<SelectListItem> GetIDLists() {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "(select)", Value = "" });
            items.Add(new SelectListItem { Text = "1 (one)", Value = "1" });
            items.Add(new SelectListItem { Text = "2 (two)", Value = "2" });
            items.Add(new SelectListItem { Text = "3 (three)", Value = "3" });
            return items;
        }
    }
}