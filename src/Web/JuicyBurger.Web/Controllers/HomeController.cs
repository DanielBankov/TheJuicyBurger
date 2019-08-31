using JuicyBurger.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace JuicyBurger.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        public IActionResult About()
        {
            return this.View();
        }

        public IActionResult Contact()
        {
            return this.View();
        }

        public IActionResult _ComingSoon()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel {  RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
