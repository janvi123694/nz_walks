namespace nz_walks.Repositories;
using NZWalks.Models.Domain;
public interface IWalkRepository
{
    Task<Walk> CreateAsync(Walk walk);
    Task<List<Walk>> GetAlAsync(string? filterOn = null, string? filterQuery = null,
    string?sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000); 
    
    // get walk by id 
    Task<Walk?> GetByIdAsync(Guid id);
    Task<Walk?> UpdateAsync(Guid id, Walk walk); 
    
    //delete walkby id
    Task<Walk?> DeleteAsync(Guid id); 

}