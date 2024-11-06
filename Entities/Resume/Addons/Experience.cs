using System.ComponentModel.DataAnnotations;

namespace Entities.Addons
{
    public class Experience
    {
        public string Id { get; set; }
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
    }
}
