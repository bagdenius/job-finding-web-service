namespace Entities
{
    public class Application
    {
        public string Id { get; set; }
        public string ApplicantId { get; set; }
        public string Name { get; set; }
        //public string College { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public bool IsApproved { get; set; } = false;

        public string VacancyId { get; set; }
        public Vacancy Vacancy { get; set; }
    }
}
