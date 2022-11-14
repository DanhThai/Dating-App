using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.DTO;

namespace DatingApp.API.Services
{
    public interface IAuthService
    {
        public string Login(UserDTO user);
        public string Register(UserRegisterDTO user);
    }
}