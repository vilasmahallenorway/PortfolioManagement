using PortfolioHub.Domain.Models;

namespace PortfolioHub.Application.Contracts
{
    public interface IUsersService
    {
        Task<Users> AddUserAsync(Users userCreationRequest);

        Task DeleteUserByUserIdAsync(Guid userId);

        Task<List<Users>> GetAllUsersAsync();

        Task<Users?> GetUserByIdAsync(Guid userId);
    }
}
