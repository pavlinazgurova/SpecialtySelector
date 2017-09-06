using System.ComponentModel.DataAnnotations;

namespace SpecialtySelector.Data.SpecialtySelectorEnums
{
    public enum FormOfEducation
    {
        #region Form of education in Bulgarian

        // присъствено обучение
        // задочно обучение

        #endregion Form of education in Bulgarian

            [Display(Name = "Присъствено обучение")]
        AttendanceТraining,
            [Display(Name = "Задочно обучение")]
        DistanceLearning
    }
}