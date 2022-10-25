using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotoGuild_API.Dto.PostDtos;
using MotoGuild_API.Repository.Interface;

namespace MotoGuild_API.Controllers;

[ApiController]
[Route("api/ride/{rideId:int}/post")]
public class RidePostsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IPostRepository _postRepository;
    private readonly ILoggedUserRepository _loggedUserRepository;

    public RidePostsController(IPostRepository postRepositort, IMapper mapper, ILoggedUserRepository loggedUserRepository)
    {
        _postRepository = postRepositort;
        _mapper = mapper;
        _loggedUserRepository = loggedUserRepository;
    }

    [HttpGet]
    public IActionResult GetRidePosts(int rideId)
    {
        var posts = _postRepository.GetAllRide(rideId);
        return Ok(_mapper.Map<List<PostDto>>(posts));
    }

    [HttpGet("{postId:int}", Name = "GetRidePost")]
    public IActionResult GetRidePost(int postId)
    {
        var post = _postRepository.Get(postId);
        return Ok(_mapper.Map<PostDto>(post));
    }

    [Authorize]
    [HttpPost]
    public IActionResult CreateRidePost(int rideId, [FromBody] CreatePostDto createPostDto)
    {
        var userName = _loggedUserRepository.GetLoggedUserName();
        var post = _mapper.Map<Post>(createPostDto);
        post.CreateTime = DateTime.Now;
        _postRepository.InsertToRide(post, rideId, userName);
        _postRepository.Save();
        var postDto = _mapper.Map<PostDto>(post);
        return CreatedAtRoute("GetRidePost", new {rideId, postId = postDto.Id}, postDto);
    }

    [HttpDelete("{postId:int}")]
    public IActionResult DeleteRidePost(int postId)
    {
        var post = _postRepository.Get(postId);
        if (post == null) return NotFound();
        _postRepository.Delete(postId);
        _postRepository.Save();
        return Ok();
    }
}