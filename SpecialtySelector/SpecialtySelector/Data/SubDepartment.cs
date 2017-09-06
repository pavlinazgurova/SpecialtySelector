using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpecialtySelector.Data
{
    public class SubDepartment
    {
        private ICollection<Specialty> specialties;

        public SubDepartment()
        {
            this.specialties = new HashSet<Specialty>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(1000)]
        [MinLength(3)]
        public string Name { get; set; }

        [StringLength(1600)]
        public string Description { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string AdminId { get; set; }

        public virtual User Admin { get; set; }

        public virtual ICollection<Specialty> Specialties
        {
            get { return this.specialties; }
            set { this.specialties = value; }
        }

        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }

        public int? DepartmentId { get; set; }
    }
}