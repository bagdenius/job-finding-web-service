using System.ComponentModel.DataAnnotations;

namespace Entities.Enums
{
    public enum Role
    {
        [Display(Name = "Адміністратор")]
        Admin,

        [Display(Name = "Працівник")]
        Applicant,

        [Display(Name = "Роботодавець")]
        Employer
    }
}
