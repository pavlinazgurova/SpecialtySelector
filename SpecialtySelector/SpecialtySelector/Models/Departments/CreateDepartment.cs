using System.ComponentModel.DataAnnotations;

namespace SpecialtySelector.Models.Departments
{
    public class CreateDepartment
    {
       // [MinLength(3)]
        [Required(ErrorMessage = "Името трябва да бъде между 1-1000 символа !!!")]
        [Display(Name = "Име на Направлението")]
        public string Name { get; set; }

       // [MinLength(20)]
        [Required(ErrorMessage = "Описанието трябва да бъде между 1-1600 символа !!!")]
        [Display(Name = "Кратко описание на Направлението")]
        public string Description { get; set; }
    }
}