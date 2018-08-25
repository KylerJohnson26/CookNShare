using System;
using System.Collections.Generic;

namespace CookNShareWebApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastActive { get; set; }
        public ICollection<Recipe> MyProperty { get; set; }
    }
}