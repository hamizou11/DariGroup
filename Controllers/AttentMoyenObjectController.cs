using DariGroupe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace DariGroupe.Controllers
{
    public class AttentMoyenObjectController : Controller
    {
        public ActionResult Index()
        {
            AttentMoyenObject tempsAttenteMoyen = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8080");
                //HTTP GET
                var responseTask = client.GetAsync("SpringMVC/servlet/getTempsAttenteMoyen");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<AttentMoyenObject>();
                    readTask.Wait();

                    tempsAttenteMoyen = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    tempsAttenteMoyen = null;

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(tempsAttenteMoyen);
        }

        // GET: AttentMoyenObject/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AttentMoyenObject/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AttentMoyenObject/Create
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

        // GET: AttentMoyenObject/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AttentMoyenObject/Edit/5
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

        // GET: AttentMoyenObject/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AttentMoyenObject/Delete/5
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
