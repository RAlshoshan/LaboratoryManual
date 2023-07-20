using Microsoft.AspNetCore.Mvc;
using LabM.Models;
using LabM.Data;

namespace LabM.Controllers
{
    public class ManagementsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ManagementsController(ApplicationDbContext context)
        {
            _context = context;
        }
        /*
        public IActionResult Index()
        {
            return View();
        }
        */
        // GET
        public IActionResult Edit(int? id)
        {
            var limitationCountResult = _context.Management.Where(x => x.Name == "limitationDays").FirstOrDefault();
            return View(limitationCountResult==null?0:limitationCountResult.Value);
        }
        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int limitationDays)
        {
            var limitationDaysObject = _context.Management.Where(x => x.Name == "limitationDays").FirstOrDefault();
            if (limitationDaysObject==null) {
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
            return View(limitationDays);
        }
    }
}
