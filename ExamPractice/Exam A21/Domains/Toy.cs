using System.ComponentModel.DataAnnotations;

namespace Domains;

public class Toy
{
    [Key]
    public int Id { get; set; }
    [MaxLength(20)]
    public string Name { get; set; }
    public string? Color { get; set; }
    public string? Condition { get; set; }
    public bool? IsFavourite { get; set; }
    
}