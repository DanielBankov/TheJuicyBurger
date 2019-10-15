using JuicyBurger.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace JuicyBurger.Web.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return this.View();
        }

        public async Task<IActionResult> Privacy()
        {
            return this.View();
        }

        public async Task<IActionResult> About()
        {
            return this.View();
        }

        public async Task<IActionResult> Contact()
        {
            return this.View();
        }

        public async Task<IActionResult> _ComingSoon()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            return this.View(new ErrorViewModel {  RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
