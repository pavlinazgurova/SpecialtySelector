namespace SpecialtySelector.Controllers
{
    using Microsoft.AspNet.Identity;
    using SpecialtySelector.Data;
    using SpecialtySelector.Models.Subjects;
    using System.Collections.Generic;
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

                    var subject = new Subject()
                    {
                        Name = createSubject.Name,
                        IsMandatory = createSubject.IsMandatory,
                        Credits = createSubject.Credits,
                        Course = createSubject.Course,
                        Description = createSubject.Description,
                        //SpecialtyId = createSubject.SpecialtyId,
                        //TeacherId = createSubject.TeacherId,
                        AdminId = adminId

                    };

                    if (createSubject.TeacherId != null)
                    {
                        //foreach (var id in createSubject.TeacherId)
                        //{
                        //    var teacher = db.Teachers.Find(id);

                        //    subject.Teachers.Add(teacher);
                        //}
                    }

                    db.Subjects.Add(subject);
                    db.SaveChanges();
                    //var specialties = db.Specialties.ToList();
                    //ViewBag.Specialties = specialties;

                    //var teachers = db.Teachers.ToList();
                    //ViewBag.Teachers = teachers;

                    //return RedirectToAction("Details", new { id = subject.Id });
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(createSubject);
        }
    }
}