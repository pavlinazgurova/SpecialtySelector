namespace SpecialtySelector.Controllers
{
    using Microsoft.AspNet.Identity;
    using SpecialtySelector.Data;
    using SpecialtySelector.Models.Teachers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class TeacherController : Controller
    {
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            using (var db = new SpecialtySelectorDbContext())
            {
                var subjects = db.Subjects.ToList();
                
                ViewBag.Subjects = subjects;

                return View();
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(CreateTeacher createTeacher)
        {
            if (ModelState.IsValid)
            {
                using (var db = new SpecialtySelectorDbContext())
                {
                    var adminId = this.User.Identity.GetUserId();
                    var subnew = new List<Subject>();

                    foreach (var kvp in createTeacher.Subject)
                    {
                        var asd = db.Subjects.FirstOrDefault(x => x.Id == kvp);
                        subnew.Add(asd);
                    }
                    var teacher = new Teacher()
                    {
                        
                        FirstName =createTeacher.FirstName,
                        AdminId = adminId,
                        LastName = createTeacher.LastName,
                        AcademicTitle = createTeacher.AcademicTitle,
                        Degree = createTeacher.Degree,
                        Subjects = subnew
                    };

                    db.Teachers.Add(teacher);
                    db.SaveChanges();
                   
                    return RedirectToAction("Details", new { id = teacher.Id });
                }
            }

            return View(createTeacher);
        }

        public ActionResult TeacherInfo(int id)
        {
            using (var db = new SpecialtySelectorDbContext())
            {
                var teachers = db.Teachers
                    .Where(t => t.Id == id)
                    .Where(t => t.FiredOn.Equals(null))
                    .Select(t => new TeacherInfo()
                    {
                        Id = t.Id,
                        Degree = t.Degree,
                        AcademicTitle = t.AcademicTitle,
                        FirstName = t.FirstName,
                        SecondName = t.SecondName,
                        LastName = t.LastName,
                        TeacherInformation = t.TeacherInfo,
                        //SubjectId = t.SubjectId
                    })
                    .ToList();

                return View(teachers);
            }
        }

        public ActionResult Details(int id)
        {
            using (var db = new SpecialtySelectorDbContext())
            {
                var teachers = db.Teachers.
                    Where(t => t.Id == id).
                    Where(t => t.FiredOn.Equals(null)).
                    Select(t => new TeacherInfo
                    {
                        Id = t.Id,
                        Degree = t.Degree,
                        AcademicTitle = t.AcademicTitle,
                        FirstName = t.FirstName,
                        SecondName = t.SecondName,
                        LastName = t.LastName,
                        TeacherInformation = t.TeacherInfo,
                        //SubjectId = t.SubjectId
                    }).
                    FirstOrDefault();

                if (teachers == null)
                {
                    return HttpNotFound();
                }

                return View(teachers);
            }
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            using (var db = new SpecialtySelectorDbContext())
            {
                var teacher = db.Teachers.Find(id);

                if (teacher != null)
                {
                    teacher.FiredOn = DateTime.Now;
                }

                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
        }
    }
}