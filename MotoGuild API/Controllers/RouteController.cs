using System.Text.Json;
using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Mvc;
using MotoGuild_API.Dto.RouteDtos;
using MotoGuild_API.Helpers;
using MotoGuild_API.Repository.Interface;
using Route = Domain.Route;

namespace MotoGuild_API.Controllers;

[ApiController]
[Route("api/routes")]
public class RouteController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IRouteRepository _routeRepository;
    private readonly ILoggedUserRepository _loggedUserRepository;

    public RouteController(IRouteRepository routeRepository, IMapper mapper, ILoggedUserRepository loggedUserRepository)
    {
        _routeRepository = routeRepository;
        _mapper = mapper;
        _loggedUserRepository = loggedUserRepository;
    }

    [HttpGet]
    public IActionResult GetRoutes([FromQuery] PaginationParams @params, [FromQuery] bool orderByRating = false)
    {
        var paginationMetadata = new PaginationMetadata(_routeRepository.TotalNumberOfRoutes(), @params.Page,
            @params.ItemsPerPage);
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
        if (!orderByRating)
        {
            var routes = _routeRepository.GetAll(@params);
            return Ok(_mapper.Map<List<RouteDto>>(routes));
        }

        var selectedRoutes = _routeRepository.GetFiveOrderByRating(@params);
        return Ok(_mapper.Map<List<RouteDto>>(selectedRoutes));
    }

    [HttpGet("all")]
    public IActionResult GetAllRoutes()
    {
        var selectedRoutes = _routeRepository.GetAllWithoutPagination();
        return Ok(_mapper.Map<List<RouteDto>>(selectedRoutes));
    }

    [HttpGet("{id:int}", Name = "GetRoute")]
    public IActionResult GetRoute(int id)
    {
        var route = _routeRepository.Get(id);
        if (route == null) return NotFound();
        return Ok(_mapper.Map<FullRouteDto>(route));
    }

    [HttpPost]
    public IActionResult CreateRoute([FromBody] CreateRouteDto createRouteDto)
    {
        var userName = _loggedUserRepository.GetLoggedUserName();
        var route = _mapper.Map<Route>(createRouteDto);
        _routeRepository.Insert(route, userName);
        _routeRepository.Save();
        var routeDto = _mapper.Map<FullRouteDto>(route);
        return CreatedAtRoute("GetRoute", new {id = routeDto.Id}, routeDto);
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteRoute(int id)
    {
        var route = _routeRepository.Get(id);
        if (route == null) return NotFound();

        _routeRepository.Delete(id);
        _routeRepository.Save();
        return Ok();
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateRoute(int id, [FromBody] UpdateRouteDto updateRouteDto)
    {
        updateRouteDto.Id = id;
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var updateRoute = _mapper.Map<Route>(updateRouteDto);
        _routeRepository.Update(updateRoute);
        _routeRepository.Save();
        return NoContent();
    }
}