using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotoGuild_API.Dto.PostDtos;
using MotoGuild_API.Repository.Interface;

namespace MotoGuild_API.Controllers;

[ApiController]
[Route("api/group/{groupId:int}/post")]
public class GroupPostsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IPostRepository _postRepository;
    private readonly ILoggedUserRepository _loggedUserRepository;

    public GroupPostsController(IPostRepository postRepositort, IMapper mapper, ILoggedUserRepository loggedUserRepository)
    {
        _postRepository = postRepositort;
        _mapper = mapper;
        _loggedUserRepository = loggedUserRepository;
    }

    [HttpGet]
    public IActionResult GetGroupPosts(int groupId)
    {
        var posts = _postRepository.GetAllGroup(groupId);
        return Ok(_mapper.Map<List<PostDto>>(posts));
    }

    [HttpGet("{postId:int}", Name = "GetGroupPost")]
    public IActionResult GetGroupPost(int postId)
    {
        var post = _postRepository.Get(postId);
        if (post == null) return NotFound();
        return Ok(_mapper.Map<PostDto>(post));
    }

    [Authorize]
    [HttpPost]
    public IActionResult CreateGroupPost(int groupId, [FromBody] CreatePostDto createPostDto)
    {
        var userName = _loggedUserRepository.GetLoggedUserName();
        createPostDto.CreateTime = DateTime.Now;
        var post = _mapper.Map<Post>(createPostDto);
        _postRepository.InsertToGroup(post, groupId, userName);
        _postRepository.Save();
        var postDto = _mapper.Map<PostDto>(post);
        return CreatedAtRoute("GetGroupPost", new {groupId, postId = postDto.Id}, postDto);
    }

    [HttpDelete("{postId:int}")]
    public IActionResult DeleteGroupPost(int postId)
    {
        var post = _postRepository.Get(postId);
        if (post == null) return NotFound();
        _postRepository.Delete(postId);
        _postRepository.Save();
        return Ok();
    }
}