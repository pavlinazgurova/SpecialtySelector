namespace SpecialtySelector.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNet.Identity;
    using SpecialtySelector.Data;
    using SpecialtySelector.Models.Specialties;
    using System.Linq;
    using System.Web.Mvc;
    using System.Data.Entity;
    using System;

    public class SpecialtyController : Controller
    {
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            using (var db = new SpecialtySelectorDbContext())
            {
                var subDepartments = db.SubDepartments.ToList();

                ViewBag.SubDepartments = subDepartments;

                return View();
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(CreateSpecialty createSpecialty)
        {
            if (this.ModelState.IsValid)
            {
                var adminId = this.User.Identity.GetUserId();

                var specialty = new Specialty()
                {
                    Name = createSpecialty.Name,
                    Description = createSpecialty.Description,
                    Eqd = createSpecialty.Eqd,
                    FormOfEducation = createSpecialty.FormOfEducation,
                    SubDepartmentId = createSpecialty.SubDepartmentId,
                    AdminId = adminId
                };

                using (var db = new SpecialtySelectorDbContext())
                {
                    db.Specialties.Add(specialty);
                    db.SaveChanges();

                    var subDepartments = db.SubDepartments.ToList();
                    ViewBag.SubDepartments = subDepartments;

                    return RedirectToAction("Details", new { id = specialty.Id });
                }
            }

            using (var db = new SpecialtySelectorDbContext())
            {
                var subDepartments = db.SubDepartments.ToList();

                ViewBag.SubDepartments = subDepartments;

                return View(createSpecialty);
            }
        }

        public ActionResult SpecialtyInfo(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            using (var db = new SpecialtySelectorDbContext())
            {
                var specialties = db.Specialties
                    .Where(sb => sb.SubDepartmentId == id)
                    .Where(sb => sb.DeletedOn.Equals(null))
                    .Select(sb => new SpecialtyInfo()
                    {
                        Id = sb.Id,
                        Name = sb.Name,
                        Description = sb.Description,
                        Eqd = sb.Eqd,
                        FormOfEducation = sb.FormOfEducation,
                        SubDepartmentId = sb.SubDepartmentId
                    })
                    .ToList();

                if (specialties == null)
                {
                    return HttpNotFound();
                }

                return View(specialties);
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
                var specialties = db.Specialties
                    .Where(sp => sp.Id == id)
                    .Where(sp => sp.DeletedOn.Equals(null))
                    .Select(sp => new SpecialtyInfo
                    {
                        Id = sp.Id,
                        Name = sp.Name,
                        Description = sp.Description,
                        Eqd = sp.Eqd,
                        FormOfEducation = sp.FormOfEducation,
                        SubDepartmentId = sp.SubDepartmentId
                    })
                    .FirstOrDefault();
               
                if (specialties == null)
                {
                    return HttpNotFound();
                }

                return View(specialties);
            }
        }

        public ActionResult AllSpecialties()
        {
            using (var db = new SpecialtySelectorDbContext())
            {
                var specialties = db.Specialties
                    .Include(x => x.SubDepartment.Name)
                    .Where(sp => sp.DeletedOn.Equals(null))
                    .Select(sp => new AllSpecialties()
                    {
                        Id = sp.Id,
                        Name = sp.Name,
                        Description = sp.Description,
                        Eqd = sp.Eqd,
                        FormOfEducation = sp.FormOfEducation,
                        SubDepartmentId = sp.SubDepartmentId,
                        SubDepartmentName = sp.SubDepartment.Name
                    })
                    .ToList();

                if (specialties == null)
                {
                    return HttpNotFound();
                }

                return View(specialties);
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
                Specialty specialty = db.Specialties.FirstOrDefault(s => s.Id == id);

                if (specialty != null)
                {
                    specialty.DeletedOn = DateTime.Now;
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
                var specialty = db.Specialties.Find(id);

                var specialtyViewModel = new UpdateSpecialty
                {
                    Id = specialty.Id,
                    Name = specialty.Name,
                    Description = specialty.Description,
                    Eqd = specialty.Eqd,
                    FormOfEducation = specialty.FormOfEducation,
                    AdminId = specialty.AdminId,
                    SubDepartmentId = specialty.SubDepartmentId
                };

                return View(specialtyViewModel);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Update(UpdateSpecialty updateSpecialty)
        {
            if (ModelState.IsValid && updateSpecialty != null)
            {
                using (var db = new SpecialtySelectorDbContext())
                {
                    var specialty = db.Specialties.
                        Find(updateSpecialty.Id);

                    specialty.Name = updateSpecialty.Name;
                    specialty.Description = updateSpecialty.Description;
                    specialty.Eqd = updateSpecialty.Eqd;
                    specialty.FormOfEducation = updateSpecialty.FormOfEducation;
                    specialty.DeletedOn = updateSpecialty.DeletedOn;
                    specialty.SubDepartmentId = updateSpecialty.SubDepartmentId;

                    db.SaveChanges();
                }

                return RedirectToAction("Details", new { id = updateSpecialty.Id });
            }

            return View(updateSpecialty);
        }
    }
}