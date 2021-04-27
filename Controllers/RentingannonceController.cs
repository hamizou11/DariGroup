using DariGroupe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

using System.Web;
using System.Web.Mvc;

namespace DariGroupe.Controllers
{
    public class RentingannonceController : Controller
    {
        // GET: Rentingannonce
        public ActionResult Renting(string Keyword)
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("https://localhost:44392");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("http://localhost:8084/SpringMVC/servlet/renting/retrieves-all-rentingannonce").Result;
            /* HttpResponseMessage responses = Client.GetAsync("http:localhost:8084/SpringMVC/servlet/renting/actualite").Result;*/
            HttpResponseMessage responsesearch = Client.GetAsync("http://localhost:8084/SpringMVC/servlet/renting/search/" + Keyword + "").Result;
            if (Keyword == "all")
            {
                ViewBag.resultse = response.Content.ReadAsAsync<List<Rentingannonce>>().Result;
            }
            else
            {
                // ViewBag.resultsea = responsesearch.Content.ReadAsAsync<List<Rentingannonce>>().Result;
            }


            ViewBag.Message = "Your application description page.";

            return View("~/Views/RentingAnnonce/Renting.cshtml");
        }
        public ActionResult Index()
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("https://localhost:44392/");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("http://localhost:8084/SpringMVC/servlet/renting/retrieves-all-rentingannonce").Result;
            if (response.IsSuccessStatusCode)
            {
                ViewBag.result = response.Content.ReadAsAsync<IEnumerable<Rentingannonce>>().Result;
            }
            else
            {
                ViewBag.result = " erreur";
            }


            return View("~/Views/RentingAnnonce/Renting.cshtml");
        }



        public ActionResult LastRenting()
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("https://localhost:44392/");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("http://localhost:8084/SpringMVC/servlet/renting/latestRentingAnnonce").Result;
            if (response.IsSuccessStatusCode)
            {
                ViewBag.result = response.Content.ReadAsAsync<IEnumerable<Rentingannonce>>().Result;
            }
            else
            {
                ViewBag.result = " erreur";
            }


            return View("~/Views/RentingAnnonce/Renting.cshtml");
        }
        public ActionResult Best()
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("https://localhost:44392/");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("http://localhost:8084/SpringMVC/servlet/renting/BestReviewed").Result;
            if (response.IsSuccessStatusCode)
            {
                ViewBag.result = response.Content.ReadAsAsync<IEnumerable<Rentingannonce>>().Result;
            }
            else
            {
                ViewBag.result = " erreur";
            }


            return View("~/Views/RentingAnnonce/Renting.cshtml");
        }
    }

}