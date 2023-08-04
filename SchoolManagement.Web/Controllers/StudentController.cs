using SchoolManagement.Models;
using SchoolManagement.Services;
using SchoolManagement.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagement.Web.Controllers
{
    public class StudentController : Controller
    {
        private IStudentService _studentService;
        private IClassService _classService;

        public StudentController(IStudentService studentService, IClassService classService)
        {
            _studentService = studentService;
            _classService = classService;
        }

        public ActionResult Students()
        {
            List<StudentDto> model = _studentService.GetStudents();
            return View(model);
        }

        public ActionResult Add()
        {
            StudentDto Model = new StudentDto();
            Model.Classes = _classService.GetClasses();

            return View(Model);
        }

        [HttpPost]
        public ActionResult Add(StudentDto model)
        {
            model.Classes = _classService.GetClasses();
            if (ModelState.IsValid)
            {
                model.FirstName = model.FirstName.Trim();
                model.LastName = model.LastName.Trim();

                bool result = _studentService.Add(model);

                if (result)
                {
                    ModelState.Clear();
                    return RedirectToAction("Students");
                }
                else
                {
                    ModelState.AddModelError("", "Something went wrong. Try again.");
                }
            }

            return View(model);
        }

        public ActionResult Update(int studentId)
        {
            StudentDto Model = _studentService.GetStudentById(studentId);

            if (Model != null)
            {
                Model.Classes = _classService.GetClasses();
                return View(Model);
            }
            else
            {
                return View("StudentNotFound");
            }
        }

        [HttpPost]
        public ActionResult Update(StudentDto model)
        {
            model.Classes = _classService.GetClasses();
            if (ModelState.IsValid)
            {
                model.FirstName = model.FirstName.Trim();
                model.LastName = model.LastName.Trim();

                bool result = _studentService.Update(model);

                if (result)
                {
                    ViewBag.IsSuccess = true;
                    ViewBag.Message = "Updated Successfully.";
                }
                else
                {
                    ModelState.AddModelError("", "Something went wrong. Try again.");
                }
            }

            return View(model);
        }

        public ActionResult Delete(int studentId)
        {
            _studentService.Delete(studentId);

            return RedirectToAction("Students");
        }
    }
}