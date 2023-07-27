using LabM.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LabM.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly UserManager<IdentityUser> _userManager;
        //private readonly RoleManager<IdentityRole> _roleManager;
        //,UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            //_userManager = userManager;
            //_roleManager = roleManager;
        }
        //public async Task<IActionResult> CreateRoles()
        //{
        //    var roles = new[] { "Admin", "Recep" };
        //    foreach (var role in roles)
        //    {
        //        var roleExist = await _roleManager.RoleExistsAsync(role);
        //        if(!roleExist)
        //            await _roleManager.CreateAsync(new IdentityRole(role));
                
        //    }
        //    return View("index", "roles added successfuly");
        //}
        //public async Task<IActionResult> AddRoleToUsers() { 
        //    var mohammed = await _userManager.FindByNameAsync("Mohammed@qumc.edu.sa");
        //    await _userManager.AddToRoleAsync(mohammed, "Admin");
        //    var khaled = await _userManager.FindByNameAsync("khaled@qumc.edu.sa");
        //    await _userManager.AddToRoleAsync(khaled, "Recep");
        //    return View("index", "user added successfuly");
        //}

        public IActionResult Index()
        {
            return View();
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