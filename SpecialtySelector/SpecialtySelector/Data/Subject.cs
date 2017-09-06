using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpecialtySelector.Data
{
    public class Subject
    {
        public Subject()
        {
            this.Teachers = new HashSet<Teacher>();
            this.Specialties = new HashSet<Specialty>();
        }

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

        public string AdminId { get; set; }

        public virtual User Admin { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}