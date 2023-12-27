using Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace WebAPI.Data;

public class DataAccess : IDataAccess
{
    private readonly KinderGardenContext context;

    public DataAccess(KinderGardenContext context)
    {
        this.context = context;
    }

    public async Task<Child> AddChildAsync(Child child)
    {
        try
        {
            if (child.Age < 3 || child.Age > 6) throw new Exception("Child age must be between 3 and 6");
            if (child.Name.Length > 50) throw new Exception("Child name must be less than 50 characters");

            EntityEntry<Child> addedChild = await context.Childs.AddAsync(child);
            await context.SaveChangesAsync();
            return addedChild.Entity;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw new Exception(e.Message);
        }
    }

    public async Task<ICollection<Child>> GetAllChilds(bool? favouriteFilerValue, string? genderFilter)
    {
        try
        {
            IQueryable<Child> allChilds = context.Childs.Include(child => child.AllToys).AsQueryable();

            if (favouriteFilerValue != null)
            {
                allChilds = allChilds.Select(child => new Child()
                {
                    Id = child.Id,
                    Age = child.Age,
                    Name = child.Name,
                    Gender = child.Gender,
                    AllToys = child.AllToys!.Where(toy => toy.IsFavourite == favouriteFilerValue).ToList()
                });
            }

            if (genderFilter != null)
            {
                allChilds = allChilds.Where(child => child.Gender.ToLower().Equals(genderFilter.ToLower()));
            }
            
            return allChilds.ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception(e.Message);
        }
    }

    public async Task CreateToyAsync(int selectedChildId, Toy toCreateToy)
    {
        try
        {
            Child child = (await context.Childs.Include(child => child.AllToys)
                .FirstOrDefaultAsync(child => child.Id == selectedChildId))!;
            child.AllToys!.Add(toCreateToy);
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception(e.Message);
        }
    }

    public async Task RemoveToy(int toyId)
    {
        try
        {
            Toy? toDeleteToy = await context.Toys.FindAsync(toyId);

            if (toDeleteToy is null) throw new Exception("Cannot find toy with id!!");

            context.Toys.Remove(toDeleteToy);
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception(e.Message);
        }
    }
}