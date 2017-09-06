using System.ComponentModel.DataAnnotations;
using SpecialtySelector.Data.SpecialtySelectorEnums;

namespace SpecialtySelector.Models.Specialties
{
    public class CreateSpecialty
    {
        [Required(ErrorMessage = "Името трябва да бъде между 1-1000 символа !!!")]
        [Display(Name = "Име на Специалност")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Описанието трябва да бъде между 1-1600 символа !!!")]
        [Display(Name = "Кратко описание на Специалността")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Избери поднаправление")]
        public int? SubDepartmentId { get; set; }

        [Required(ErrorMessage = "Задължително поле")]
        [Display(Name = "Образователно квалификационна спетен:")]
        public Eqd Eqd { get; set; }

        [Required(ErrorMessage = "Задължително поле ")]
        [Display(Name = "Форма на обучение:")]
        public FormOfEducation FormOfEducation { get; set; }
    }
}