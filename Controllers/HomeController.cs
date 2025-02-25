using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;
namespace MyMvcApp.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
    private readonly ILogger<HomeController> _logger;
   

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    [HttpPost]
    public IActionResult Index(string login)
    {
        if(login =="admin")
        {
            HttpContext.Session.SetString("User", login);
            return RedirectToAction("Dashboard");
        }
        else
        {
        ViewBag.Error = "NIE POPRAWNY LOGIN!";
        return View();
        }
    }
    public IActionResult Dashboard()
    {
        if(string.IsNullOrEmpty(HttpContext.Session.GetString("User")))
        {
            return RedirectToAction("Login");
        }
        ViewBag.User = HttpContext.Session.GetString("User");
        return View();

    }
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index");
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
