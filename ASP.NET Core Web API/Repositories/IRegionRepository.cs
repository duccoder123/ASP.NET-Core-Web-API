using ASP.NET_Core_Web_API.Models.Domain;

namespace ASP.NET_Core_Web_API.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();
        Task<Region> GetRegionByIdAsync(Guid id);
        Task<Region> CreateAsync(Region region);
        Task<Region> UpdateAsync(Guid id,Region region);
        Task<Region> DeleteAsync(Guid id);

    }
}
