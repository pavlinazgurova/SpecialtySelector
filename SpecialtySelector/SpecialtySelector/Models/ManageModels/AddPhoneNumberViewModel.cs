using System.ComponentModel.DataAnnotations;

namespace SpecialtySelector.Models.ManageModels
{
    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }
}