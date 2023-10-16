using ASP.NET_Core_Web_API.Data;
using ASP.NET_Core_Web_API.Models.Domain;
using ASP.NET_Core_Web_API.Models.DTO;
using ASP.NET_Core_Web_API.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Core_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IRegionRepository _regionRepo;
        private readonly IMapper _mapper;
        public RegionsController(ApplicationDbContext db, IRegionRepository regionRepo, IMapper mapper)
        {
            _db =db;
            _regionRepo = regionRepo;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regionsDomain = await _regionRepo.GetAllAsync();

            //var regionsDto = new List<RegionDTO>();
            //foreach(var region in regionsDomain)
            //{
            //    regionsDto.Add(new RegionDTO()
            //    {
            //        Id = region.Id,
            //        Name = region.Name,
            //        Code = region.Code,
            //        RegionImageUrl = region.RegionImageUrl,
            //    });
            //}

            return Ok(_mapper.Map<List<RegionDTO>>(regionsDomain));
        }

        [HttpGet("{id:Guid}",Name ="GetRegion")]
        public async Task<IActionResult> GetRegion([FromRoute]Guid id)
        {
            var regionDomain = await _regionRepo.GetRegionByIdAsync(id);
            if(regionDomain == null)
            {
                return BadRequest();
            }
            var regionDto = new RegionDTO
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl,
            };
            return Ok(regionDto);
        }
        [HttpPost]
        public async Task<IActionResult> CreateRegion([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            var regionDomain = new Region
            {
                Code = addRegionRequestDTO.Code,
                Name = addRegionRequestDTO.Name,
                RegionImageUrl = addRegionRequestDTO.RegionImageUrl,
            };

           regionDomain = await  _regionRepo.CreateAsync(regionDomain);
            var regionDto = new RegionDTO
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl,
            };
            return CreatedAtAction(nameof(GetRegion), new {id = regionDto.Id}, regionDto);
        }


        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {
            var regionDomainModel = new Region()
            {
                RegionImageUrl = updateRegionRequestDTO.RegionImageUrl,
                Name = updateRegionRequestDTO.Name,
                Code = updateRegionRequestDTO.Code,

            };
             regionDomainModel =await _regionRepo.UpdateAsync(id, regionDomainModel);
            if(regionDomainModel == null)
            {
                return NotFound();
            }
         

            // convert domain Model to DTO

            var regionDto = new RegionDTO()
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };

            return Ok(regionDto);    
        }

        [HttpDelete]
        [Route("id:Guid")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
           var regionDomainModel = await _regionRepo.DeleteAsync(id);
            if(regionDomainModel == null)
            {
                return NotFound();
            }
           

            var regionDto = new RegionDTO()
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };
            return Ok(regionDto);
        }
    }
}
