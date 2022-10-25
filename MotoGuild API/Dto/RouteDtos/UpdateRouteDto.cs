using System.ComponentModel.DataAnnotations;

namespace MotoGuild_API.Dto.RouteDtos;

public class UpdateRouteDto
{
    public int Id { get; set; }

    [Required] [MaxLength(32)] public string Name { get; set; }

    [Required] [MaxLength(32)] public string StartPlace { get; set; }

    [Required] [MaxLength(32)] public string EndingPlace { get; set; }

    [Required] [MaxLength(32)] public string Description { get; set; }

    public int Rating { get; set; }
}