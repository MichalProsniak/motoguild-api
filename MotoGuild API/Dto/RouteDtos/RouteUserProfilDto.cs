using MotoGuild_API.Dto.StopDtos;
using MotoGuild_API.Dto.UserDtos;

namespace MotoGuild_API.Dto.RouteDtos
{
    public class RouteUserProfilDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public UserDto Owner { get; set; }
        public ICollection<StopDto> Stops { get; set; }
        public string StartPlace { get; set; }
        public string EndingPlace { get; set; }
        public int Rating { get; set; }
    }
}
