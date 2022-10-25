using System.ComponentModel.DataAnnotations;
using MotoGuild_API.Dto.RouteDtos;
using MotoGuild_API.Dto.UserDtos;

namespace MotoGuild_API.Dto.RideDtos;

public class CreateRideDto
{
    [Required] public string Name { get; set; }

    [Required] public string Description { get; set; }

    [Required] public RouteDto Route { get; set; }

    public DateTime StartTime { get; set; }
    public UserDto Owner { get; set; }
    public int MinimumRating { get; set; }
    public int Estimation { get; set; }

    public ICollection<UserDto>? Participants { get; set; } = new List<UserDto>();
}