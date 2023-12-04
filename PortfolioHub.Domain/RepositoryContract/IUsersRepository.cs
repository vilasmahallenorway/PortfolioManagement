using PortfolioHub.Domain.Models;

namespace PortfolioHub.Domain.RepositoryContract
{
    public interface IUsersRepository
    {
        Task<Users> AddUserAsync(Users userCreationRequest);
        Task DeleteUserByIdAsync(Guid userId);
        Task<List<Users>> GetAllUsersAsync();
        Task<Users?> GetUserByIdAsync(Guid userId);
    }
}
