using System.ComponentModel.DataAnnotations;

namespace SpecialtySelector.Models.Departments
{
    public class DepartmentDetailsModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(1000)]
        [MinLength(3)]
        public string Name { get; set; }

        [StringLength(1600)]
        public string Description { get; set; }
    }
}