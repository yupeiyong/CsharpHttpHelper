using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication6.Models;


namespace WebApplication6.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult LoginResult(string account, string password)
        {
            if(string.IsNullOrEmpty(account)||string.IsNullOrEmpty(password))
                throw new Exception("帐号或密码为空！");

            var user=new User {AccountName=account,Password=password};
            return RedirectToAction("Index", "Manager", user);
        }
    }
}