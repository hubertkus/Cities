using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Cities.Models;

namespace Cities.Controllers;

public class HomeController : Controller
{
    private ICityRepository _repository;

    public HomeController(ICityRepository repository)
    {
        _repository = repository;
    }

    public ViewResult Index() => View(_repository.Cities);

    public ViewResult Create() => View();

    [HttpPost]
    public IActionResult Create(City city)
    {
        _repository.AddCity(city);
        return RedirectToAction("Index", "Home");
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
