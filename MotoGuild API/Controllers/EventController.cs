using System.Text.Json;
using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Mvc;
using MotoGuild_API.Dto.EventDtos;
using MotoGuild_API.Helpers;
using MotoGuild_API.Repository.Interface;

namespace MotoGuild_API.Controllers;

[ApiController]
[Route("api/events")]
public class EventController : ControllerBase
{
    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;
    private readonly ILoggedUserRepository _loggedUserRepository;

    public EventController(IEventRepository eventRepository, IMapper mapper, ILoggedUserRepository loggedUserRepository)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
        _loggedUserRepository = loggedUserRepository;
    }


    [HttpGet]
    public IActionResult GetEvents([FromQuery] PaginationParams @params)
    {
        var paginationMetadata = new PaginationMetadata(_eventRepository.TotalNumberOfEvents(), @params.Page,
            @params.ItemsPerPage);
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
        var events = _eventRepository.GetAll(@params);
        var eventsDto = _mapper.Map<IEnumerable<EventDto>>(events);
        return Ok(eventsDto);
    }

    [HttpGet("{id}", Name = "GetEvent")]
    public IActionResult GetEvent(int id, [FromQuery] bool includePosts = false)
    {
        var eve = _eventRepository.Get(id);
        if (eve == null) return NotFound();
        return Ok(_mapper.Map<EventDto>(eve));
    }

    [HttpPost]
    public IActionResult CreateEvent([FromBody] CreateEventDto createEventDto)
    {
        var userName = _loggedUserRepository.GetLoggedUserName();
        var eve = _mapper.Map<Event>(createEventDto);
        _eventRepository.Insert(eve);
        _eventRepository.Save();
        var eventDto = _mapper.Map<EventDto>(eve);
        return CreatedAtRoute("GetEvent", new {id = eve.Id}, eventDto);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateEvent(int id, [FromBody] UpdateEventDto updateEventDto)
    {
        var eve = _eventRepository.Get(id);
        if (eve == null) return NotFound();
        _mapper.Map(updateEventDto, eve);
        _eventRepository.Update(eve);
        _eventRepository.Save();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteEvent(int id)
    {
        var eve = _eventRepository.Get(id);
        if (eve == null) return NotFound();
        _eventRepository.Delete(id);
        _eventRepository.Save();
        return NoContent();
    }


}