using DariGroupe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;

namespace DariGroupe.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            HttpClient Client = new HttpClient();
            var accessToken = (String)Session["JWTtoken"];
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            Client.BaseAddress = new Uri("http://localhost:8082");
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = Client.GetAsync("/retrieve-all-users").Result;

            IEnumerable<User> result;

            if (response.IsSuccessStatusCode)
            { result = response.Content.ReadAsAsync<IEnumerable<User>>().Result; }
            else
            {
                result = null;
            }


            //return ViewBag;
            //List<User> users = new List<User>();
            //users.Add(new Models.User() { Id = 1, username = "nouha", password = "nouha" });
            return View(result);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        public ActionResult Login()
        {
            //HttpClient Client = new HttpClient();
            //Client.BaseAddress = new Uri("http://localhost:8082");
            //Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //HttpResponseMessage response = Client.GetAsync("/SpringMVC/login").Result;







            return View();
        }
        // POST: User/Create
        [HttpPost]
        public async Task<ActionResult> Login(Authentification aut)
        {

            using (var httpclient = new HttpClient())
            {

                {
                    StringContent stringContent = new StringContent(JsonConvert.SerializeObject(aut), Encoding.UTF8, "application/json");
                    using (var response = await httpclient.PostAsync("http://localhost:8082/api/auth/signin", stringContent))
                    {
                        var token = await response.Content.ReadAsAsync<JwtResponse>();
                        if (response.IsSuccessStatusCode)
                        {
                            Session.Add("JWTtoken", token.token);

                            return Redirect("~/Home/Index");
                        }



                    }
                    ViewBag.Message = "Bad credentials.";
                    return View(aut);



                }




            }
        }



        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(User user)
        {

            // TODO: Add insert logic here
            HttpClient Client = new HttpClient();
            var accessToken = (String)Session["JWTtoken"];
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            Client.BaseAddress = new Uri("http://localhost:8082");
            Client.PostAsJsonAsync<User>("/account/customerRegister", user);
            return RedirectToAction("Index");
        }







        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            User u = null;

            using (var client = new HttpClient())
            {
                var accessToken = (String)Session["JWTtoken"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                client.BaseAddress = new Uri("http://localhost:8082");
                //HTTP GET
                var responseTask = client.GetAsync("/retrieve-user/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<User>();
                    readTask.Wait();

                    u = readTask.Result;
                }
            }

            return View(u);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(User user)
        {
            using (var client = new HttpClient())
            {
                var accessToken = (String)Session["JWTtoken"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                client.BaseAddress = new Uri("http://localhost:8082");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<User>("/modify-user", user);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(user);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                var accessToken = (String)Session["JWTtoken"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                client.BaseAddress = new Uri("http://localhost:8082");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("/remove-user/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }

        // POST: User/Delete/5
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
