using DariGroupe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace DariGroupe.Controllers
{
    public class FurnituresController : Controller
    {
        // GET: Furnitures
        public ActionResult Index()
        {
            IEnumerable<Furnitures> furnitures = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8080");
                //HTTP GET
                var responseTask = client.GetAsync("/SpringMVC/servlet/retrieve-all-furnitures");//hot pathok
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Furnitures>>();
                    readTask.Wait();

                    furnitures = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    furnitures = Enumerable.Empty<Furnitures>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(furnitures);

        }

        // GET: Furnitures/Details/5
        public ActionResult Details(int id)
        {
            Furnitures furnitures = null;
            using (var client = new HttpClient())
            {
                //client.DefaultRequestHeaders.Clear();
                // client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", str);
                client.BaseAddress = new Uri("http://localhost:8080/");
                //  client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "Your Oauth token");
                var responseTask = client.GetAsync("SpringMVC/servlet/retrieve-Furniture/" + id.ToString());
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Furnitures>();
                    readTask.Wait();
                    furnitures = readTask.Result;
                }

            }
            return View(furnitures);
        }

        // GET: Furnitures/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Furnitures/Create


        [HttpPost]
        public ActionResult Create(Furnitures furnitures)
        {
           
            Furnitures furniture = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8080/SpringMVC/servlet/");


                var postJob = client.PostAsJsonAsync<Furnitures>("add-furniture", furnitures);
                postJob.Wait();
                var postResult = postJob.Result;
                if (postResult.IsSuccessStatusCode)
                    return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Server occured ");
            return View(furniture);
        }

        // GET: Furnitures/Edit/5
        public ActionResult Edit(long id)
        {

            Furnitures furniture = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8080");
                var responseTask = client.GetAsync("SpringMVC/servlet/retrieve-Furniture/" + id.ToString());
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Furnitures>();
                    readTask.Wait();
                    furniture = readTask.Result;
                }
            }
            return View(furniture);
        }

        // POST: Furnitures/Edit/5
        [HttpPost]
        public ActionResult Edit(Furnitures furniture)
        {
            using (var client = new HttpClient())
            {
                


                client.BaseAddress = new Uri("http://localhost:8080");
                var putTask = client.PutAsJsonAsync<Furnitures>("SpringMVC/servlet/modify-furniture/", furniture);
                putTask.Wait();

                var ressult = putTask.Result;
                if (ressult.IsSuccessStatusCode)

                { return RedirectToAction("Index"); }

                return View(furniture);
            }
        }

                // GET: Furnitures/Delete/5
                public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8080");
                var deleteFurniture = client.DeleteAsync("SpringMVC/servlet/remove-Furniture/" + id.ToString());
                var result = deleteFurniture.Result;
                if (result.IsSuccessStatusCode)
                    return RedirectToAction("Index");
            }
            return View();
        }

        // POST: Furnitures/Delete/5
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
