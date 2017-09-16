namespace SpecialtySelector.Models.Subjects
{
    using SpecialtySelector.Data;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CreateSubject
    {
        [Required(ErrorMessage = "Името трябва да бъде между 1-1000 символа !!!")]
        [Display(Name = "Име на предмет:")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Задължително поле")]
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

        public List<int> Specialty { get; set; }
        //fix
        public ICollection<Specialty> Specialties { get; set; }

        public List<int> Teacher { get; set; }
        //fix
        public ICollection<Teacher> Teachers { get; set; }
    }
}