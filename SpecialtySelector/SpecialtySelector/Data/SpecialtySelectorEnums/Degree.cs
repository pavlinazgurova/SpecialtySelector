using System.ComponentModel.DataAnnotations;

namespace SpecialtySelector.Data.SpecialtySelectorEnums
{
    public enum Degree
    {
        #region The scientific degrees in Bulgarian

        //Научните степени са:
        //1. "доктор" (научно-образователна);
        //2. "доктор на науките".

        #endregion The scientific degrees in Bulgarian
            [Display(Name = "Доктор")]
        Doctor,
            [Display(Name = "Доктор на науките")]
        DoctorOfScience
    }
}