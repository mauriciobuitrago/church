using Church.Common.Entities;
using Church.Common.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Church.Web.Models
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
       
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string Document { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string NumberPhone { get; set; }
   
        [Display(Name = "Image")]
        public Guid ImageId { get; set; }

        [Display(Name = "Image")]
        public string ImageFullPath => ImageId == Guid.Empty
           ? $"https://churchma.azurewebsites.net/images/noimage.png"
            : $"https://churchidemo.blob.core.windows.net/users/{ImageId}";

        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }

     
        [Display(Name = "Profession")]
        [Range(1, int.MaxValue, ErrorMessage = "you must select a Profession")]
        public int ProfessionsId { get; set; }
        public IEnumerable<SelectListItem> Professions { get; set; }
      
        [Display(Name = "Church")]
        [Range(1, int.MaxValue, ErrorMessage = "you must select a Church")]
        public int ChurchisId { get; set; }
        public IEnumerable<SelectListItem> Churchis { get; set; }

        [Display(Name = "Distric")]
        [Range(1, int.MaxValue, ErrorMessage = "you must select a District")]
        public int DistrictsId { get; set; }
        public IEnumerable<SelectListItem> Districts { get; set; }

        [Display(Name = "Campus")]
        [Range(1, int.MaxValue, ErrorMessage = "you must select a Campus")]
        public int CampusesId { get; set; }
        public IEnumerable<SelectListItem> Campuses { get; set; }


    }
}
