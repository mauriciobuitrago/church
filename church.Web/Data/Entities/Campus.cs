using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Church.Common.Entities
{
   public  class Campus
    {
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        public ICollection<District> Districts { get; set; }

        [DisplayName("District Number")]
        public int DistrictsNumber => Districts == null ? 0 : Districts.Count;

        [Display(Name = "# Churches")]
        public int ChurchesNumber => Districts == null ? 0 : Districts.Sum(d => d.ChurchesNumber);

        [Display(Name = "# Users")]
        public int UsersNumber => Districts == null ? 0 : Districts.Sum(d => d.ChurchesNumber);

    }
}
