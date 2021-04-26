using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using DariGroupe.Models;

namespace DariGroupe.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        public ActionResult Index()
        {
            Paylink paylink = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8090/api/");
                var responseTask = client.GetAsync("paypal/pay/1");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<Paylink>();
                    readJob.Wait();
                    paylink = readJob.Result;
                }
                else
                {
                    paylink = null;
                    ModelState.AddModelError(string.Empty, "Server Error!");
                }
            }

            return View(paylink);
        }
    }
}