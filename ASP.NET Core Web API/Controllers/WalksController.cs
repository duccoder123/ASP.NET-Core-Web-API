using ASP.NET_Core_Web_API.Models.Domain;
using ASP.NET_Core_Web_API.Models.DTO;
using ASP.NET_Core_Web_API.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalkRepository _walkRepository;
        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            _mapper = mapper;
            _walkRepository = walkRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDTO addWalkRequestDTO)
        {
            if(ModelState.IsValid)
            {
                var walk = _mapper.Map<Walk>(addWalkRequestDTO);
                await _walkRepository.CreateAsync(walk);
                return Ok(_mapper.Map<WalkDTO>(walk));
            }
            else
            {
                return BadRequest(ModelState);
            }
          
        }

        // GET : /api/walks?filterOn=Name&filterQuery=Track&sortBy=Name&isAscending=true&pageNumber=16&pageSize=5
        [HttpGet]
        public async Task<IActionResult> GetAll([FromRoute] string? filterOn, [FromRoute] string? filterQuery,
            [FromRoute] string? sortBy, [FromRoute] bool? isAscending,
            [FromRoute] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var walk = await _walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber,pageSize);
            return Ok(_mapper.Map<IEnumerable<WalkDTO>>(walk));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetWalkById([FromRoute] Guid id)
        {
            var walkDomainModel = await _walkRepository.GetWalkByIdAsync(id);
            if(walkDomainModel == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<WalkDTO>(walkDomainModel));
        }

        [HttpPost]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDTO updateWalkRequestDTO)
        {
            if (ModelState.IsValid)
            {
                var walkDomainModel = _mapper.Map<Walk>(updateWalkRequestDTO);

                walkDomainModel = await _walkRepository.UpdateAsync(id, walkDomainModel);
                if (walkDomainModel == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<WalkDTO>(walkDomainModel));
            }
            else
            {
                return BadRequest(ModelState);
            }
           
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
        var walkDomainModel = await _walkRepository.DeleteAsync(id);
            if( walkDomainModel == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<WalkDTO>(walkDomainModel));
        }
    }

}
