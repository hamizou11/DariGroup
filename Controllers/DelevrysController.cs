using DariGroupe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace DariGroupe.Controllers
{
    public class DelevrysController : Controller
    {
        // GET: Delevrys
        public ActionResult Index()
        {
            IEnumerable<Delevrys> delevrys = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8080");
                //HTTP GET
                var responseTask = client.GetAsync("/SpringMVC/servlet/getDeliveries");//hot pathok
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Delevrys>>();
                    readTask.Wait();

                    delevrys = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    delevrys = Enumerable.Empty<Delevrys>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(delevrys);


        }
    

        // GET: Delevrys/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Delevrys/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Delevrys/Create

        [HttpPost]
        public ActionResult Create(Delevrys delevry)
        {
            Delevrys delevrys = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8080/SpringMVC/servlet/");


                var postJob = client.PostAsJsonAsync<Delevrys>("addDelivery", delevry);
                postJob.Wait();
                var postResult = postJob.Result;
                if (postResult.IsSuccessStatusCode)
                    return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Server occured ");
            return View(delevry);
        }
    
        // GET: Delevrys/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Delevrys/Edit/5
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

        // GET: Delevrys/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Delevrys/Delete/5
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
