using ASP.NET_Core_Web_API.Models.Domain;

namespace ASP.NET_Core_Web_API.Repositories
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walk>> GetAllAsync();
        Task<Walk> CreateAsync(Walk walk);
        Task<Walk> GetWalkByIdAsync(Guid id);
        Task<Walk> UpdateAsync(Guid id,Walk walk);
        Task<Walk> DeleteAsync(Guid id);

    }

}
