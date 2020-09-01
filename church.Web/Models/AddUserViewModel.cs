using Church.Web.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Church.Web.Models
{
    public class AddUserViewModel : User
    {
        [Display(Name = "Churchi")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Church.")]
        [Required]
        public int ChurchId { get; set; }

        public IEnumerable<SelectListItem> Churches { get; set; }



        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }

    }
}
