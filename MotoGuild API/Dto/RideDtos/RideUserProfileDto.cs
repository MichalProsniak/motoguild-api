using MotoGuild_API.Dto.UserDtos;

namespace MotoGuild_API.Dto.RideDtos
{
    public class RideUserProfileDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public UserDto Owner { get; set; }
        public DateTime StartTime { get; set; }
        public int Estimation { get; set; }
        public int MinimumRating { get; set; }
    }
}
