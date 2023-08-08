using Microsoft.AspNetCore.Mvc.Rendering;

namespace LabM.Models
{
    public class StudentCollegeVM
    {
        public Request Request { get; set; }
        public SelectList CollegeSelectList { get; set; }
        public List<DateTime> AvilableDates { get; set;}
    }
}
