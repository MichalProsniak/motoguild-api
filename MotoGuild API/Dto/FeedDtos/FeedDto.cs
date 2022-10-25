using MotoGuild_API.Dto.PostDtos;

namespace MotoGuild_API.Dto.FeedDtos;

public class FeedDto
{
    public int Id { get; set; }
    public ICollection<PostDto> Posts { get; set; }
}