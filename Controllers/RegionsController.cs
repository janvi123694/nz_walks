using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nz_walks.Models.DTO;
using nz_walks.Repositories;
using NZWalks.Data;
using NZWalks.Models.Domain;

namespace nz_walks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository; 
        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository; 
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regionsDomain = await regionRepository.GetAllAsync();

            var regionsDto = new List<RegionDTO>();

            foreach (var region in (regionsDomain))
            {
                regionsDto.Add(new RegionDTO()
                {
                    Id = region.Id, 
                    RegionImageUrl = region.RegionImageUrl, 
                    Name = region.RegionImageUrl, 
                    Code = region.RegionImageUrl
                        
                });
                
            }
            return Ok(regionsDomain); 
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> getById([FromRoute] Guid id)
        {
            var regionDomain = await regionRepository.GetByIdAsync(id); 
            if (regionDomain == null)
            {
                return NotFound(); 
            }

            var regionDto = new RegionDTO()
            {
                Id = regionDomain.Id,
                RegionImageUrl = regionDomain.RegionImageUrl,
                Name = regionDomain.RegionImageUrl,
                Code = regionDomain.RegionImageUrl
            }; 
            return Ok(regionDto); 
        }

        [HttpPost]
        public async Task<IActionResult> create([FromBody] AddRegionRequest addRegionRequestDto)
        {
            var regionDomainModel = new Region()
            {
                Code = addRegionRequestDto.Code, 
                RegionImageUrl = addRegionRequestDto.RegionImageUrl,
                Name = addRegionRequestDto.Name
            };

            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel); 

            var newRegionDTO = new RegionDTO()
            {
                Id = regionDomainModel.Id, 
                RegionImageUrl = regionDomainModel.RegionImageUrl,
                Name = regionDomainModel.Name, 
                Code = regionDomainModel.Code
            }; 
            
            return CreatedAtAction(nameof(getById),
                new { id = regionDomainModel.Id }, newRegionDTO
                ); 

        }
        
        
        [HttpPut]
        [Route("{id:Guid}")]
        public  async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var regionDomainModel = new Region()
            {
                Code = updateRegionRequestDto.Code,
                Name = updateRegionRequestDto.Name,
                RegionImageUrl = updateRegionRequestDto.RegionImageUrl
            }; 
            regionDomainModel =  await regionRepository.UpdateAsync(id, regionDomainModel);
            if (regionDomainModel == null)
            {
                return NotFound(); 
            }

            var regionDto = new RegionDTO()
            {
                Id = regionDomainModel.Id, RegionImageUrl = regionDomainModel.RegionImageUrl,
                Code = regionDomainModel.Code, Name = regionDomainModel.Name
            };
            return Ok(regionDto);
        }
        
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id); 
            if (regionDomainModel == null)
            {
                return NotFound(); 
            }
            
            return Ok("successfully deleted"); 
        }
        
    }
}
