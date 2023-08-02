using Microsoft.AspNetCore.Mvc;
using LabM.Models;
using LabM.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Net.Mail;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace LabM.Controllers
{
    public class RequestsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public RequestsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [Authorize(Roles = "Admin, Recep")]

        //public async Task< IActionResult> Index(string? college, string? studentsStatus)
        //{
        //        //var userName = await _userManager.FindByEmailAsync("khaled@qumc.edu.sa");
        //        //ViewData["Checkk"] = "False";
        //        //if (await _userManager.IsInRoleAsync(userName, "Admin"))
        //        //{
        //        //    ViewData["Checkk"] = "True";
        //        //}
        //    if (!string.IsNullOrEmpty(college) && !string.IsNullOrEmpty(studentsStatus))
        //    {
        //        return View(await _context.Request.Where(r => r.College == college && r.StudentsStatus == studentsStatus).ToListAsync());
        //    }
        //    else if (!string.IsNullOrEmpty(college) || !string.IsNullOrEmpty(studentsStatus))
        //    {
        //        return View(await _context.Request.Where(r => r.College == college || r.StudentsStatus == studentsStatus).ToListAsync());
        //    }
        //    else
        //    {
        //        return _context.Request != null ?
        //                    View(await _context.Request.ToListAsync()) :
        //                    Problem("Entity set 'ApplicationDbContext.Requests'  is null.");
        //    }
        //}
        public async Task<IActionResult> Index(string searchSelected, string searchString)
        {
            if (_context.Request == null)
            {
                return NotFound("Request Is Null");
            }
            var request = from m in _context.Request
                          select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                if (searchSelected == "College")
                {
                    request = request.Where(x => x.College.Contains(searchString));
                }
                else if (searchSelected == "StudentsStatus")
                {
                    request = request.Where(x => x.StudentsStatus.Contains(searchString));
                }
            }
            return View(await request.ToListAsync());

        }
        //GET
        public IActionResult Create()
        {
            var managemnt = _context.Management.Where(x => x.Name == "limitationDays").FirstOrDefault();
            if (managemnt is null)
            {
                ViewBag.ErrorMessage = "You Need To Set The Limit In Management";
                return View();
            }
            var limitDays = managemnt.Value;
            var dateTo = DateTime.Now.AddDays(30);
            List<DateTime> avilableDates = new List<DateTime>();
            for (var date = DateTime.Now; date <= dateTo; date = date.AddDays(1))
            {
                if (date.DayOfWeek.ToString() == "Friday" || date.DayOfWeek.ToString() == "Saturday")
                {
                    continue;
                }
                var requestsCount = _context.Request.Where(x => x.Date.Date == date.Date).Count();
                if (requestsCount >= limitDays)
                {
                    continue;
                }
                avilableDates.Add(date);
            }
            ViewBag.AvilableDates = avilableDates;
            return View();
        }
        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NationalOrResidenceId,UniversityNumber,StudentsStatus,College,FirstNameEnglish,FatherNameEnglish,GrandFatherNameEnglish,FamilyNameEnglish,FirstNameArabic,FatherNameArabic,GrandFatherNameArabic,FamilyNameArabic,Email,PhoneNo,BirthDate,MedicalFileNo,Date")] Request request)
        {
            var managemnt = _context.Management.Where(x => x.Name == "limitationDays").FirstOrDefault();
            if (managemnt is null)
            {
                ViewBag.ErrorMessage = "You Need To Set The Limit In Management";
                return View();
            }
            var limitDays = managemnt.Value;
            var requestsCount = _context.Request.Where(x => x.Date == request.Date).Count();
            if (requestsCount >= limitDays)
            {
                ViewBag.ErrorMessage = "Sorry, The Limit Of Request For This Day Is Reached";
                return View();
            }
            if (ModelState.IsValid)
            {
                _context.Add(request);
                await _context.SaveChangesAsync();
                return RedirectToAction("Message");
                //return View("Message");
            }
            return View(request);
        }
        public IActionResult Message()
        {
            return View();
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Request == null)
            {
                return NotFound();
            }

            var request = await _context.Request.FirstOrDefaultAsync(m => m.Id == id);
            if (request == null)
            {
                return NotFound();
            }
            return View(request);
        }
    }
}
