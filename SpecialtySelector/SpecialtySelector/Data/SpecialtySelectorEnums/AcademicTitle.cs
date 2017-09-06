using System.ComponentModel.DataAnnotations;

namespace SpecialtySelector.Data.SpecialtySelectorEnums
{
    public enum AcademicTitle
    {
        #region Academic titles in Bulgarian

        //Академичните длъжности са:
        //1. "асистент";
        //2. "главен асистент";
        //3. "доцент";
        //4. "професор".

        #endregion Academic titles in Bulgarian

            [Display(Name = "Асистент")]
        Assistant,
            [Display(Name = "Главен асистент")]
        ChiefAssistant,
            [Display(Name = "Доцент")]
        AssociateProfessor,
            [Display(Name = "Професор")]
        Professor
    }
}