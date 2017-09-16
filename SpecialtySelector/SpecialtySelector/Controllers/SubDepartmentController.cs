using Microsoft.AspNet.Identity;
using SpecialtySelector.Data;
using SpecialtySelector.Models.SubDepartment;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace SpecialtySelector.Controllers
{
    public class SubDepartmentController : Controller
    {
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            using (var db = new SpecialtySelectorDbContext())
            {
                var departments = db.Departments.ToList();

                ViewBag.Departments = departments;

                return View();
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(CreateSubDepartment subDepartmentModel)
        {
            if (this.ModelState.IsValid && subDepartmentModel.Description != null && subDepartmentModel.Name != null)
            {
                var adminId = this.User.Identity.GetUserId();

                var subDepartment = new SubDepartment()
                {
                    Name = subDepartmentModel.Name,
                    Description = subDepartmentModel.Description,
                    DepartmentId = subDepartmentModel.DepartmentId,
                    AdminId = adminId
                };

                using (var db = new SpecialtySelectorDbContext())
                {
                    db.SubDepartments.Add(subDepartment);
                    db.SaveChanges();

                    return RedirectToAction("Details", new { id = subDepartment.Id });
                }
            }

            using (var db = new SpecialtySelectorDbContext())
            {
                var departments = db.Departments.ToList();

                ViewBag.Departments = departments;

                return View(subDepartmentModel);
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
                var subDepartment = db.SubDepartments
                    .Where(d => d.DeletedOn.Equals(null))
                    .Where(d => d.Id == id)
                    .Select(d => new SubDepartmentInfo()
                    {
                        Id = d.Id,
                        Name = d.Name,
                        Description = d.Description
                    })
                    .FirstOrDefault();

                if (subDepartment == null)
                {
                    return HttpNotFound();
                }

                return View(subDepartment);
            }
        }

        public ActionResult SubDepartmentInfo(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            using (var db = new SpecialtySelectorDbContext())
            {
                var subDepartments = db.SubDepartments
                    .Where(sb => sb.DepartmentId == id)
                    .Where(sb => sb.DeletedOn.Equals(null))
                    .Select(sb => new SubDepartmentInfo()
                    {
                        Id = sb.Id,
                        Name = sb.Name,
                        Description = sb.Description
                    })
                    .ToList();

                if (subDepartments == null)
                {
                    return HttpNotFound();
                }

                return View(subDepartments);
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
                SubDepartment subDepartment = db.SubDepartments.FirstOrDefault(x => x.Id == id);

                if (subDepartment != null)
                {
                    subDepartment.DeletedOn = DateTime.Now;
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
                var subDepartment = db.SubDepartments
                    .Find(id);

                if (subDepartment == null)
                {
                    return HttpNotFound();
                }

                var subDepartmentViewModel = new UpdateSubDepartment
                {
                    Id = subDepartment.Id,
                    Name = subDepartment.Name,
                    Description = subDepartment.Description,
                    AdminId = subDepartment.AdminId,
                    DepartmentId = subDepartment.DepartmentId
                };

                return View(subDepartmentViewModel);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Update(UpdateSubDepartment updateSubDepartment)
        {
            if (ModelState.IsValid)
            {
                using (var db = new SpecialtySelectorDbContext())
                {
                    var subDepartment = db.SubDepartments
                        .Find(updateSubDepartment.Id);

                    subDepartment.Name = updateSubDepartment.Name;
                    subDepartment.Description = updateSubDepartment.Description;
                    subDepartment.DeletedOn = updateSubDepartment.DeletedOn;
                    subDepartment.DepartmentId = updateSubDepartment.DepartmentId;
                    db.SaveChanges();
                }

                return RedirectToAction("Details", new { id = updateSubDepartment.Id });
            }

            return View(updateSubDepartment);
        }

        public ActionResult AllSubDepartments()
        {
            using (var db = new SpecialtySelectorDbContext())
            {
                var subDepartments = db.SubDepartments
                    .Include(x => x.Department.Name)
                    .Where(x => x.DeletedOn.Equals(null))
                    .Select(sp => new AllSubDepartments()
                    {
                        Id = sp.Id,
                        Name = sp.Name,
                        Description = sp.Description,
                        DepartmentId = sp.DepartmentId,
                        DepatrmentName = sp.Department.Name
                    })
                    .ToList();

                if (subDepartments == null)
                {
                    return HttpNotFound();
                }

                return View(subDepartments);
            }
        }
    }
}