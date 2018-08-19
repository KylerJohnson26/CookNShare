using System;
using System.ComponentModel.DataAnnotations;

namespace CookNShareWebApi.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(12, MinimumLength = 4, ErrorMessage = "Your password must be between 4 and 12 characters long")]
        public string Password { get; set; }

        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }
    }
}