using System.ComponentModel.DataAnnotations;

namespace MotoGuild_API.Dto.UserDtos;

public class CreateUserDto
{
    [Required] [MaxLength(32)] 
    public string UserName { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    [EmailAddress]
    public string Email { get; set; }

    public string Password { get; set; }

    [DataType(DataType.PhoneNumber)] public int? PhoneNumber { get; set; } = null;
}