using MotoGuild_API.Dto.UserDtos;

namespace MotoGuild_API.Dto.PostDtos;

public class CreatePostDto
{
    public UserDto Author { get; set; }
    public string Content { get; set; }
    public DateTime CreateTime { get; set; }
}