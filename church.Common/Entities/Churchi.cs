using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Church.Common.Entities
{
    public class Churchi
    {
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [NotMapped]
        public int IdChurch { get; set; }


    }
}
