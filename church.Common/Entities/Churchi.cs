using System.ComponentModel.DataAnnotations;

namespace Church.Common.Entities
{
    public class Churchi
    {
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

    }
}
