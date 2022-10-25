using MotoGuild_API.Dto.UserDtos;

namespace MotoGuild_API.Dto.EventDtos
{
    public class EventUserProfileDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public UserDto Owner { get; set; }
        public string Place { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime StopDate { get; set; }
    }
}
