using MotoGuild_API.Dto.StopDtos;
using MotoGuild_API.Dto.UserDtos;

namespace MotoGuild_API.Dto.RouteDtos;

public class CreateRouteDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string StartPlace { get; set; }
    public string EndingPlace { get; set; }
    public int Rating { get; set; }
    public UserDto Owner { get; set; }

    public List<CreateStopDto> Stops { get; set; }

}