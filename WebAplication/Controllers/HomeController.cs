using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using WebAplication.Helper;
using WebAplication.Models;

namespace WebAplication.Controllers
{
    public class HomeController : Controller
    {
        MovieApi _movieApi = new MovieApi();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //List<MovieData> movies = new List<MovieData>();
            //HttpClient client = _movieApi.Initial();
            //HttpResponseMessage httpResponseMessage = await client.GetAsync("api/movie");

            //if (httpResponseMessage.IsSuccessStatusCode)
            //{
            //    var results = httpResponseMessage.Content.ReadAsStringAsync().Result;
            //    movies = JsonConvert.DeserializeObject<List<MovieData>>(results);
            //}

            return View(/*movies*/);
        }

        [HttpGet]
        public async Task<IActionResult> Movies()
        {
            List<MovieData> movies = new List<MovieData>();
            HttpClient client = _movieApi.Initial();
            HttpResponseMessage httpResponseMessage = await client.GetAsync("api/movie");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var results = httpResponseMessage.Content.ReadAsStringAsync().Result;
                movies = JsonConvert.DeserializeObject<List<MovieData>>(results);
            }

            return View(movies);
        }

        //public async Task<IActionResult> Details(Guid id)
        //{
        //    var movie = new MovieData();
        //    HttpClient client = _movieApi.Initial();
        //    HttpResponseMessage httpResponseMessage = await client.GetAsync($"api/movie/{id}");

        //    if (httpResponseMessage.IsSuccessStatusCode)
        //    {
        //        var results = httpResponseMessage.Content.ReadAsStringAsync().Result;
        //        movie = JsonConvert.DeserializeObject<MovieData>(results);
        //    }

        //    return View(movie);
        //}

        public ActionResult create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(MovieData movie)
        {
            HttpClient client = _movieApi.Initial();
            //HttpPost
            var postTask = client.PostAsJsonAsync("api/movie", movie);
            postTask.Wait();

            var result = postTask.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Movies");
            }

            return View();
        }

        public ActionResult Edit(Guid id)
        {
            var movie = new MovieData();
            HttpClient client = _movieApi.Initial();
            var request = client.GetAsync("api/movie/" + id.ToString());
            request.Wait();

            var result = request.Result;
            if (result.IsSuccessStatusCode)
            {
                var displayData = result.Content.ReadAsAsync<MovieData>();
                displayData.Wait();
                movie = displayData.Result;
            }

            return View(/*movie*/);
        }

        [HttpPost]
        public ActionResult Edit(MovieData movie)
        {
            HttpClient client = _movieApi.Initial();
            var insertedRecord = client.PutAsJsonAsync<MovieData>("api/movie/", movie);
            insertedRecord.Wait();

            var result = insertedRecord.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Movies");
            }

            return View();
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            HttpClient client = _movieApi.Initial();
            HttpResponseMessage httpResponseMessage = await client.DeleteAsync($"api/movie/{id}");

            return RedirectToAction("Movies");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
