namespace SpecialtySelector.Data
{
    using SpecialtySelector.Data.SpecialtySelectorEnums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Specialty
    {
        private ICollection<Subject> subjects;

        public Specialty()
        {
            this.subjects = new HashSet<Subject>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(1000)]
        [MinLength(3)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Задължително поле")]
        public Eqd Eqd { get; set; }

        [Required(ErrorMessage = "Задължително поле")]
        public FormOfEducation FormOfEducation { get; set; }

        [StringLength(1600)]
        [Required(ErrorMessage = "Описанието трябва да бъде между 1-1600 символа !!!")]
        public string Description { get; set; }

        public DateTime? DeletedOn { get; set; }
       
        public int? SubDepartmentId { get; set; }
        public virtual SubDepartment SubDepartment { get; set; }

        public string AdminId { get; set; }

        public virtual User Admin { get; set; }

        public virtual ICollection<Subject> Subjects
        {
            get { return this.subjects; }
            set { this.subjects = value; }
        }
    }
}