using ASP.NET_Core_Web_API.Data;
using ASP.NET_Core_Web_API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Core_Web_API.Repositories
{
    public class SQLWalkReposiory : IWalkRepository
    {
        private readonly ApplicationDbContext _db;
        public SQLWalkReposiory(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Walk> CreateAsync(Walk walk)
        {
           await _db.Walks.AddAsync(walk);
            await _db.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> DeleteAsync(Guid id)
        {
            var existingItem = _db.Walks.FirstOrDefault(x => x.Id == id);
            if(existingItem == null)
            {
                return null;
            }
            _db.Walks.Remove(existingItem);
            await _db.SaveChangesAsync();
            return existingItem;
        }

        public async Task<IEnumerable<Walk>> GetAllAsync()
        {
            return await _db.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk> GetWalkByIdAsync(Guid id)
        {
            return await _db.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Walk> UpdateAsync(Guid id,Walk walk)
        {
            var walkFrmDb = await _db.Walks.FirstOrDefaultAsync(u => u.Id == id);
            if(walkFrmDb == null)
            {
                return null;
            }
            walkFrmDb.Name = walk.Name;
            walkFrmDb.Description = walk.Description;
            walkFrmDb.LengthInKm = walk.LengthInKm;
            walkFrmDb.WalkImageUrl = walk.WalkImageUrl;
            walkFrmDb.DifficultyId = walk.DifficultyId;
            walkFrmDb.RegionId = walk.RegionId;
            await _db.SaveChangesAsync();

            return walkFrmDb;   


        }
    }
}
