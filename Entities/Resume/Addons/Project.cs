using System.ComponentModel.DataAnnotations;

namespace Entities.Addons
{
    public class Project
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        //public string? Contribution { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
    }
}
