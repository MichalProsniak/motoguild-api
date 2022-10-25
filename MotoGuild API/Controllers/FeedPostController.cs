using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotoGuild_API.Dto.PostDtos;
using MotoGuild_API.Repository.Interface;

namespace MotoGuild_API.Controllers;

[ApiController]
[Route("api/feed/{feedId:int}/post")]
public class FeedPostController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IPostRepository _postRepository;
    private readonly ILoggedUserRepository _loggedUserRepository;

    public FeedPostController(IPostRepository postRepositort, IMapper mapper, ILoggedUserRepository loggedUserRepository)
    {
        _postRepository = postRepositort;
        _mapper = mapper;
        _loggedUserRepository = loggedUserRepository;
    }

    [HttpGet]
    [Authorize]
    public IActionResult GetPostsFeed(int feedId)
    {
        var posts = _postRepository.GetAllFeed(feedId);

        if (posts == null) return NotFound();

        return Ok(_mapper.Map<List<PostDto>>(posts));
    }


    [HttpGet("{postId:int}", Name = "GetFeedPost")]
    public IActionResult GetPostFeed(int postId)
    {
        var post = _postRepository.Get(postId);
        if (post == null) return NotFound();
        return Ok(_mapper.Map<PostDto>(post));
    }

    [Authorize]
    [HttpPost]
    public IActionResult CreateFeedPost(int feedId, [FromBody] CreatePostDto createPostDto)
    {
        var userName = _loggedUserRepository.GetLoggedUserName();
        createPostDto.CreateTime = DateTime.Now;
        var post = _mapper.Map<Post>(createPostDto);
        _postRepository.InsertToFeed(post, feedId, userName);
        _postRepository.Save();
        var postDto = _mapper.Map<PostDto>(post);
        return CreatedAtRoute("GetFeedPost", new {feedId, postId = postDto.Id}, postDto);
    }

    [HttpDelete("{postId:int}")]
    public IActionResult DeletePost(int postId)
    {
        var post = _postRepository.Get(postId);
        if (post == null) return NotFound();
        _postRepository.Delete(postId);
        _postRepository.Save();
        return Ok();
    }
}