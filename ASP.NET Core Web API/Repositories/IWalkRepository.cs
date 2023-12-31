﻿using ASP.NET_Core_Web_API.Models.Domain;

namespace ASP.NET_Core_Web_API.Repositories
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAccending = true, int pageNumber =1, int pageSize = 1000);   
        Task<Walk> CreateAsync(Walk walk);
        Task<Walk> GetWalkByIdAsync(Guid id);
        Task<Walk> UpdateAsync(Guid id,Walk walk);
        Task<Walk> DeleteAsync(Guid id);

    }

}
