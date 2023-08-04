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
    public class ClassController : Controller
    {
        private IClassService _classService;

        public ClassController(IClassService classService)
        {
            _classService = classService;
        }

        public ActionResult Classes()
        {
            List<ClassDto> model = _classService.GetClasses();

            return View(model);
        }

        public ActionResult Delete(int classId)
        {
            _classService.DeleteClass(classId);

            return RedirectToAction("Classes");
        }

        public ActionResult AddClass()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddClass(ClassDto model)
        {
            if (string.IsNullOrWhiteSpace(model.ClassName))
            {
                ModelState.AddModelError("", "Class Name is mandatory.");

                return View(model);
            }

            model.ClassName = model.ClassName.Trim();

            bool isExist = _classService.CheckClassNameExist(model.ClassId, model.ClassName);

            if (isExist)
            {
                ModelState.AddModelError("", "Class Name already exist.");

                return View(model);
            }

            bool result = _classService.AddClass(model);

            if (result)
            {
                ModelState.Clear();
                return RedirectToAction("Classes");
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong. Try again.");
            }

            return View(model);
        }

        public ActionResult Update(int classId)
        {
            ClassDto model = _classService.GetClassById(classId);

            if (model != null)
            {
                return View(model);
            }
            else
            {
                return View("ClassNotFound");
            }
        }

        [HttpPost]
        public ActionResult Update(ClassDto model)
        {
            if (string.IsNullOrWhiteSpace(model.ClassName))
            {
                ModelState.AddModelError("", "Class Name is mandatory.");

                return View(model);
            }

            model.ClassName = model.ClassName.Trim();

            bool isExist = _classService.CheckClassNameExist(model.ClassId, model.ClassName);

            if (isExist)
            {
                ModelState.AddModelError("", "Class Name already exist.");

                return View(model);
            }

            _classService.UpdateClass(model);

            return RedirectToAction("Classes");
        }
    }
}