using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using nz_walks.Models.DTO;
using nz_walks.Repositories;
using NZWalks.Models.Domain;

namespace nz_walks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalkRepository _walkRepository; 
        
        
        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this._mapper = mapper;
            this._walkRepository = walkRepository; 
        }
        //create walk
        //post /api/walks
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            // Map DTO to Domain Model
            var walkDomainModel = _mapper.Map<Walk>(addWalkRequestDto);

            await _walkRepository.CreateAsync(walkDomainModel);

            // Map Domain model to DTO
            return Ok(_mapper.Map<WalkDTO>(walkDomainModel));
        }
        
        //get all walks
        //api/walks
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walksDomainModel =  await _walkRepository.GetAlAsync();
            // map domain model to dto
            return Ok(_mapper.Map<List<WalkDTO>>(walksDomainModel)); 
        }
        
        //get walk by id
        // /api/walks/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await _walkRepository.GetByIdAsync(id);
            if (walkDomainModel == null)
            {
                return NotFound(); 
            }
            return Ok(_mapper.Map<WalkDTO>(walkDomainModel)); 
        }
        
        //update walk by id 
        // /api/walks/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {
            var walkDomainModel = _mapper.Map<Walk>(updateWalkRequestDto);
           // Console.WriteLine(walkDomainModel);
            walkDomainModel = await _walkRepository.UpdateAsync(id, walkDomainModel);
            if (walkDomainModel == null)
            {
                return NotFound(); 
            }

            return Ok(_mapper.Map<WalkDTO>(walkDomainModel));

        }
        
        
        //delete walk by by 
        // /api/walks/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedWalkDomainModel = await _walkRepository.DeleteAsync(id);
            if (deletedWalkDomainModel == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<WalkDTO>(deletedWalkDomainModel)); 
        }
    }
}
