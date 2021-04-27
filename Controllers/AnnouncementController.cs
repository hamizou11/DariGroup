using DariGroupe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace DariGroupe.Controllers
{
    public class AnnouncementController : Controller
    {
        // GET: Announcement
        public ActionResult Index()
        {
            IEnumerable<Announcement> list = null;
            using (var a = new HttpClient())
            {
                a.BaseAddress = new Uri("http://localhost:8090");
                var responseTask = a.GetAsync("/getAll");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<IList<Announcement>>();
                    readJob.Wait();
                    list = readJob.Result;
                }
                else
                {
                    list = Enumerable.Empty<Announcement>();
                    ModelState.AddModelError(string.Empty, "serveur error");
                }
            }
            return View(list);
        }

        // GET: Announcement/Details/5
        public ActionResult Details(int id)
        {
            Announcement announcement = null;
            using (var client = new HttpClient())
            {
                //client.DefaultRequestHeaders.Clear();
                // client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", str);
                client.BaseAddress = new Uri("http://localhost:8090/");
                //  client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "Your Oauth token");
                var responseTask = client.GetAsync("GetDetail/" + id.ToString());
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Announcement>();
                    readTask.Wait();
                    announcement = readTask.Result;
                }

            }
            return View(announcement);
        }

        // GET: Announcement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Announcement/Create
        [HttpPost]
        public ActionResult Create(Announcement a)
        {
            using (var client = new HttpClient())
            { // TODO: Add insert logic here


                client.BaseAddress = new Uri("http://localhost:8090");
                var post = client.PostAsJsonAsync<Announcement>("/add", a);
                post.Wait();
                var postResult = post.Result;
                if (postResult.IsSuccessStatusCode)
                    return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "server occured errors");
            return View(a);
        }

        // GET: Announcement/Edit/5
        public ActionResult Edit(int id)
        {
            Announcement announcement = null;
            using (var client = new HttpClient())
            {
                //client.DefaultRequestHeaders.Clear();
                // client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", str);
                client.BaseAddress = new Uri("http://localhost:8090/");
                //  client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "Your Oauth token");
                var responseTask = client.GetAsync("GetDetail/" + id.ToString());
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Announcement>();
                    readTask.Wait();
                    announcement = readTask.Result;
                }

            }
            return View(announcement);
        }

        // POST: Announcement/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Announcement announcement)
        {
            using (var client = new HttpClient())
            {


                client.BaseAddress = new Uri("http://localhost:8090/");
                var putTask = client.PutAsJsonAsync<Announcement>("modify/", announcement);
                putTask.Wait();

                var ressult = putTask.Result;
                if (ressult.IsSuccessStatusCode)

                { return RedirectToAction("Index"); }

                return View(announcement);


            }
        }

        // GET: Announcement/Delete/5
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8090");
                var deleteAnnouncement = client.DeleteAsync("delete/" + id.ToString());
                var result = deleteAnnouncement.Result;
                if (result.IsSuccessStatusCode)
                    return RedirectToAction("Index");
            }
            return View();
        }

        // POST: Announcement/Delete/5
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
        public ActionResult PDF()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8090");
                var pdf = client.DeleteAsync("/annonce/export/pdf/12");
                var result = pdf.Result;
                if (result.IsSuccessStatusCode)
                    return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult order()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8090");
                var deleteAnnouncement = client.GetAsync("/order");
                var result = deleteAnnouncement.Result;
                if (result.IsSuccessStatusCode)
                    return RedirectToAction("Index");
            }
            return View();
        }
    }
}
