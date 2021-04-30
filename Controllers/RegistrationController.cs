using DariGroupe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace DariGroupe.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult ConfirmRegistration()
        {
            string token = Request.QueryString["token"];
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:8082");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("/confirm-account?token="+token).Result;

            

            if (response.IsSuccessStatusCode)
            { return RedirectToAction("Login","User"); }
            
            
                return View();
            
            
        }

        // GET: Registration/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Registration/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Registration/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        // GET: Registration/CustomerRegistraion
        public ActionResult CustomerRegistraion()
        {
            return View();
        }

        // POST: Registration/CustomerRegistraion
        [HttpPost]
        public ActionResult CustomerRegistraion(User user, HttpPostedFileBase file)
        {
            //user.picture = file.FileName;
            //if (file.ContentLength > 0)
            //{
            //    var path = Path.Combine(Server.MapPath("~/Content/Upload/"),
            //    file.FileName);
            //    file.SaveAs(path);
            //}

            HttpClient Client = new HttpClient();

            Client.BaseAddress = new Uri("http://localhost:8082");
            Client.PostAsJsonAsync<User>("/account/customerRegister", user);
            return View(user);





        }

        // GET: Registration/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Registration/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Registration/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Registration/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
