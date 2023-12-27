using Domains;

namespace BlazorServerApp.Data;

public interface IChildService
{
    Task<Child> CreateAsync(Child toCreateChild);
    Task<ICollection<Child>> GetAllChilds(bool? favouriteFilerValue = null, string? genderFilter = null);
    Task DeleteToyAsync(int toyId);
}