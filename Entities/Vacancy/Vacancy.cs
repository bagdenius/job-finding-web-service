namespace Entities
{
    public class Vacancy
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string? Picture { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string? Sector { get; set; }
        public string EmployerId { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        public ICollection<Application> Applications { get; set; }
    }
}
