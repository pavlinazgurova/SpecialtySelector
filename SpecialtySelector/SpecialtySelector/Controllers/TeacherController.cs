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
            if (ModelState.IsValid && createTeacher != null)
            {
                using (var db = new SpecialtySelectorDbContext())
                {
                    var adminId = this.User.Identity.GetUserId();
                    var subnew = new List<Subject>();

                    if (createTeacher.Subject != null)
                    {
                        foreach (var kvp in createTeacher.Subject)
                        {
                            var asd = db.Subjects.FirstOrDefault(x => x.Id == kvp);
                            subnew.Add(asd);
                        }
                    }
                   
                    var teacher = new Teacher()
                    {
                        
                        FirstName =createTeacher.FirstName,
                        SecondName = createTeacher.SecondName,
                        AdminId = adminId,
                        LastName = createTeacher.LastName,
                        AcademicTitle = createTeacher.AcademicTitle,
                        Degree = createTeacher.Degree,
                        TeacherInfo = createTeacher.TeacherInfo,                        
                        Subjects = subnew
                    };

                    db.Teachers.Add(teacher);
                    db.SaveChanges();
                   
                    return RedirectToAction("Details", new { id = teacher.Id });
                }
            }

            using (var db = new SpecialtySelectorDbContext())
            {
                var subjects = db.Subjects.ToList();

                ViewBag.Subjects = subjects;

                return View(createTeacher);
            }
        }

        public ActionResult TeacherInfo(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

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
                        Subjects = t.Subjects
                    })
                    .ToList();

                if (teachers == null)
                {
                    return HttpNotFound();
                }

                return View(teachers);
            }
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

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
                        Subjects = t.Subjects
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
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

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

        [Authorize]
        [HttpGet]
        public ActionResult Update(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            using (var db = new SpecialtySelectorDbContext())
            {                
                var teacher = db.Teachers.Find(id);
                var adminId = this.User.Identity.GetUserId();

                if (teacher == null)
                {
                    return HttpNotFound();
                }

                var teacherViewModel = new UpdateTeacher
                {
                    Id = teacher.Id,
                    FirstName = teacher.FirstName,
                    SecondName = teacher.SecondName,
                    LastName = teacher.LastName,
                    TeacherInfo = teacher.TeacherInfo,
                    Degree = teacher.Degree,
                    AcademicTitle = teacher.AcademicTitle,
                    AdminId = adminId,
                    Subjects = teacher.Subjects
                };

                return View(teacherViewModel);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Update(UpdateTeacher updateTeacher)
        {
            if (ModelState.IsValid && updateTeacher != null)
            {
                using (var db = new SpecialtySelectorDbContext())
                {
                    var teachers = db.Teachers.
                        Find(updateTeacher.Id);
                    var adminId = this.User.Identity.GetUserId();
                    var subnew = new List<Subject>();

                    if (updateTeacher.Subject != null)
                    {
                        foreach (var kvp in updateTeacher.Subject)
                        {
                            var asd = db.Subjects.FirstOrDefault(x => x.Id == kvp);
                            subnew.Add(asd);
                        }
                    }
                    
                    teachers.AdminId = adminId;
                    teachers.FirstName = updateTeacher.FirstName;
                    teachers.SecondName = updateTeacher.SecondName;
                    teachers.LastName = updateTeacher.LastName;
                    teachers.TeacherInfo = updateTeacher.TeacherInfo;
                    teachers.Degree = updateTeacher.Degree;
                    teachers.AcademicTitle = updateTeacher.AcademicTitle;
                    teachers.FiredOn = updateTeacher.FiredOn;
                    teachers.Subjects = subnew;

                    db.SaveChanges();
                }

                return RedirectToAction("Details", new { id = updateTeacher.Id });
            }

            return View(updateTeacher);
        }

        public ActionResult AllTeachers()
        {
            using (var db = new SpecialtySelectorDbContext())
            {
                var teachers = db.Teachers
                    .Where(t => t.FiredOn.Equals(null))
                    .Select(t => new AllTeachers()
                    {
                        Id = t.Id,
                        FirstName = t.FirstName,
                        SecondName = t.SecondName,
                        LastName = t.LastName,
                        TeacherInformation = t.TeacherInfo,
                        Degree = t.Degree,
                        AcademicTitle = t.AcademicTitle,
                        Subjects = t.Subjects
                    })
                    .ToList();

                if (teachers == null)
                {
                    return HttpNotFound();
                }

                return View(teachers);
            }
        }
    }
}