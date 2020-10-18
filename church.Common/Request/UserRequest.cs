﻿using System.ComponentModel.DataAnnotations;

namespace Church.Common.Requests
{
    public class UserRequest
    {
        [Required]
        public string Document { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string Password { get; set; }

        public string PasswordConfirm { get; set; }

        public int ChurcheId { get; set; }

        public int ProfessionId { get; set; }

        public byte[] PictureArray { get; set; }

    }
}
