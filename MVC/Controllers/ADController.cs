using MVC.AdServices;
using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class ADController : Controller
    {
        ADCrud adc = new ADCrud();
        // GET: AD
        public ActionResult Index()
        {
            return View();
        }
        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(ADUserModel um)
        {
            User u = new User
            {
              
                FirstName=um.firstname,
                LastName = um.lastName,
                Password = um.password,
                Email = um.email,
                UserName=um.username
                
            };
            try
            {
                // TODO: Add insert logic here
                adc.AddUser(u);
               
                return RedirectToAction("Index");
            }
            catch
            {

                return View();
            }
        }
    }
}