using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers;

public class AuthorController : Controller
{
    // GET
    public IActionResult Index()
    {
        
        return View();
    }
}