using PortfolioHub.Application.Contracts;
using PortfolioHub.Domain.Models;
using PortfolioHub.Domain.RepositoryContract;

namespace PortfolioHub.Application.Providers
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _userRepository;
        public UsersService(IUsersRepository userRepository)
        {
            this._userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        /// <summary>
        /// create new user record in DB
        /// </summary>
        /// <param name="userCreationRequest"></param>
        /// <returns></returns>
        public async Task<Users> AddUserAsync(Users userCreationRequest)
        {
            return await _userRepository.AddUserAsync(userCreationRequest);
        }

        /// <summary>
        /// delete user from DB by providing user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task DeleteUserByUserIdAsync(Guid userId)
        {
            await _userRepository.DeleteUserByIdAsync(userId);
        }

        /// <summary>
        /// fetch all users from database
        /// </summary>
        /// <returns></returns>
        public async Task<List<Users>> GetAllUsersAsync()
        {
           return  await _userRepository.GetAllUsersAsync();
        }

        /// <summary>
        /// fetch user details by id(pk) field
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Users?> GetUserByIdAsync(Guid userId)
        {
            return await _userRepository.GetUserByIdAsync(userId);
        }
    }
}
