
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Students.Models
{
    public class StudentController : Controller
    {
        private StudentContext context;
        public StudentController(StudentContext context)
        {
            this.context = context;
        }


        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetStudents()
        {

            return View(context.Students.AsParallel().ToList());
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return new BadRequestObjectResult("Cannot find student without id!");


            Student student = context.Students.FirstOrDefault(s => s.Id == id);
            if (student == null)
                return new BadRequestObjectResult("Cannot find student with this id!");


            return View(student);
        }



        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            Student student = new Student { Id = id.Value};

            context.Entry(student).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;

            await context.SaveChangesAsync();


            return RedirectToActionPermanent(nameof(Index));
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(StudentViewModel studentModel)
        {
            ///splitting names
            var fullName = studentModel.FullName.Split(' ');
            var mothersFullName = studentModel.MothersFullName.Split(' ');
            var fathersFullName = studentModel.FathersFullName.Split(' ');


            ///Validating names
            if (fullName.Length < 3 || mothersFullName.Length < 2 || fathersFullName.Length < 2)
                return new BadRequestObjectResult("Cannot add unexisting information!");

            ///Checking existance
            if (context.Students.FirstOrDefault(o => o.FirstName == fullName[1] && o.LastName == fullName[0]) != null)
                return new BadRequestObjectResult("Cannot add two similar students");

            


            Student student = new Student
            {
                LastName = fullName[0],
                FirstName = fullName[1],
                Patronymic = fullName[2],
                Age = studentModel.Age,

                Mother = new Parent { FirstName = mothersFullName[0], LastName = mothersFullName[1], Patronymic = mothersFullName.Length < 3 ? null: mothersFullName[2] },
                Father = new Parent { FirstName = fathersFullName[0], LastName = fathersFullName[1], Patronymic = fathersFullName.Length < 3 ? null: fathersFullName[2] },
                Address = studentModel.Address       
            };

            await context.Students.AddAsync(student);
            await context.SaveChangesAsync();


            return RedirectToActionPermanent(nameof(Index));
        }
        
        public IActionResult GetStudent(int? id)
        {
            if(id == null)
                return new BadRequestObjectResult("Cannot find student with that id!");
            var student = context.Students.FirstOrDefault(s => s.Id == id);
            if (student == null)
                return new BadRequestObjectResult("Cannot find student with that id!");

            return View(student);
        }



        [Route("Error")]
        public IActionResult Error() 
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerPathFeature>();


            ViewBag.ExceptionName = exception.Error.Message;
            ViewBag.Path = exception.Path;

            return View("~/Views/Shared/Error.cshtml");
        }
    }
}
