using IFSPRojectTest.Persitance.db;
using IFSPRojectTest.Persitance.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace IFSPRojectTest.Controllers
{
    public class AccountController : Controller
    {
        IFSTestDBcontext dbContext = null;
        public AccountController()
        {
            dbContext = new IFSTestDBcontext();
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(IFSLoginView objIFSLoginView)
        {
            if (ModelState.IsValid)
            {
                var result = dbContext.UserMaster.Where(c => c.UserName == objIFSLoginView.UserName && c.Password == objIFSLoginView.Password).FirstOrDefault();

                if (result != null)
                {
                    Session[Common.CandidateUserId] = Convert.ToInt32(result.id);
                    Session[Common.CandidateUserName] = objIFSLoginView.UserName;
                    return RedirectToAction("Index", "Home");
                }
                else
                {

                }
            }

            return View(objIFSLoginView);
        }


        // GET: /Account/LogOff
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.RemoveAll();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Register()
        {
            return View();
        }
    }
}