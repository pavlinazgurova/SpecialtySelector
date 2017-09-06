using SpecialtySelector.Data.SpecialtySelectorEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpecialtySelector.Data
{
    public class Teacher
    {
        public Teacher()
        {
            this.Subjects = new HashSet<Subject>();
        }

        public ICollection<Subject> Subjects { get; set; }

        public int Id { get; set; }

        public Degree Degree { get; set; }

        public AcademicTitle AcademicTitle { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string SecondName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string TeacherInfo { get; set; }

        public string AdminId { get; set; }

        public virtual User Admin { get; set; }

        public DateTime? FiredOn { get; set; }
    }
}