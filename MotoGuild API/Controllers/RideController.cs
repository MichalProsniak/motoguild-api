using System.Text.Json;
using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Mvc;
using MotoGuild_API.Dto.RideDtos;
using MotoGuild_API.Helpers;
using MotoGuild_API.Repository.Interface;

namespace MotoGuild_API.Controllers;

[ApiController]
[Route("api/rides")]
public class RideController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IRideRepository _rideRepository;
    private readonly ILoggedUserRepository _loggedUserRepository;

    public RideController(IRideRepository rideRepository, IMapper mapper, ILoggedUserRepository loggedUserRepository)
    {
        _rideRepository = rideRepository;
        _mapper = mapper;
        _loggedUserRepository = loggedUserRepository;

    }

    [HttpGet]
    public IActionResult GetRides([FromQuery] PaginationParams @params)
    {
        var paginationMetadata = new PaginationMetadata(_rideRepository.TotalNumberOfRides(), @params.Page,
            @params.ItemsPerPage);
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
        var rides = _rideRepository.GetAll(@params);
        return Ok(_mapper.Map<List<RideDto>>(rides));
    }

    [HttpGet("{rideId:int}", Name = "GetRide")]
    public IActionResult GetRide(int rideId)
    {
        var ride = _rideRepository.Get(rideId);
        if (ride == null) return NotFound();
        return Ok(_mapper.Map<RideDto>(ride));
    }

    [HttpPost]
    public IActionResult CreateRide([FromBody] CreateRideDto createRideDto, int rideId)
    {
        var userName = _loggedUserRepository.GetLoggedUserName();
        var ride = _mapper.Map<Ride>(createRideDto);
        _rideRepository.Insert(ride, userName);
        _rideRepository.Save();
        var rideDto = _mapper.Map<RideDto>(ride);
        return CreatedAtRoute("GetRide", new {id = rideDto.Id, rideId}, rideDto);
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteRide(int id)
    {
        var ride = _rideRepository.Get(id);
        if (ride == null) return NotFound();
        _rideRepository.Delete(id);
        _rideRepository.Save();
        return NoContent();
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateRide(int id, [FromBody] UpdateRideDto updateRideDto)
    {
        var ride = _rideRepository.Get(id);
        if (ride == null) return NotFound();
        _mapper.Map(updateRideDto, ride);
        _rideRepository.Update(ride);
        _rideRepository.Save();
        return NoContent();
    }
}