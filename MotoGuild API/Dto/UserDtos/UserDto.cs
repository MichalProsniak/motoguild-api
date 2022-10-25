namespace MotoGuild_API.Dto.UserDtos;

public class UserDto
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public double Rating { get; set; }
    public string? Image { get; set; }
    public override bool Equals(object? obj)
    {
        if (obj == null) return false;
        var other = obj as UserDto;
        return Id == other.Id;
    }
}