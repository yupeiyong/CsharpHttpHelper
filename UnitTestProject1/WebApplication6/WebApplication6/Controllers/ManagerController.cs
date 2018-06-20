using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication6.Models;


namespace WebApplication6.Controllers
{
    public class ManagerController : Controller
    {
        // GET: Manager
        public ActionResult Index(User user)
        {
            return View(user);
        }
    }
}