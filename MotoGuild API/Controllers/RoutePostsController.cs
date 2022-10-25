using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotoGuild_API.Dto.PostDtos;
using MotoGuild_API.Repository.Interface;

namespace MotoGuild_API.Controllers;

[ApiController]
[Route("api/route/{routeId:int}/post")]
public class RoutePostsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IPostRepository _postRepository;
    private readonly ILoggedUserRepository _loggedUserRepository;

    public RoutePostsController(IPostRepository postRepositort, IMapper mapper, ILoggedUserRepository loggedUserRepository)
    {
        _postRepository = postRepositort;
        _mapper = mapper;
        _loggedUserRepository = loggedUserRepository;
    }

    [HttpGet]
    public IActionResult GetRoutePosts(int routeId)
    {
        var posts = _postRepository.GetAllRoute(routeId);
        return Ok(_mapper.Map<List<PostDto>>(posts));
    }

    [HttpGet("{postId:int}", Name = "GetRoutePost")]
    public IActionResult GetRoutePost(int postId)
    {
        var post = _postRepository.Get(postId);
        return Ok(_mapper.Map<PostDto>(post));
    }

    [Authorize]
    [HttpPost]
    public IActionResult CreateRoutePost(int routeId, [FromBody] CreatePostDto createPostDto)
    {
        var userName = _loggedUserRepository.GetLoggedUserName();
        createPostDto.CreateTime = DateTime.Now;
        var post = _mapper.Map<Post>(createPostDto);
        _postRepository.InsertToRoute(post, routeId, userName);
        _postRepository.Save();
        var postDto = _mapper.Map<PostDto>(post);
        return CreatedAtRoute("GetRoutePost", new {routeId, postId = postDto.Id}, postDto);
    }

    [HttpDelete("{postId:int}")]
    public IActionResult DeleteRoutePost(int postId)
    {
        var post = _postRepository.Get(postId);
        if (post == null) return NotFound();
        _postRepository.Delete(postId);
        _postRepository.Save();
        return Ok();
    }
}