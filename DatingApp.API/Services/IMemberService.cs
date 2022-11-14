using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.DTO;

namespace DatingApp.API.Services
{
    public interface IMemberService
    {
        List<MemberDTO> GetMembers();
        MemberDTO GetMemberByUsername(string username);
    }
}