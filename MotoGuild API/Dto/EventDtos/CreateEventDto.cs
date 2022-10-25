using System.ComponentModel.DataAnnotations;
using MotoGuild_API.Dto.PostDtos;
using MotoGuild_API.Dto.UserDtos;

namespace MotoGuild_API.Dto.EventDtos;

public class CreateEventDto
{
    [Required] public string Name { get; set; }

    [Required] public UserDto Owner { get; set; }

    [Required] public string Description { get; set; } = string.Empty;

    public ICollection<UserDto>? Participants { get; set; } = new List<UserDto>();

    [Required] public string Place { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime StartDate { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime StopDate { get; set; }

    public ICollection<PostDto> Posts { get; set; } = new List<PostDto>();
}