using System.ComponentModel.DataAnnotations;

namespace Domains;

public class Image
{
    [Key] public int Id { get; set; }
    [Required, MaxLength(25)] public string Title { get; set; }
    public string? Description { get; set; }
    [Required] public string URL { get; set; }
    [Required] public string Topic { get; set; }
}