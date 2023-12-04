using Microsoft.EntityFrameworkCore;
using PortfolioHub.Domain.Models;
using PortfolioHub.Domain.RepositoryContract;

namespace PortfolioHub.Infrastructure.Efcore.RepositoryProvider
{
    public class UserRepository : Repository<Users>, IUsersRepository
    {
        public UserRepository(PortfolioHubContext portfolioHubContext) : base(portfolioHubContext) { }

        /// <summary>
        /// Create new user record in database 
        /// </summary>
        /// <param name="userCreationRequest"></param>
        /// <returns></returns>
        public async Task<Users> AddUserAsync(Users userCreationRequest)
        {
            await AddAsync(userCreationRequest);
            await SaveChangesAsync();
            return userCreationRequest;
        }

        /// <summary>
        /// Delete user from DB based on user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task DeleteUserByIdAsync(Guid userId)
        {
            var user = await GetUserByIdAsync(userId) ?? throw new Exception($"user not exist with user Id => {userId}.");
            Delete(user);
            await SaveChangesAsync();
        }

        /// <summary>
        /// fetch all users from database
        /// </summary>
        /// <returns></returns>
        public async Task<List<Users>> GetAllUsersAsync()
        {
            return await GetAll().ToListAsync();
        }

        /// <summary>
        /// fetch user details based on Id(PKey)
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Users?> GetUserByIdAsync(Guid userId)
        {
            return await FindByPredicate(x => x.Id == userId).FirstOrDefaultAsync();
        }
    }
}