using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Flower.UI.Models;
using Flower.UI.Filter;
using Flower.UI.Exception;

namespace Flower.UI.Controllers;

[ServiceFilter(typeof(AuthFilter))]

public class HomeController : Controller
{
 
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Error()
    {
        return View();
    }


}


