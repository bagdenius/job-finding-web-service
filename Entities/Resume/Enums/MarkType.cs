using System.ComponentModel.DataAnnotations;

namespace Entities.Enums
{
    public enum MarkType
    {
        [Display(Name = "П'ятибальна (коледжна)")]
        FivePoint,

        [Display(Name = "Семибальна (GPA)")]
        GPA,

        [Display(Name = "Десятибальна")]
        TenPoint,

        [Display(Name = "Дванадцятибальна")]
        TwelvePoint,

        [Display(Name = "Відсоткова")]
        Percentage
    }
}
