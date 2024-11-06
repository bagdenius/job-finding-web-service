using System.ComponentModel.DataAnnotations;

namespace Entities.Enums
{
    public enum Sector
    {
        [Display(Name = "Фінансова діяльність")]
        FinancialActivities,

        [Display(Name = "Інформаційні технології")]
        InformationTechnology,

        [Display(Name = "Медіа та інтернет")]
        MediaAndInternet,

        [Display(Name = "Інжиніринг та виробництво")]
        EngineeringAndProduction,

        [Display(Name = "Побутове обслуговування")]
        ConsumerServices,

        [Display(Name = "Медицина")]
        Medicine,

        [Display(Name = "Юриспруденція")]
        Jurisprudence,

        [Display(Name = "Промисловість")]
        Industry,

        [Display(Name = "Малий та середній бізнес")]
        SmallAndMediumBusiness,

        [Display(Name = "Наука")]
        Science,

        [Display(Name = "Культура")]
        Culture,

        [Display(Name = "Соціально-психологічна діяльність")]
        SocioPsychologicalActivities,

        [Display(Name = "Мистецтво та прикладна творчість")]
        ArtAndAppliedArt,

        [Display(Name = "Інформаційні системи")]
        InformationSystems,

        [Display(Name = "Армія та силові структури")]
        ArmyAndLawEnforcementAgencies,

        [Display(Name = "Морська справа")]
        Nautics,

        [Display(Name = "Транспорт")]
        Transport,

        [Display(Name = "Державна служба (чиновництво)")]
        CivilService_Bureaucracy,

        [Display(Name = "Житлово-комунальне господарство")]
        Utilities,

        [Display(Name = "Сільське господарство")]
        Agriculture,

        [Display(Name = "Інспекторські служби")]
        InspectionServices,

        [Display(Name = "Політика та громадська діяльність")]
        PoliticsAndPublicActivities,

        [Display(Name = "Будівництво")]
        Construction,

        [Display(Name = "Електротехніка")]
        ElectricalEngineering,

        [Display(Name = "Освіта та виховання")]
        EducationAndUpbringing,

        [Display(Name = "Торгівля")]
        Trade,

        [Display(Name = "Менеджмент (управління)")]
        Management,

        [Display(Name = "Енергетика")]
        Energy,

        [Display(Name = "Хімія та біологія")]
        ChemistryAndBiology,

        [Display(Name = "Засоби масової інформації")]
        Media,

        [Display(Name = "Видобуток та переробка копалин")]
        MiningAndProcessingOfMinerals,

        [Display(Name = "Аналітична діяльність та прогнозування")]
        AnalyticalActivityAndForecasting,

        [Display(Name = "Природокористування (екологія та охорона)")]
        NatureManagement_EcologyAndProtection,

        [Display(Name = "Адміністрація")]
        Administration,

        [Display(Name = "Туризм, відпочинок, подорожі")]
        TourismRecreationTravel,

        [Display(Name = "Спорт")]
        Sport,

        [Display(Name = "Розважальний та шоу-бізнес")]
        EntertainmentAndShowBusiness,

        [Display(Name = "Металообробка")]
        Metalworking,

        [Display(Name = "Лісне господарство")]
        Forestry,

        [Display(Name = "Деревообробка")]
        Woodworking,

        [Display(Name = "Виробництво продуктів харчування")]
        FoodProduction,

        [Display(Name = "Моделювання та виробництво одягу та взуття")]
        ModelingAndProductionOfClothingAndFootwear,

        [Display(Name = "Поліграфія та рекламна справа")]
        PrintingAndAdvertising,

        [Display(Name = "Громадське харчування та ресторанний бізнес")]
        CateringAndRestaurantBusiness
    }
}
