using Microsoft.AspNet.Identity;
using SpecialtySelector.Data;
using SpecialtySelector.Models.Departments;
using System;
using System.Linq;
using System.Web.Mvc;

namespace SpecialtySelector.Controllers
{
    public class DepartmentController : Controller
    {
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(CreateDepartment departmentModel)
        {
            if (this.ModelState.IsValid && departmentModel.Description != null && departmentModel.Name != null)
            {
                var adminId = this.User.Identity.GetUserId();

                var department = new Department
                {
                    Name = departmentModel.Name,
                    Description = departmentModel.Description,
                    AdminId = adminId
                };

                using (var db = new SpecialtySelectorDbContext())
                {
                    db.Departments.Add(department);
                    db.SaveChanges();

                    return RedirectToAction("Details", new { id = department.Id });
                }
            }

            return View(departmentModel);
        }

        public ActionResult Details(int id)
        {
            using (var db = new SpecialtySelectorDbContext())
            {
                var department = db.Departments
                    .Where(d => d.DeletedOn.Equals(null))
                    .Where(d => d.Id == id)
                    .Select(d => new DepartmentDetailsModel()
                    {
                        Name = d.Name,
                        Description = d.Description
                    })
                    .FirstOrDefault();

                return View(department);
            }
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            using (var db = new SpecialtySelectorDbContext())
            {
                Department department = db.Departments.FirstOrDefault(x => x.Id == id);

                if (department != null)
                {
                    department.DeletedOn = DateTime.Now;
                }

                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult Update(int id)
        {
            using (var db = new SpecialtySelectorDbContext())
            {
                var department = db.Departments
                    .Find(id);

                var departmentViewModel = new UpdateDepartment
                {
                    Id = department.Id,
                    Name = department.Name,
                    Description = department.Description,
                    AdminId = department.AdminId
                };

                return View(departmentViewModel);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Update(UpdateDepartment updateDepartment)
        {
            if (ModelState.IsValid)
            {
                using (var db = new SpecialtySelectorDbContext())
                {
                    var department = db.Departments
                        .Find(updateDepartment.Id);
                    
                    department.Name = updateDepartment.Name;
                    department.Description = updateDepartment.Description;
                    department.DeletedOn = updateDepartment.DeletedOn;
                    db.SaveChanges();

                }

                return RedirectToAction("Details", new {id = updateDepartment.Id});
            }

            return View(updateDepartment);
        }
    }
}