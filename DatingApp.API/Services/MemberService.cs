
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DatingApp.API.Data;
using DatingApp.API.Data.Entity;
using DatingApp.API.DTO;

namespace DatingApp.API.Services
{
    public class MemberService : IMemberService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public MemberService(DataContext context,IMapper mapper)
        {
            this._mapper = mapper;
            this._context = context;
        }

        public MemberDTO GetMemberByUsername(string username)
        {
            var user= _context.AppUsers.FirstOrDefault(u=>u.Username == username);
            if (user==null) return null;
            return _mapper.Map<User,MemberDTO>(user);
        }

        public List<MemberDTO> GetMembers()
        {
            return _context.AppUsers
                    .ProjectTo<MemberDTO>(_mapper.ConfigurationProvider).ToList();
            
        }
    }
}