using System.ComponentModel.DataAnnotations;

namespace Rakais_EL3lag.Models
{
    public class Image
    {
        public int Id { get; set; }
        public int SectionId { get; set; }
        [MaxLength(5000)]
        public string ImageUrl { get; set; } = null!;
        public bool Active { get; set; } = true;

        public Section? Section { get; set; } = null!;
    }


}
