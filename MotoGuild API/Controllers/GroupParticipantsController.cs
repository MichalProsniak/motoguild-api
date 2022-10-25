using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotoGuild_API.Dto.UserDtos;
using MotoGuild_API.Repository.Interface;

namespace MotoGuild_API.Controllers;

[ApiController]
[Route("api/groups/{groupId:int}/participants")]
public class GroupParticipantsController : ControllerBase
{
    private readonly IGroupParticipantsRepository _groupParticipantsRepository;
    private readonly IMapper _mapper;
    private readonly ILoggedUserRepository _loggedUserRepository;

    public GroupParticipantsController(IGroupParticipantsRepository groupParticipantsRepository, 
        IMapper mapper, ILoggedUserRepository loggedUserRepository)
    {
        _groupParticipantsRepository = groupParticipantsRepository;
        _mapper = mapper;
        _loggedUserRepository = loggedUserRepository;
    }

    [HttpGet]
    public IActionResult GetGroupParticipants(int groupId)
    {
        var participants = _groupParticipantsRepository.GetAll(groupId);
        if (participants == null) return NotFound();
        return Ok(_mapper.Map<List<UserDto>>(participants));
    }

    [HttpGet("{id:int}", Name = "GetGroupParticipant")]
    public IActionResult GetGroupParticipant(int groupId, int id)
    {
        var participant = _groupParticipantsRepository.Get(groupId, id);
        if (participant == null) return NotFound();
        return Ok(_mapper.Map<UserDto>(participant));
    }

    [Authorize]
    [HttpPost("{id:int}")]
    public IActionResult AddGroupParticipantByUserId(int groupId, int id)
    {
        var userName = _groupParticipantsRepository.GetUserName(id);
        if (!_groupParticipantsRepository.GroupExist(groupId) || !_groupParticipantsRepository.UserExits(userName))
            return NotFound();
        if (_groupParticipantsRepository.UserInGroup(groupId, userName)) return BadRequest();
        _groupParticipantsRepository.AddParticipantByUserId(groupId, id);
        _groupParticipantsRepository.Save();
        var user = _groupParticipantsRepository.GetUserByName(userName);
        var userDto = _mapper.Map<UserDto>(user);
        return CreatedAtRoute("GetGroupParticipant", new {id = userDto.Id, groupId}, userDto);
    }

    [Authorize]
    [HttpPost("logged")]
    public IActionResult AddLoggedGroupParticipant(int groupId)
    {
        var userName = _loggedUserRepository.GetLoggedUserName();
        if (!_groupParticipantsRepository.GroupExist(groupId) || !_groupParticipantsRepository.UserExits(userName))
            return NotFound();
        if (_groupParticipantsRepository.UserInGroup(groupId, userName)) return BadRequest();
        _groupParticipantsRepository.AddParticipantByUserName(groupId, userName);
        _groupParticipantsRepository.Save();
        var user = _groupParticipantsRepository.GetUserByName(userName);
        var userDto = _mapper.Map<UserDto>(user);
        return CreatedAtRoute("GetGroupParticipant", new { id = userDto.Id, groupId }, userDto);
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public IActionResult DeleteGroupParticipantByUserId(int groupId, int id)
    {
        var userName = _groupParticipantsRepository.GetUserName(id);
        if (!_groupParticipantsRepository.GroupExist(groupId) || !_groupParticipantsRepository.UserExits(userName))
            return NotFound();

        if (!_groupParticipantsRepository.UserInGroup(groupId, userName)) return NotFound();
        _groupParticipantsRepository.DeleteParticipantByUserId(groupId, id);
        _groupParticipantsRepository.Save();

        return NoContent();
    }

    [Authorize]
    [HttpDelete("logged")]
    public IActionResult DeleteGroupParticipant(int groupId)
    {
        var userName = _loggedUserRepository.GetLoggedUserName();
        var id = _groupParticipantsRepository.GetUserByName(userName).Id;
        if (!_groupParticipantsRepository.GroupExist(groupId) || !_groupParticipantsRepository.UserExits(userName))
            return NotFound();

        if (!_groupParticipantsRepository.UserInGroup(groupId, userName)) return NotFound();
        _groupParticipantsRepository.DeleteParticipantByUserId(groupId, id);
        _groupParticipantsRepository.Save();

        return NoContent();
    }
}