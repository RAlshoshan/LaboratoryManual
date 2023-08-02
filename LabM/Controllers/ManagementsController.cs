using Microsoft.AspNetCore.Mvc;
using LabM.Models;
using LabM.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace LabM.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ManagementsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        // private readonly RoleManager<IdentityRole> _roleManager;
        public ManagementsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        // GET
        public IActionResult Edit(int? id)
        {
            var limitationCountResult = _context.Management.Where(x => x.Name == "limitationDays").FirstOrDefault();
            return View(limitationCountResult == null ? 0 : limitationCountResult.Value);
        }
        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int limitationDays)
        {
            var limitationDaysObject = _context.Management.Where(x => x.Name == "limitationDays").FirstOrDefault();
            if (limitationDaysObject == null)
            {
                limitationDaysObject = new Management();
                limitationDaysObject.Name = "limitationDays";
                limitationDaysObject.Value = limitationDays;
                _context.Management.Add(limitationDaysObject);
            }
            else
            {
                limitationDaysObject.Value = limitationDays;
            }
            _context.SaveChanges();
            //return RedirectToAction(nameof(Index),"Requests" );
            return View(limitationDays);
        }
        public async Task<IActionResult> AddUserToRole(string email, string roleSelected)
        {
            try
            {
                var userName = await _userManager.FindByEmailAsync(email);
                await _userManager.AddToRoleAsync(userName, roleSelected);
                return View("AddUserToRole", "Added successfuly");
            }
            catch (Exception ex)
            {
                return View("AddUserToRole", "Added Not valiation");
            }
        }
    }
}
