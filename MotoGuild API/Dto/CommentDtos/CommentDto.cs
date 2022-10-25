using MotoGuild_API.Dto.UserDtos;

namespace MotoGuild_API.Dto.CommentDtos;

public class CommentDto
{
    public int Id { get; set; }
    public UserDto Author { get; set; }
    public DateTime CreateTime { get; set; }
    public string Content { get; set; }
}