using System.ComponentModel.DataAnnotations;

namespace SpecialtySelector.Models.AccountModels
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}