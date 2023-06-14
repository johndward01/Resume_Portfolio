using Microsoft.AspNetCore.Mvc;

namespace Resume_Portfolio.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
