using System.ComponentModel.DataAnnotations;

namespace SpecialtySelector.Data.SpecialtySelectorEnums
{
    public enum Eqd
    {
        #region Eqd in BUlgarian

        // Бакалавър
        // Магистър
        // Докторант

        #endregion Eqd in BUlgarian
            [Display(Name = "Бакалавър")]
        Bachelor,
            [Display(Name = "Магистър")]
        Master,
            [Display(Name = "Докторант")]
        Doctorate
    }
}