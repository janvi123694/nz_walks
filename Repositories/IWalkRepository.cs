namespace nz_walks.Repositories;
using NZWalks.Models.Domain;
public interface IWalkRepository
{
    Task<Walk> CreateAsync(Walk walk);
    Task<List<Walk>> GetAlAsync(); 
    
    // get walk by id 
    Task<Walk?> GetByIdAsync(Guid id);
    Task<Walk?> UpdateAsync(Guid id, Walk walk); 
    
    //delete walkby id
    Task<Walk?> DeleteAsync(Guid id); 

}