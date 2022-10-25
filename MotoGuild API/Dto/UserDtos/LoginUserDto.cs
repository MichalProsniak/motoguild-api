using System.ComponentModel.DataAnnotations;

namespace MotoGuild_API.Dto.UserDtos;

public class LoginUserDto
{
    [Required] [MaxLength(32)] 
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }

}