using AutoMapper;
using nz_walks.Models.DTO;
using NZWalks.Models.Domain;

namespace nz_walks.Mappings;

public class AutomapperProfiles : Profile
{
    public AutomapperProfiles()
    {
        CreateMap<Region, RegionDTO>().ReverseMap();
        CreateMap<AddRegionRequestDTO, Region>().ReverseMap(); 
        CreateMap<UpdateRegionRequestDto, Region>().ReverseMap(); 
    }
}