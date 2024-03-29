﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Church.Common.Entities
{
    public class District
    {
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        public ICollection<Churchi> Churches { get; set; }

        [DisplayName("Churches Number")]
        public int ChurchesNumber => Churches == null ? 0 : Churches.Count;

        [Display(Name = "# Users")]
        public int UsersNumber => Churches == null ? 0 : Churches.Sum(c => c.UsersNumber);


        [NotMapped]
        public int IdCampus { get; set; }

        [JsonIgnore]
        public Campus Campuses { get; set; }
    }
}
