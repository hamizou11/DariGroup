using DariGroupe.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace DariGroupe.Controllers
{
    public class ComplaintController : Controller
    {
        // GET: Complaint
        public ActionResult Index(string filtre)
        {
            HttpClient Client = new HttpClient();
            var accessToken = (String)Session["JWTtoken"];
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            Client.BaseAddress = new Uri("http://localhost:8082");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("/retrieve-all-Complaint").Result;

            IEnumerable<Complaint> result;

            if (response.IsSuccessStatusCode)
            { result = response.Content.ReadAsAsync<IEnumerable<Complaint>>().Result; }
            else
            {
                result = null;
            }
            if (!String.IsNullOrEmpty(filtre))
            {

            }



            return View(result);
        }

        // GET: Complaint
        public ActionResult ScamComplaint(string filtre)
        {
            HttpClient Client = new HttpClient();
            var accessToken = (String)Session["JWTtoken"];
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            Client.BaseAddress = new Uri("http://localhost:8082");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("/ScamComplaint").Result;

            IEnumerable<Complaint> result;

            if (response.IsSuccessStatusCode)
            { result = response.Content.ReadAsAsync<IEnumerable<Complaint>>().Result; }
            else
            {
                result = null;
            }
            if (!String.IsNullOrEmpty(filtre))
            {

            }



            return View(result);
        }
        // GET: Complaint
        public ActionResult OtherComplaint(string filtre)
        { HttpClient Client = new HttpClient();

            var accessToken = (String)Session["JWTtoken"];
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
           
            Client.BaseAddress = new Uri("http://localhost:8082");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("/OtherComplaint").Result;

            IEnumerable<Complaint> result;

            if (response.IsSuccessStatusCode)
            { result = response.Content.ReadAsAsync<IEnumerable<Complaint>>().Result; }
            else
            {
                result = null;
            }
            if (!String.IsNullOrEmpty(filtre))
            {

            }



            return View(result);
        }
        public ActionResult CustomerComplaint()
        {
            IEnumerable<Complaint> cutomerComplaint = null;

            using (var client = new HttpClient())
            {
                var accessToken = (String)Session["JWTtoken"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                client.BaseAddress = new Uri("http://localhost:8082");
                //HTTP GET
                var responseTask = client.GetAsync("/CustomerComplaint");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Complaint>>();
                    readTask.Wait();

                    cutomerComplaint = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    cutomerComplaint = Enumerable.Empty<Complaint>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(cutomerComplaint);
        }


        // GET: Complaint/Details/5
        public ActionResult Details(int id)
        {
            Complaint c = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8082");
                //HTTP GET
                var responseTask = client.GetAsync("/retrieve-Complaint/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Complaint>();
                    readTask.Wait();

                    c = readTask.Result;
                }
            }

            return View(c);





        }

        // GET: Complaint/Create
        public ActionResult Create()
        {
            List<TypeRec> ComplaintTypes = Enum.GetValues(typeof(TypeRec)).Cast<TypeRec>().ToList();
            ViewBag.type = new SelectList(ComplaintTypes);


            return View();
        }


        // POST: Complaint/Create
        [HttpPost]
        public ActionResult Create(Complaint c, HttpPostedFileBase file)
        {
            c.file = file.FileName;
            if (file.ContentLength > 0)
            {
                var path = Path.Combine(Server.MapPath("~/Content/Upload/"),
                file.FileName);
                file.SaveAs(path);
            }

            HttpClient Client = new HttpClient();
            var accessToken = (String)Session["JWTtoken"];
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            Client.BaseAddress = new Uri("http://localhost:8082");
            Client.PostAsJsonAsync<Complaint>("/add-Complaint", c);
            return Redirect("~/Complaint/CustomerComplaint");
        }

        // GET: Complaint/Edit/5
        public ActionResult Edit(int id)
        {
            List<TypeRec> ComplaintTypes = Enum.GetValues(typeof(TypeRec)).Cast<TypeRec>().ToList();
            ViewBag.type = new SelectList(ComplaintTypes);
            Complaint c = null;

            using (var client = new HttpClient())
            {
                var accessToken = (String)Session["JWTtoken"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                client.BaseAddress = new Uri("http://localhost:8082");
                //HTTP GET
                var responseTask = client.GetAsync("/retrieve-Complaint/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Complaint>();
                    readTask.Wait();

                    c = readTask.Result;
                }
            }

            return View(c);
        }

        // POST: Complaint/Edit/5
        [HttpPost]
        public ActionResult Edit(Complaint c)
        {

            using (var client = new HttpClient())
            {
                var accessToken = (String)Session["JWTtoken"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                client.BaseAddress = new Uri("http://localhost:8082");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<Complaint>("/modify-Complaint", c);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return Redirect("~/Complaint/CustomerComplaint");
                }
            }
            return View(c);
        }
    

        // POST: Complaint/Edit/5
        [HttpPost]
        public ActionResult Approve(int id)
        {

            using (var client = new HttpClient())
            {
                var accessToken = (String)Session["JWTtoken"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                client.BaseAddress = new Uri("http://localhost:8082");

                //HTTP POST
                var postTask = client.GetAsync("/complaint/"+id.ToString()+"/approve");
                postTask.Wait();
                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult Reject(int id)
        {

            using (var client = new HttpClient())
            {
                var accessToken = (String)Session["JWTtoken"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                client.BaseAddress = new Uri("http://localhost:8082");

                //HTTP POST
                var postTask = client.GetAsync("/complaint/" + id.ToString() + "/reject");
                postTask.Wait();
                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }


        // GET: Complaint/Delete/5
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                var accessToken = (String)Session["JWTtoken"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                client.BaseAddress = new Uri("http://localhost:8082");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("/remove-Complaint/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");

        }

        // POST: Complaint/Delete/5
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
