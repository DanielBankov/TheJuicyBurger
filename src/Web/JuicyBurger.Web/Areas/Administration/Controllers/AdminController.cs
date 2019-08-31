using JuicyBurger.Services.GlobalConstants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JuicyBurger.Web.Areas.Administration.Controllers
{

    [Authorize(Roles = ServicesGlobalConstants.Admin )]
    [Area(ServicesGlobalConstants.Administration)]
    public class AdminController : Controller
    {
    }
}