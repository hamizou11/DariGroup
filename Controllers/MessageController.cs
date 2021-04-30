using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace DariGroupe.Controllers
{
    public class MessageController : Controller
    {
        // GET: Message
        public ActionResult Index()
        {



            HttpClient client = new HttpClient();
            var accessToken = (String)Session["JWTtoken"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                client.BaseAddress = new Uri("http://localhost:8082");
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //HTTP GET
            HttpResponseMessage response = client.GetAsync("/message").Result;

                IEnumerable<Message> result;
                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.ReadAsAsync<IEnumerable<Message>>().Result;



                }
                else
                {
                    result = null;
                }

                return View(result);
            
        }
        // GET: Message
        public ActionResult MessagesNotif()
        {
            IEnumerable<Message> cutomerMessage = null;

            using (var client = new System.Net.Http.HttpClient())
            {
                var accessToken = (String)Session["JWTtoken"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                client.BaseAddress = new Uri("http://localhost:8082");
                //HTTP GET
                var responseTask = client.GetAsync("/messages");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Message>>();
                    readTask.Wait();

                    cutomerMessage = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    cutomerMessage = Enumerable.Empty<Message>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(cutomerMessage);
        }

        // GET: Message/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Message/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Message/Create
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

        // GET: Message/Edit/5
        public ActionResult ReadM(long id)
        {

            using (var client = new HttpClient())
            {
                var accessToken = (String)Session["JWTtoken"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                client.BaseAddress = new Uri("http://localhost:8082");
                //HTTP GET
                var responseTask = client.GetAsync("/message/read/" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult ReadFromNotif(long id)
        {

            using (var client = new HttpClient())
            {
                var accessToken = (String)Session["JWTtoken"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                client.BaseAddress = new Uri("http://localhost:8082");
                //HTTP GET
                var responseTask = client.GetAsync("/messages/read/" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("MessagesNotif");
                }
            }
            return RedirectToAction("MessagesNotif");
        }

        // POST: Message/Edit/5
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

        // GET: Message/Delete/5
        public ActionResult Delete(long id)
        {
            using (var client = new HttpClient())
            {
                var accessToken = (String)Session["JWTtoken"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                client.BaseAddress = new Uri("http://localhost:8082");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("/message/delete/" + id);
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }

        // POST: Message/Delete/5
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
