using System.Collections;
using Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace WebAPI.Data;

public class DataAccess : IDataAccess
{
    private readonly DatabaseContext context;

    public DataAccess(DatabaseContext context)
    {
        this.context = context;
    }

    public async Task<Album> AddAlbumAsync(Album album)
    {
        try
        {
            if (album.Title.Length > 15) throw new Exception("Title must be less than 15 characters!");
            if (album.Description!.Length > 15) throw new Exception("Description must be less than 40 characters!");

            EntityEntry<Album> createdAlbum = await context.Albums.AddAsync(album);
            await context.SaveChangesAsync();
            return createdAlbum.Entity;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception(e.Message);
        }
    }

    public async Task<ICollection<string>> GetAlbumTitlesAsync()
    {
        try
        {
            ICollection<string> allTitles = await context.Albums.Select(album => album.Title).ToListAsync();
            return allTitles;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception(e.Message);
        }
    }

    public async Task<Image> AddImageToAlbumAsync(string albumTitle, Image img)
    {
        try
        {
            Album? album = await context.Albums
                .FirstOrDefaultAsync(album => album.Title.Equals(albumTitle));
            album.AllImages.Add(img);
            await context.SaveChangesAsync();
            return img;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception(e.Message);
        }
    }

    public async Task<ICollection<Image>> GetImagesAsync(string? albumCreator, string? topic)
    {
        IQueryable<Album> allAlbums = context.Albums.Include(album => album.AllImages).AsQueryable();
        List<Image> query = await allAlbums.SelectMany(album => album.AllImages!).ToListAsync();
        if (albumCreator != null)
        {
            query = await allAlbums.Where(album => album.CreatedBy.ToLower().Equals(albumCreator.ToLower()))
                .SelectMany(album => album.AllImages!).ToListAsync();
        }

        if (topic != null)
        {
            query = query.Where(image => image.Topic.ToLower().Equals(topic.ToLower())).ToList();
        }

        return query;
    }
}