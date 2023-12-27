using System.ComponentModel.DataAnnotations;

namespace Domains;

public class Child
{
    [Key] public int Id { get; set; }
    [Required, MaxLength(50)] public string Name { get; set; }
    [Required] public string Gender { get; set; }
    [Required, Range(3, 6)] public int Age { get; set; }
    public ICollection<Toy>? AllToys { get; set; } = new List<Toy>();
}