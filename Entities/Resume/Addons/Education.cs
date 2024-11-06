using System.ComponentModel.DataAnnotations;

namespace Entities.Addons
{
    public class Education
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Institute { get; set; }
        public string University { get; set; }
        public string Specialization { get; set; }
        public float AvgMark { get; set; }
        public string MarkType { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
    }
}
