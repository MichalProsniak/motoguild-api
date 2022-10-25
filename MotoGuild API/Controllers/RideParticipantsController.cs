using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotoGuild_API.Dto.UserDtos;
using MotoGuild_API.Repository.Interface;

namespace MotoGuild_API.Controllers;

[ApiController]
[Route("api/rides/{rideId:int}/participants")]
public class RideParticipantsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IRideParticipantsRepository _rideParticipantsRepository;
    private readonly ILoggedUserRepository _loggedUserRepository;

    public RideParticipantsController(IRideParticipantsRepository rideParticipantsRepository, IMapper mapper, ILoggedUserRepository loggedUserRepository)
    {
        _rideParticipantsRepository = rideParticipantsRepository;
        _mapper = mapper;
        _loggedUserRepository = loggedUserRepository;
    }

    [HttpGet]
    public IActionResult GetRideParticipants(int rideId)
    {
        var participants = _rideParticipantsRepository.GetAll(rideId);
        if (participants == null) return NotFound();
        return Ok(_mapper.Map<List<UserDto>>(participants));
    }

    [HttpGet("{id:int}", Name = "GetRideParticipant")]
    public IActionResult GetRideParticipant(int rideId, int id)
    {
        var participant = _rideParticipantsRepository.Get(rideId, id);
        if (participant == null) return NotFound();
        return Ok(_mapper.Map<UserDto>(participant));
    }


    [Authorize]
    [HttpPost("logged")]
    public IActionResult AddRideParticipantLogged(int rideId)
    {
        var userName = _loggedUserRepository.GetLoggedUserName();
        if (!_rideParticipantsRepository.RideExist(rideId) || !_rideParticipantsRepository.UserExits(userName))
            return NotFound();

        if (_rideParticipantsRepository.UserInRide(rideId, userName)) return BadRequest();
        _rideParticipantsRepository.AddParticipantByUserName(rideId, userName);
        _rideParticipantsRepository.Save();
        var user = _rideParticipantsRepository.GetUserByName(userName);
        var userDto = _mapper.Map<UserDto>(user);
        return CreatedAtRoute("GetRideParticipant", new { id = userDto.Id, rideId }, userDto);
    }

    [Authorize]
    [HttpDelete("logged")]
    public IActionResult DeleteRideParticipantLogged(int rideId)
    {
        var userName = _loggedUserRepository.GetLoggedUserName();
        if (!_rideParticipantsRepository.RideExist(rideId) || !_rideParticipantsRepository.UserExits(userName))
            return NotFound();

        if (!_rideParticipantsRepository.UserInRide(rideId, userName)) return NotFound();
        _rideParticipantsRepository.DeleteParticipantByUserName(rideId, userName);
        _rideParticipantsRepository.Save();

        return NoContent();
    }
}