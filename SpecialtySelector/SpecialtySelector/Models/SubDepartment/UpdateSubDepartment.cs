using System;
using System.ComponentModel.DataAnnotations;

namespace SpecialtySelector.Models.SubDepartment
{
    public class UpdateSubDepartment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Името трябва да бъде между 3-1000 символа !!!")]
        [MaxLength(1000)]
        [MinLength(1)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Описанието трябва да бъде между 20-1600 символа !!!")]
        [StringLength(1600)]
        [MinLength(1)]
        public string Description { get; set; }

        public string AdminId { get; set; }

        public DateTime? DeletedOn { get; set; }

        public int? DepartmentId { get; set; }
    }
}