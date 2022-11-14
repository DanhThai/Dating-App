using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.DTO
{
    public class UserDTO
    {
        [Required]
        [MaxLength(50)]
        public string Username{get;set;}
        
        [Required]
        [MaxLength(50)]
        public string Password{get;set;}
    }
    
}