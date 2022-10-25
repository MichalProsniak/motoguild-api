using MotoGuild_API.Dto.UserDtos;

namespace MotoGuild_API.Dto.GroupDtos
{
    public class GroupUserProfilDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public UserDto Owner { get; set; }
        public DateTime? CreationDate { get; set; }
        public double Rating { get; set; }
      
    }
}
