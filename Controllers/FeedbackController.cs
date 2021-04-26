using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DariGroupe.Models;
using System.Net.Http;

namespace DariGroupe.Controllers
{
    public class FeedbackController : Controller
    {
        // GET: Feedback
        public ActionResult Index()
        {
            return View();
        }

        // POST: Feedback
        public ActionResult Create()
        {
            return View();
        }

        // POST: Feedback
        [HttpPost]
        public ActionResult Create(Feedback feedback)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8090/api/contactps");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<Feedback>("", feedback);
                postTask.Wait();

                var result = postTask.Result;
                Response.Write("<script>alert('" + result + "')</script>");
                if (result.IsSuccessStatusCode)
                {
                    Response.Write("<script>alert('" + "Feedback Sent!" + "')</script>");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server Error");
                }
            }
            return View(feedback);
        }
    }
    
}