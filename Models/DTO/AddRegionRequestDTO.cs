using System.ComponentModel.DataAnnotations;

namespace nz_walks.Models.DTO;

public class AddRegionRequestDTO
{
    [Microsoft.Build.Framework.Required]
    [MinLength(3 , ErrorMessage = "Code has to be min of 3 chars")]
    [MaxLength(3 , ErrorMessage = "Code has to be max of 3 chars")]
    public string Code { get; set; }
    
    
    [Microsoft.Build.Framework.Required]
    [MaxLength(100 , ErrorMessage = "Code has to be max of 100 chars")]
    public string Name { get; set; }

    public string? RegionImageUrl { get; set; }
    
     
}