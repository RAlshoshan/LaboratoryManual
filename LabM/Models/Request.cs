namespace LabM.Models
{
    public class Request
    {
        public int Id { get; set; }
        public int NationalOrResidenceId { get; set; }
        public int UniversityNumber { get; set; }
        public string StudentsStatus { get; set; }
        public string College { get; set; }
        public string FirstNameEnglish { get; set; }
        public string FatherNameEnglish { get; set; }
        public string GrandFatherNameEnglish { get; set; }
        public string FamilyNameEnglish { get; set; }
        public string FirstNameArabic { get; set; }
        public string FatherNameArabic { get; set; }
        public string GrandFatherNameArabic { get; set; }
        public string FamilyNameArabic { get; set; }
        public string Email { get; set; }
        public int PhoneNo { get; set; }
        public DateTime BirthDate { get; set; }
        public int MedicalFileNo { get; set; }
        public DateTime Date { get; set; }
    }
}
