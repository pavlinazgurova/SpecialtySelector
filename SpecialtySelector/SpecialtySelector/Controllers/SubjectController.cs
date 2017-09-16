namespace SpecialtySelector.Controllers
{
    using Microsoft.AspNet.Identity;
    using SpecialtySelector.Data;
    using SpecialtySelector.Models.Subjects;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;

    public class SubjectController : Controller
    {
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            using (var db = new SpecialtySelectorDbContext())
            {
                var teachers = db.Teachers.ToList();
                var specialties = db.Specialties.ToList();
                
                ViewBag.Teachers = teachers;
                ViewBag.Specialties = specialties;

                return View();
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(CreateSubject createSubject)
        {
            if (createSubject != null && ModelState.IsValid)
            {
                using (var db = new SpecialtySelectorDbContext())
                {
                    var adminId = this.User.Identity.GetUserId();

                    var listOfTeachers = new List<Teacher>();
                    var listOfSpecialties = new List<Specialty>();

                    //FIX
                    if (createSubject.Teacher != null)
                    {
                        foreach (var teacher in createSubject.Teacher)
                        {
                            var currentTeacher = db.Teachers.FirstOrDefault(t => t.Id == teacher);
                            listOfTeachers.Add(currentTeacher);
                        }
                    }

                    //FIX
                    if (createSubject.Specialty != null)
                    {
                        foreach (var specialty in createSubject.Specialty)
                        {
                            var currentSpecialty = db.Specialties.FirstOrDefault(s => s.Id == specialty);
                            listOfSpecialties.Add(currentSpecialty);
                        }
                    }
                    
                    var subject = new Subject()
                    {
                        Name = createSubject.Name,
                        IsMandatory = createSubject.IsMandatory,
                        Credits = createSubject.Credits,
                        Course = createSubject.Course,
                        Description = createSubject.Description,
                        Specialties = listOfSpecialties,
                        Teachers = listOfTeachers,
                        AdminId = adminId
                    };

                    db.Subjects.Add(subject);
                    db.SaveChanges();

                    //FIX
                    return RedirectToAction("Details", new { id = subject.Id });
                }
            }

            using (var db = new SpecialtySelectorDbContext())
            {
                var teachers = db.Teachers.ToList();
                var specialties = db.Specialties.ToList();

                ViewBag.Teachers = teachers;
                ViewBag.Specialties = specialties;

                return View(createSubject);
            }            
        }

        public ActionResult AllSubjects()
        {
            using (var db = new SpecialtySelectorDbContext())
            {
                var subjects = db.Subjects
                    .Include(x => x.Teachers)
                    .Include(x => x.Specialties)
                    .Where(sp => sp.DeletedOn.Equals(null))
                    .Select(sp => new AllSubjects()
                    {
                        Id = sp.Id,
                        Name = sp.Name,
                        Credits = sp.Credits,
                        Course = sp.Course,
                        IsMandatory = sp.IsMandatory
                    })
                    .ToList();

                return View(subjects);
            }
        }

        public ActionResult Details(int? id)
        {
            //FIX
            if (id == null)
            {
                return HttpNotFound();
            }

            using (var db = new SpecialtySelectorDbContext())
            {
                var subjects = db.Subjects
                    .Where(sp => sp.Id == id)
                    .Where(sp => sp.DeletedOn.Equals(null))
                    .Select(sp => new SubjectInfo
                    {
                        Id = sp.Id,
                        Name = sp.Name,
                        Description = sp.Description,
                        Specialties = sp.Specialties,
                        Teachers = sp.Teachers,
                        Course = sp.Course,
                        Credits = sp.Credits,
                        IsMandatory = sp.IsMandatory
                    })
                    .FirstOrDefault();

                if (subjects == null)
                {
                    return HttpNotFound();
                }

                return View(subjects);
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
                var subject = db.Subjects.Find(id);

                if (subject != null)
                {
                    subject.DeletedOn = DateTime.Now;
                }

                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult SubjectInfo(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            using (var db = new SpecialtySelectorDbContext())
            {
                var subjects = db.Subjects
                    .Where(s => s.Id == id)
                    .Where(s => s.DeletedOn.Equals(null))
                    .Select(s => new SubjectInfo()
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Description = s.Description,
                        Specialties = s.Specialties,
                        Teachers = s.Teachers,
                        Course = s.Course,
                        Credits = s.Credits,
                        IsMandatory = s.IsMandatory
                    })
                    .ToList();

                if (subjects == null)
                {
                    return HttpNotFound();
                }

                return View(subjects);
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
                var subject = db.Subjects.Find(id);
                var adminId = this.User.Identity.GetUserId();

                if (subject == null)
                {
                    return HttpNotFound();
                }

                var updateSubjectModel = new UpdateSubject
                {
                    AdminId = adminId,
                    Id = subject.Id,
                    Name = subject.Name,
                    Description = subject.Description,
                    Credits = subject.Credits,
                    Course = subject.Course,
                    Teachers = subject.Teachers,
                    Specialties = subject.Specialties,
                    IsMandatory = subject.IsMandatory,
                    DeletedOn = subject.DeletedOn
                };

                return View(updateSubjectModel);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Update(UpdateSubject updateSubject)
        {
            if (ModelState.IsValid && updateSubject != null)
            {
                using (var db = new SpecialtySelectorDbContext())
                {
                    var subjects = db.Subjects.
                        Find(updateSubject.Id);

                    var adminId = this.User.Identity.GetUserId();

                    var listOfTeachers = new List<Teacher>();
                    var listOfSpecialties = new List<Specialty>();

                    if (updateSubject.Teacher != null)
                    {
                        foreach (var teacher in updateSubject.Teacher)
                        {
                            var currentTeacher = db.Teachers.FirstOrDefault(t => t.Id == teacher);
                            listOfTeachers.Add(currentTeacher);
                        }
                    }

                    if (updateSubject.Specialty != null)
                    {
                        foreach (var specialty in updateSubject.Specialty)
                        {
                            var currentSpecialty = db.Specialties.FirstOrDefault(s => s.Id == specialty);
                            listOfSpecialties.Add(currentSpecialty);
                        }
                    }

                    subjects.AdminId = adminId;
                    subjects.Name = updateSubject.Name;
                    subjects.Description = updateSubject.Description;
                    subjects.Course = updateSubject.Course;
                    subjects.Credits = updateSubject.Credits;
                    subjects.IsMandatory = updateSubject.IsMandatory;
                    subjects.DeletedOn = updateSubject.DeletedOn;
                    subjects.Specialties = listOfSpecialties;
                    subjects.Teachers = listOfTeachers;
                    subjects.Id = updateSubject.Id;

                    db.SaveChanges();
                }

                return RedirectToAction("Details", new { id = updateSubject.Id });
            }

            return View(updateSubject);
        }
    }
}