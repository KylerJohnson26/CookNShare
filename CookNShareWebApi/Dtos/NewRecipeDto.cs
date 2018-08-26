using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CookNShareWebApi.Dtos
{
    public class NewRecipeDto
    {
        public int UserId { get; set; }

        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }

        [Required]
        public ICollection<string> Steps { get; set; }
    }
}