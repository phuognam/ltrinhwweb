using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using lab1.Models;

namespace lab1.Controllers
{
    public class StudentController : Controller
    {
        private List<Student> listStudents = new List<Student>();
        public StudentController() {
            listStudents = new List<Student>()
            {
                new Student() { Id = 101, Name = "Hải Nam", Branch = Branch.IT,
                Gender = Gender.Male, IsRegular = true,
                Address = "A1-2018", Email = "nam@g.com"},
                new Student() { Id = 102, Name = "Hải Tú", Branch = Branch.BE,
                Gender = Gender.Male, IsRegular = true,
                Address = "A1-2019", Email = "tu@g.com"},
                new Student() { Id = 103, Name = "Phong Nam", Branch = Branch.CE,
                Gender = Gender.Female, IsRegular = false,
                Address = "A1-2020", Email = "pnam@g.com"},
                new Student() { Id = 104, Name = "Xuân Mai", Branch = Branch.EE,
                Gender = Gender.Male, IsRegular = false,
                Address = "A1-2021", Email = "mai@g.com"},
            };
        }
        public IActionResult Index()
        {
            return View(listStudents);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.AllGenders =  Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
            ViewBag.AllBranches = new List<SelectListItem>()
            {
                new SelectListItem { Text = "IT", Value = "1"},
                new SelectListItem { Text = "BE", Value = "2"},
                new SelectListItem { Text = "CE", Value = "3"},
                new SelectListItem { Text = "EE", Value = "4"}
            };
        return View();
        }
        [HttpPost]
        public IActionResult Create(Student s)
        {
            if (ModelState.IsValid)
            {   
                s.Id = listStudents.Last<Student>().Id + 1;
                listStudents.Add(s);
                return View("Index", listStudents);
            }
            ViewBag.AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
            ViewBag.AllGenders = new List<SelectListItem>()
            {
                new SelectListItem { Text = "IT", Value="1"},
                new SelectListItem { Text = "BE", Value="2"},
                new SelectListItem { Text = "CE", Value="3"},
                new SelectListItem { Text = "EE", Value="4"}
            };
            return View();
        }
    }
}
