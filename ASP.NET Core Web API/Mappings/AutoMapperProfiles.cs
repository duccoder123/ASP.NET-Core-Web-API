using ASP.NET_Core_Web_API.Models.Domain;
using ASP.NET_Core_Web_API.Models.DTO;
using AutoMapper;

namespace ASP.NET_Core_Web_API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<AddRegionRequestDTO, Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDTO, Region>().ReverseMap();
            CreateMap<AddWalkRequestDTO, Walk>().ReverseMap();
            CreateMap<Walk, WalkDTO>().ReverseMap();
            CreateMap<Difficulty, DifficultyDTO>().ReverseMap();    
            CreateMap<UpdateWalkRequestDTO, Walk>().ReverseMap();    
        }
    }
}
