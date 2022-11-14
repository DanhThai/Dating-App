using DatingApp.API.DTO;
using DatingApp.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/members")]
    public class MemberController : BaseController
    {
        private readonly IMemberService _memberService;
        public MemberController(IMemberService memberService)
        {
            this._memberService = memberService;
        }

        [HttpGet]
        public ActionResult<List<MemberDTO>> Get()
        {
            return Ok(_memberService.GetMembers());
        }

        [HttpGet("{username}")]
        public ActionResult<MemberDTO> Get(string username)
        {
            var member= _memberService.GetMemberByUsername(username);
            if( member == null)
                return NotFound();
            return member;
        }

       
    }
}