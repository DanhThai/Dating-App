using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.DTO
{
    public class UserRegisterDTO
    {
        [Required]
        [MaxLength(256)]
        public string Username { get; set; }
 
        [MaxLength(256)]
        public string Email { get; set; }
 
        [Required]
        [MaxLength(50)]
        public string Password{get;set;}
        [MaxLength(256)]
        public string Avatar { get; set; }
 
        public DateTime? DateOfBirth { get; set; }
 
        [MaxLength(32)]
        public string KnownAs { get; set; }
 
        [MaxLength(6)]
        public string Gender { get; set; }
 
        [MaxLength(32)]
        public string City { get; set; }
 
        [MaxLength(256)]
        public string Introduction { get; set; }
    }
}