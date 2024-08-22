using Microsoft.Build.Framework;

namespace nz_walks.Models.DTO;

public class ImageUploadRequestDto
{   
    [Required]
    public IFormFile File { get; set;  }
    
    [Required]
    public string FileName { get; set;  }
    
    public string? FileDescription { get; set;  }
}