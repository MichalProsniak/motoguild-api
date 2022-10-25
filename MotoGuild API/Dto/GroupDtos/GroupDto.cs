using MotoGuild_API.Dto.PostDtos;
using MotoGuild_API.Dto.UserDtos;

namespace MotoGuild_API.Dto.GroupDtos;

public class GroupDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public UserDto Owner { get; set; }
    public bool IsPrivate { get; set; }
    public DateTime CreationDate { get; set; }
    public List<UserDto> Participants { get; set; }
    public List<UserDto> PendingUsers { get; set; }
    public List<PostDto> Posts { get; set; }
    public double Rating
    {
        get { return Participants.Average(u => u.Rating); }
        set
        {
            if (value < 0 || value > 5)
                throw new ArgumentOutOfRangeException(nameof(value),
                    "The valid rating is between 0 and 5.");
        }
    }
}

public class SelectedGroupDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public UserDto Owner { get; set; }
    public bool IsPrivate { get; set; }
    public string GroupImage { get; set; }
    public DateTime CreationDate { get; set; }
    public List<UserDto> Participants { get; set; }
    public List<UserDto> PendingUsers { get; set; }
    public double Rating
    {
        get { return Participants.Average(u => u.Rating); }
        set
        {
            if (value < 0 || value > 5)
                throw new ArgumentOutOfRangeException(nameof(value),
                    "The valid rating is between 0 and 5.");
        }
    }
}