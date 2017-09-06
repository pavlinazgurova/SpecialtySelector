using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace SpecialtySelector.Data
{
    public class SpecialtySelectorDbContext : IdentityDbContext<User>
    {
        public SpecialtySelectorDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<Department> Departments { get; set; }

        public virtual IDbSet<SubDepartment> SubDepartments { get; set; }

        public virtual IDbSet<Specialty> Specialties { get; set; }

        public virtual IDbSet<Subject> Subjects { get; set; }

        public virtual IDbSet<Teacher> Teachers { get; set; }

        public static SpecialtySelectorDbContext Create()
        {
            return new SpecialtySelectorDbContext();
        }
    }
}