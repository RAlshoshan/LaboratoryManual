using Microsoft.AspNetCore.Mvc;
using LabM.Models;
using LabM.Data;
using Microsoft.EntityFrameworkCore;

namespace LabM.Controllers
{
    public class RequestsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public RequestsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task< IActionResult> Index(string? college, string? studentsStatus)
        {
            if (!string.IsNullOrEmpty(college) && !string.IsNullOrEmpty(studentsStatus))
            {
                return View(await _context.Request.Where(r => r.College == college && r.StudentsStatus == studentsStatus).ToListAsync());
            }
            else if (!string.IsNullOrEmpty(college) || !string.IsNullOrEmpty(studentsStatus))
            {
                return View(await _context.Request.Where(r => r.College == college || r.StudentsStatus == studentsStatus).ToListAsync());
            }
            else
            {
                return _context.Request != null ?
                            View(await _context.Request.ToListAsync()) :
                            Problem("Entity set 'ApplicationDbContext.Requests'  is null.");
            }
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
                if(date.DayOfWeek.ToString() == "Friday" ||  date.DayOfWeek.ToString() == "Saturday")
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
            if(requestsCount >= limitDays)
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
