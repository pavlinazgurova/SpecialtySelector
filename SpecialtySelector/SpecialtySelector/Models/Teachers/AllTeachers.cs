namespace SpecialtySelector.Models.Teachers
{
    using SpecialtySelector.Data;
    using SpecialtySelector.Data.SpecialtySelectorEnums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AllTeachers
    {
        public int Id { get; set; }

        public Degree Degree { get; set; }

        public AcademicTitle AcademicTitle { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string SecondName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string TeacherInformation { get; set; }

        public DateTime? FiredOn { get; set; }

        public List<int> Subject { get; set; }

        public ICollection<Subject> Subjects { get; set; }

        public string AdminId { get; set; }
    }
}