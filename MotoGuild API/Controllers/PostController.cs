using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MotoGuild_API.Dto.PostDtos;
using MotoGuild_API.Repository.Interface;

namespace MotoGuild_API.Controllers;

[ApiController]
[Route("api/post")]
public class PostController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IPostRepository _postRepository;

    public PostController(IPostRepository postRepository, IMapper mapper)
    {
        _postRepository = postRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetPosts()
    {
        var posts = _postRepository.GetAll();
        return Ok(_mapper.Map<List<PostDto>>(posts));
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