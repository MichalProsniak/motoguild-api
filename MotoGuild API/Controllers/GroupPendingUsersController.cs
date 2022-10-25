using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotoGuild_API.Dto.UserDtos;
using MotoGuild_API.Repository.Interface;

namespace MotoGuild_API.Controllers;

[ApiController]
[Route("api/groups/{groupId:int}/pendingusers")]
public class GroupPendingUsersController : ControllerBase
{
    private readonly IGroupPendingUsersRepository _groupPendingUsersRepository;
    private readonly IMapper _mapper;
    private readonly ILoggedUserRepository _loggedUserRepository;

    public GroupPendingUsersController(IGroupPendingUsersRepository groupPendingUsersRepository, IMapper mapper,
        ILoggedUserRepository loggedUserRepository)
    {
        _groupPendingUsersRepository = groupPendingUsersRepository;
        _mapper = mapper;
        _loggedUserRepository = loggedUserRepository;
    }

    [HttpGet]
    public IActionResult GetGroupPendingUsers(int groupId)
    {
        var pendingUsers = _groupPendingUsersRepository.GetAll(groupId);
        if (pendingUsers == null) return NotFound();
        return Ok(_mapper.Map<List<UserDto>>(pendingUsers));
    }

    [HttpGet("{id:int}", Name = "GetGroupPendingUser")]
    public IActionResult GetGroupPendingUser(int groupId, int id)
    {
        var pendingUsers = _groupPendingUsersRepository.Get(groupId, id);
        if (pendingUsers == null) return NotFound();
        return Ok(_mapper.Map<UserDto>(pendingUsers));
    }

    [Authorize]
    [HttpPost("{id:int}")]
    public IActionResult AddGroupPendingUserByUserId(int groupId, int id)
    {
        var userName = _loggedUserRepository.GetLoggedUserName();
        id = _groupPendingUsersRepository.GetUserId(userName);
        if (!_groupPendingUsersRepository.GroupExist(groupId) || !_groupPendingUsersRepository.UserExits(id))
            return NotFound();

        if (_groupPendingUsersRepository.UserInPendingUsers(groupId, id)) return BadRequest();
        _groupPendingUsersRepository.AddPendingUserByUserId(groupId, id);
        _groupPendingUsersRepository.Save();
        var user = _groupPendingUsersRepository.GetUser(id);
        var userDto = _mapper.Map<UserDto>(user);
        return CreatedAtRoute("GetGroupPendingUser", new {id = userDto.Id, groupId}, userDto);
    }


    [HttpDelete("{id:int}")]
    public IActionResult DeleteGroupPendingUserByUserId(int groupId, int id)
    {
        if (!_groupPendingUsersRepository.GroupExist(groupId) || !_groupPendingUsersRepository.UserExits(id))
            return NotFound();

        if (!_groupPendingUsersRepository.UserInPendingUsers(groupId, id)) return NotFound();
        _groupPendingUsersRepository.DeletePendingUserByUserId(groupId, id);
        _groupPendingUsersRepository.Save();

        return NoContent();
    }
}