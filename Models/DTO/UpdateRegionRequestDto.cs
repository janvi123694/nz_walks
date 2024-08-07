namespace nz_walks.Models.DTO;
using System.ComponentModel.DataAnnotations;

public class UpdateRegionRequestDto
{
    [Microsoft.Build.Framework.Required]
    [MinLength(3 , ErrorMessage = "Code has to be min of 3 chars")]
    [MaxLength(3 , ErrorMessage = "Code has to be max of 3 chars")]
    public string Code { get; set; }
   
    public string Name { get; set; }

    public string? RegionImageUrl { get; set; }
    
}