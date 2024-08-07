using AutoMapper;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using nz_walks.Models.DTO;
using NZWalks.Models.Domain;

namespace nz_walks.Mappings;

public class AutomapperProfiles : Profile
{
    public AutomapperProfiles()
    {   //region mappings
        CreateMap<Region, RegionDTO>().ReverseMap();
        CreateMap<AddRegionRequestDTO, Region>().ReverseMap(); 
        CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();
        
        // walk mappings
        CreateMap<AddWalkRequestDto, Walk>().ReverseMap();
        CreateMap<Walk, WalkDTO>().ReverseMap();
        CreateMap<UpdateWalkRequestDto, Walk>().ReverseMap(); 
        
        // difficulty mappings 
        CreateMap<Difficulty, DifficultyDTO>().ReverseMap(); 
        
    }
}