using Domains;

namespace BlazorServerApp.Data;

public interface IToyService
{
    Task CreateAsync(int selectedChildId, Toy toCreateToy);
}