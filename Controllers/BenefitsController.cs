using DariGroupe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace DariGroupe.Controllers
{
    public class BenefitsController : Controller
    {
        // GET: Benefits
        public ActionResult Index()
        {
            //IEnumerable<Benefits> benefits = null;
            Benefits benefits = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8090/api/");
                var responseTask = client.GetAsync("subscription/checkBenefits/1");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    //var readJob = result.Content.ReadAsAsync<IList<Benefits>>();
                    var readJob = result.Content.ReadAsAsync<Benefits>();
                    readJob.Wait();
                    benefits = readJob.Result;
                }
                else
                {
                    //Return Error Code Here
                    //benefits = Enumerable.Empty<Benefits>();
                    benefits = null;
                    ModelState.AddModelError(string.Empty, "Server Error!");
                }
            }

            return View(benefits);
        }
    }
}