using Church.Common.Entities;
using Church.Common.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Church.Web.Data.Entities
{
    public class User : IdentityUser
    {
       

        [Display(Name = "First Name")]
        [MaxLength(50)]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(50)]
        [Required]
        public string LastName { get; set; }

        [MaxLength(20)]
        [Required]
        public string Document { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        [MaxLength(20)]
        [Required]
        public string NumberPhone { get; set; }

        [MaxLength(50)]
        public string EmailAddress { get; set; }

        public Churchi Churchi { get; set; }

        [Display(Name = "Image")]
        public Guid ImageId { get; set; }

        [Display(Name = "Image")]
        public string ImageFullPath => ImageId == Guid.Empty
           ? $"https://churchma.azurewebsites.net/images/noimage.png"
            : $"https://churchidemo.blob.core.windows.net/users/{ImageId}";

        public Profession Profession { get; set; }


        [Display(Name = "User Type")]
        public UserType UserType { get; set; }

        [Display(Name = "User")]
        public string FullName => $"{FirstName} {LastName}";

        [Display(Name = "User")]
        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";


        public ICollection<Assistance> Assistances { get; set; }
    }
}
