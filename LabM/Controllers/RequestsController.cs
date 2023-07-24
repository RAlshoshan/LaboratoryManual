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
            return View();
        }
        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NationalOrResidenceId,UniversityNumber,StudentsStatus,College,FirstNameEnglish,FatherNameEnglish,GrandFatherNameEnglish,FamilyNameEnglish,FirstNameArabic,FatherNameArabic,GrandFatherNameArabic,FamilyNameArabic,Email,PhoneNo,BirthDate,MedicalFileNo,Date")] Request request)
        {
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
