
using web_based_Bug_Management_Dashboard.Models.Domain;

namespace web_based_Bug_Management_Dashboard.Repositories.Interface
{
    public interface IBugRepository
    {
        Task<Bug> CreateAsync(Bug bug);
        Task<IReadOnlyList<Bug>> GetAllAsync(BugStatus? status = null);
        Task<Bug?> GetByIdAsync(Guid id);
        Task<Bug?> UpdateAsync(Guid id, Bug bug);
        Task<Bug?> DeleteAsync(Guid id);
    }
}
