using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

using Newtonsoft.Json;

namespace Church.Common.Entities
{
    public class Profession
    {
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

     
    }
}
