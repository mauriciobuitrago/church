using Church.Web.Data.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
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

        [JsonIgnore]
        public ICollection<User> Users { get; set; }

        [Display(Name = "# Users")]
        public int UsersNumber => Users == null ? 0 : Users.Count;

        [JsonIgnore]
        public District District { get; set; }

        [JsonIgnore]
        [NotMapped]
        public int DistrictId { get; set; }

    }
}
