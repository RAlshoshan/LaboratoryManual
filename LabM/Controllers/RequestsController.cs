using Microsoft.AspNetCore.Mvc;
using LabM.Models;
using LabM.Data;

namespace LabM.Controllers
{
    public class RequestsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public RequestsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var requests = _context.Request.ToList();
            return View(requests);
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
                return RedirectToAction("Index");
            }
            return View(request);
        }
    }
}
