using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Models.Domain;

namespace nz_walks.Repositories;

public class SQLWalkRepository : IWalkRepository
{
    private readonly NZWalksDbContext _dbContext; 
    public SQLWalkRepository(NZWalksDbContext dbContext)
    {
        this._dbContext = dbContext; 
    }
    public async Task<Walk> CreateAsync(Walk walk)
    {
        
        await _dbContext.Walks.AddAsync(walk);
        await _dbContext.SaveChangesAsync();
        return walk;
    }

    public async Task<List<Walk>> GetAlAsync()
    {
        return await _dbContext.Walks.Include("Difficulty")
            .Include("Region")
            .ToListAsync(); 
    }

    public async Task<Walk?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Walks
            .Include("Region")
            .Include("Difficulty")
            .FirstOrDefaultAsync(x => x.Id == id); 
    }

    public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
    {
        var existingWalk = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
        if (existingWalk == null)
        {
            return null; 
        }

        existingWalk.Name = walk.Name;
        existingWalk.WalkImageUrl = walk.WalkImageUrl;
        existingWalk.Description = walk.Description;
        existingWalk.LengthInKm = walk.LengthInKm;
        existingWalk.DifficultyId = walk.DifficultyId;
        existingWalk.RegionId = walk.RegionId;
        try
        {
            await _dbContext.SaveChangesAsync();
            Console.WriteLine("Update successful.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while updating the walk: {ex.Message}");
            // Optionally, log the exception or handle it as necessary
        }
        return existingWalk; 
;    }

    public async Task<Walk?> DeleteAsync(Guid id)
    {
        var existingWalk = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
        if (existingWalk == null)
        {
            return null; 
        }

        _dbContext.Walks.Remove(existingWalk);
        await _dbContext.SaveChangesAsync();
        return existingWalk; 
    }
}