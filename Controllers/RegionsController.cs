using AutoMapper;
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
        private IMapper _mapper; 
        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this._mapper = mapper; 
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regionsDomain = await regionRepository.GetAllAsync(); 
            //map domain model to  dto 
            var regionsDto = _mapper.Map<List<RegionDTO>>(regionsDomain); 
            
            return Ok(regionsDto); 
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> getById([FromRoute] Guid id)
        {
            var regionDomain = await regionRepository.GetByIdAsync(id); 
            if (regionDomain == null)
            {
                return NotFound(); 
            }

            var regionDto = _mapper.Map<RegionDTO>(regionDomain); 
            return Ok(regionDto); 
        }

        [HttpPost]
        public async Task<IActionResult> create([FromBody] AddRegionRequestDTO addRegionRequestDto)
        {
            var regionDomainModel = _mapper.Map<Region>(addRegionRequestDto); 

            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel); 

            var newRegionDTO = _mapper.Map<RegionDTO>(regionDomainModel); 
            return CreatedAtAction(nameof(getById),
                new { id = regionDomainModel.Id }, newRegionDTO
                ); 

        }
        
        
        [HttpPut]
        [Route("{id:Guid}")]
        public  async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var regionDomainModel = _mapper.Map<Region>(updateRegionRequestDto); 
            regionDomainModel =  await regionRepository.UpdateAsync(id, regionDomainModel);
            if (regionDomainModel == null)
            {
                return NotFound(); 
            }

            var regionDto = _mapper.Map<RegionDTO>(regionDomainModel); 
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
