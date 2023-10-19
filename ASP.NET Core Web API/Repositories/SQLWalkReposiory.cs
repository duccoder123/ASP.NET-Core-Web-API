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

        public async Task<IEnumerable<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy =null, bool isAscending = true, int pageNumber=1, int pageSize = 1000)
        {
            var walks = _db.Walks.Include("Difficulty").Include("Region").AsQueryable();
            //Filtering
            if(string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false) 
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
               
            }
            // Sortig
            if(string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("LengthByKm", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }
            // Pagination
            var skipResults = (pageNumber - 1) * pageSize;



            // skip để bỏ qua một vài phần tử cụ thể 
            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
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
