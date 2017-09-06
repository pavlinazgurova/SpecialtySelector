using SpecialtySelector.Data;
using SpecialtySelector.Models.Departments;
using System.Linq;
using System.Web.Mvc;
using SpecialtySelector.Models.SubDepartment;

namespace SpecialtySelector.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new SpecialtySelectorDbContext())
            {
                var departments = db.Departments
                    .Where(y => y.DeletedOn.Equals(null))
                    .Select(x => new HomeIndexDepartmentsModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description
                    })
                    .ToList();

                return View(departments);
            }
        }
    }
}