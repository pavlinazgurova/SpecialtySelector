namespace SpecialtySelector.Models.Specialties
{
    using SpecialtySelector.Data.SpecialtySelectorEnums;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class UpdateSpecialty
    {
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

        public string AdminId { get; set; }
    }
}