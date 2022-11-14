
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using DatingApp.API.Data.Entity;
using DatingApp.API.Data.Repositories;
using DatingApp.API.DTO;

namespace DatingApp.API.Services
{
    public class AuthService : IAuthService
    {

        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AuthService(IUserRepository userRepository ,ITokenService tokenService,IMapper mapper)
        {
            this._userRepository = userRepository;
            this._tokenService = tokenService;
            this._mapper = mapper;
        }

        public string Login(UserDTO userDTO)
        {
            userDTO.Username=userDTO.Username.ToLower();
            var currentUser= _userRepository.GetUserByUsername(userDTO.Username);
            if(currentUser ==null){
                throw new UnauthorizedAccessException("Username is invalid");
            }
            using var hmac= new HMACSHA512(currentUser.PasswordSalt);
            var passwordByte=hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password));
            for (int i =0; i< currentUser.PasswordHash.Length; i++)
                if(currentUser.PasswordHash[i]!=passwordByte[i])
                    throw new UnauthorizedAccessException("Password is invalid");

            var token= _tokenService.CreateToken(currentUser.Username);
            return token;
        }


        public string Register(UserRegisterDTO user)
        {
            user.Username=user.Username.ToLower();
            if(_userRepository.GetUserByUsername(user.Username)!=null)
                throw new BadHttpRequestException("Username is already register");
            using var hmac= new HMACSHA512();
            var passwordToByte=Encoding.UTF8.GetBytes(user.Password);

            var newUser=new User{
                Username= user.Username,
                PasswordSalt=hmac.Key,
                PasswordHash=hmac.ComputeHash(passwordToByte),
                Avatar= user.Avatar,
                DateOfBirth= user.DateOfBirth,
                KnownAs = user.KnownAs,
                Gender= user.Gender,
                City= user.City,
                Introduction= user.Introduction
            };
            _userRepository.InsertNewUser(newUser);
            _userRepository.IsSaveChanges();
            var token= _tokenService.CreateToken(newUser.Username);
            return token;
        }
    }
}