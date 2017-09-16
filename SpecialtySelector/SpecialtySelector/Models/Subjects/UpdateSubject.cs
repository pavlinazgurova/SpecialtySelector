namespace SpecialtySelector.Models.Subjects
{
    using SpecialtySelector.Data;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class UpdateSubject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Името трябва да бъде между 1-1000 символа !!!")]
        [Display(Name = "Име на предмет:")]
        public string Name { get; set; }

       // [Required(ErrorMessage = "Задължително поле")]
        [Display(Name = "Задължителен:")]
        public bool IsMandatory { get; set; }

        [Required(ErrorMessage = "Задължително поле")]
        [Display(Name = "Кредити:")]
        public int Credits { get; set; }

        [Required(ErrorMessage = "Задължително поле")]
        [Display(Name = "Курс:")]
        public int Course { get; set; }

        [Required(ErrorMessage = "Описанието трябва да бъде между 1-1600 символа !!!")]
        [Display(Name = "Кратко описание на предмета:")]
        public string Description { get; set; }

        public DateTime? DeletedOn { get; set; }

        public List<int> Specialty { get; set; }

        public ICollection<Specialty> Specialties { get; set; }

        public List<int> Teacher { get; set; }

        public ICollection<Teacher> Teachers { get; set; }

        public string AdminId { get; set; }
    }
}
