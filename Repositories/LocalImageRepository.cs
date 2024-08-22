using NZWalks.Data;
namespace nz_walks.Repositories;
using NZWalks.Models.Domain;

public class LocalImageRepository : IImageRepository
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly NZWalksDbContext _dbContext; 
    
    public LocalImageRepository(IWebHostEnvironment webHostEnvironment, 
        IHttpContextAccessor httpContextAccessor, NZWalksDbContext nzWalksDbContext)
    {
        this._webHostEnvironment = webHostEnvironment;
        this._httpContextAccessor = httpContextAccessor;
        this._dbContext = nzWalksDbContext; 
    }
    
    public async Task<Image> Upload(Image image)
    {
        var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, 
            "Images", $"{image.FileName}{image.FileExtension}");
        
        //upload image to local folder
        using var stream = new FileStream(localFilePath, FileMode.Create);
        await image.File.CopyToAsync(stream);
        
        //url file path 
        var urlFilePath =
            $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";
        image.FilePath = urlFilePath;

        //save changes to db
        await _dbContext.Images.AddAsync(image);
        await _dbContext.SaveChangesAsync();
        return image; 

    }
    
}