using System.ComponentModel.DataAnnotations;

namespace SpecialtySelector.Models.SubDepartment
{
    public class CreateSubDepartment
    {
        [Required(ErrorMessage = "Името трябва да бъде между 1-1000 символа !!!")]
        [Display(Name = "Име на Направлението")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Описанието трябва да бъде между 1-1600 символа !!!")]
        [Display(Name = "Кратко описание на Направлението")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Избери направление")]
        public int? DepartmentId { get; set; }
    }
}