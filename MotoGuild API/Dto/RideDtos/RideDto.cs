using MotoGuild_API.Dto.PostDtos;
using MotoGuild_API.Dto.RouteDtos;
using MotoGuild_API.Dto.UserDtos;

namespace MotoGuild_API.Dto.RideDtos;

public class RideDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public ICollection<UserDto>? Participants { get; set; } = new List<UserDto>();
    public RouteDto Route { get; set; }
    public DateTime StartTime { get; set; }
    public int Estimation { get; set; }
    public ICollection<PostDto> Posts { get; set; } = new List<PostDto>();
    public int MinimumRating { get; set; }
    public UserDto Owner { get; set; }
}