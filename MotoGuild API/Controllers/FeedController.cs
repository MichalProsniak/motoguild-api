using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Mvc;
using MotoGuild_API.Dto.FeedDtos;
using MotoGuild_API.Dto.PostDtos;
using MotoGuild_API.Dto.UserDtos;
using MotoGuild_API.Repository.Interface;

namespace MotoGuild_API.Controllers;

[ApiController]
[Route("api/feeds")]
public class FeedController : ControllerBase
{
    private readonly IFeedRepository _feedRepository;
    private readonly IMapper _mapper;

    public FeedController(IFeedRepository feedRepository, IMapper mapper)
    {
        _feedRepository = feedRepository;
        _mapper = mapper;
    }

    [HttpGet("{id:int}", Name = "GetFeed")]
    public IActionResult GetFeed(int id)
    {
        var feed = _feedRepository.Get(id);
        if (feed == null) return NotFound();
        return Ok(_mapper.Map<FeedDto>(feed));
    }

    [HttpPost]
    public IActionResult CreateFeed()
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);


        var feed = new Feed
        {
            Posts = new List<Post>()
        };

        _feedRepository.Insert(feed);
        _feedRepository.Save();
        var feedDto = _mapper.Map<FeedDto>(feed);
        return CreatedAtRoute("GetFeed", new {id = feedDto.Id}, feedDto);
    }

    private FeedDto GetFeedDto(Feed feed)
    {
        var postsDtos = new List<PostDto>();
        foreach (var post in feed.Posts)
        {
            var authorDto = new UserDto
            {
                Email = post.Author.Email,
                Id = post.Author.Id,
                Rating = post.Author.Rating,
                UserName = post.Author.UserName
            };

            postsDtos.Add(new PostDto
            {
                Author = authorDto,
                Content = post.Content,
                CreateTime = post.CreateTime,
                Id = post.Id
            });
        }

        var feedDto = new FeedDto {Id = feed.Id, Posts = postsDtos};
        return feedDto;
    }
}