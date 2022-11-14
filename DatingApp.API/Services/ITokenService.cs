using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data.Entity;

namespace DatingApp.API.Services
{
    public interface ITokenService
    {
        string CreateToken(string username);
    }
}