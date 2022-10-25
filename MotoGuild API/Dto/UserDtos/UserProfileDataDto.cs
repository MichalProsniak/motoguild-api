using Domain;

namespace MotoGuild_API.Dto.UserDtos
{
    public class UserProfileDataDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Pictrue { get; set; }
        public int? PhoneNumber { get; set; }
        public double Rating { get; set; }

    }
}
