using web_based_Bug_Management_Dashboard.Data;
using web_based_Bug_Management_Dashboard.Models.Domain;
using web_based_Bug_Management_Dashboard.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace web_based_Bug_Management_Dashboard.Repositories.Implementaion
{
    public class BugRepository : IBugRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<BugRepository> logger;

        public BugRepository(ApplicationDbContext dbContext, ILogger<BugRepository> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public async Task<Bug> CreateAsync(Bug bug)
        {
            bug.Id = Guid.NewGuid();
            bug.CreatedAtUtc = DateTime.UtcNow;

            await dbContext.Bugs.AddAsync(bug);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Bug {BugId} created with status {Status}", bug.Id, bug.Status);
            return bug;
        }

        public async Task<IReadOnlyList<Bug>> GetAllAsync(BugStatus? status = null)
        {
            var query = dbContext.Bugs.AsNoTracking();

            if (status.HasValue)
            {
                query = query.Where(x => x.Status == status.Value);
            }

            return await query
                .OrderByDescending(x => x.CreatedAtUtc)
                .ToListAsync();
        }

        public async Task<Bug?> GetByIdAsync(Guid id)
        {
            return await dbContext.Bugs
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Bug?> UpdateAsync(Guid id, Bug bug)
        {
            var existingBug = await dbContext.Bugs.FirstOrDefaultAsync(x => x.Id == id);

            if (existingBug == null)
            {
                return null;
            }

            existingBug.Title = bug.Title;
            existingBug.Description = bug.Description;
            existingBug.Status = bug.Status;
            existingBug.ReporterName = bug.ReporterName;
            existingBug.AssignedTo = bug.AssignedTo;
            existingBug.UpdatedAtUtc = DateTime.UtcNow;

            await dbContext.SaveChangesAsync();

            logger.LogInformation("Bug {BugId} updated to status {Status}", existingBug.Id, existingBug.Status);
            return existingBug;
        }

        public async Task<Bug?> DeleteAsync(Guid id)
        {
            var existingBug = await dbContext.Bugs.FirstOrDefaultAsync(x => x.Id == id);

            if (existingBug == null)
            {
                return null;
            }

            dbContext.Bugs.Remove(existingBug);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Bug {BugId} deleted", existingBug.Id);
            return existingBug;
        }
    }
}
