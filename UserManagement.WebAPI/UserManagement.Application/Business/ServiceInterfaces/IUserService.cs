using UserManagement.Domain.Models;

namespace UserManagement.Application.Business.ServiceInterfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Gets all users
        /// </summary>
        /// <returns></returns>
        Task<ICollection<User>> GetAllUsers();

        /// <summary>
        /// Gets user by id
        /// </summary>
        /// <returns></returns>
        Task<User?> GetUser(int id);

        /// <summary>
        /// Creates user with parameters
        /// </summary>
        /// <returns></returns>
        Task<User> CreateUser(User user);

        /// <summary>
        /// Updates a user with parameters
        /// </summary>
        /// <returns></returns>
        Task<User?> UpdateUser(User user);

        /// <summary>
        /// Deletes a user by id
        /// </summary>
        /// <returns></returns>
        Task<User?> DeleteUser(int id);

        /// <summary>
        /// Returns paginatedUsers and the total records
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<(ICollection<User>, int)> GetPaginatedUsers(int pageNumber, int pageSize);
    }
}
