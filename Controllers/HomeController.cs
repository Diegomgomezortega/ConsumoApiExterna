using ConsumoApiExterna.Models;
using ConsumoApiExterna.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsumoApiExterna.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        HttpClient httpClient = new HttpClient();

        public async Task<IActionResult> Index()
        {

            HttpService httpService = new HttpService(httpClient);
            List<User> usersList = new List<User>();
            var respuesta = await httpService.Get<List<User>>("https://jsonplaceholder.typicode.com/todos?userId=1");
            usersList = respuesta.Respuesta;            
            return View(usersList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
