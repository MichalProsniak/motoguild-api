using System.ComponentModel.DataAnnotations;
using MotoGuild_API.Dto.UserDtos;

namespace MotoGuild_API.Dto.GroupDtos;

public class UpdateGroupDto
{
    [Required] public int Id { get; set; }

    [Required] public string Name { get; set; }

    [Required] public string Description { get; set; }

    [Required] public UserDto Owner { get; set; }

    [Required] public bool IsPrivate { get; set; }
}