using Domains;

namespace WebAPI.Data;

public interface IDataAccess
{
    Task<Child> AddChildAsync(Child child);
    Task CreateToyAsync(int selectedChildId, Toy toCreateToy);
    Task RemoveToy(int toyId);
    Task<ICollection<Child>> GetAllChilds(bool? favouriteFilerValue, string? genderFilter);
}