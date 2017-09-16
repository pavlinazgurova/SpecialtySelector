namespace SpecialtySelector.Models.Subjects
{
    using SpecialtySelector.Data;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class SubjectInfo
    {
        public ICollection<Teacher> Teachers { get; set; }
        public ICollection<Specialty> Specialties { get; set; }

        public int Id { get; set; }

        [Required]
        [MaxLength(1000)]
        [MinLength(1)]
        public string Name { get; set; }

        public bool IsMandatory { get; set; }

        public int Credits { get; set; }

        [Required]
        public int Course { get; set; }

        [StringLength(1600)]
        public string Description { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string AdminId { get; set; }

        public virtual User Admin { get; set; }
    }
}