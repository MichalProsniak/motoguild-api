using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotoGuild_API.Dto.UserDtos;
using MotoGuild_API.Repository.Interface;

namespace MotoGuild_API.Controllers
{
   // [Authorize]
    [ApiController]
    [Route("api/profile")]
    public class ProfilController : ControllerBase
    {
      /*  private readonly IMapper _mapper;
        private readonly IProfileRepository _profileRepository;

        public ProfilController(IMapper mapper, IProfileRepository profileRepository)
        {
            _mapper = mapper;
            _profileRepository = profileRepository;
        }

        [HttpGet("{userId:int}")]
        public IActionResult GetUser(int userId)
        { 
            var user = _profileRepository.GetUser(userId);
            return Ok(_mapper.Map<UserProfileDto>(user));
        }*/
    }
}
