namespace SpecialtySelector.Models.Subjects
{
    using SpecialtySelector.Data;
    using System.ComponentModel.DataAnnotations;

    public class AllSubjects
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(1000)]
        [MinLength(1)]
        public string Name { get; set; }

        public bool IsMandatory { get; set; }

        public int Credits { get; set; }

        [Required]
        public int Course { get; set; }

        public string AdminId { get; set; }

        public virtual User Admin { get; set; }
    }
}