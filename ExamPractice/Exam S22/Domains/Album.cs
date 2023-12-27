using System.ComponentModel.DataAnnotations;

namespace Domains;

public class Album
{
    [Key, MaxLength(25)] public string Title { get; set; }
    [MaxLength(150)] public string? Description { get; set; }
    [Required] public string CreatedBy { get; set; }
    public ICollection<Image>? AllImages { get; set; } = new List<Image>();
}