using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Models;
using UserManagement.Domain.Models.Enums;

namespace UserManagement.Domain.RepositoryInterfaces
{
    /// <summary>
    /// Interface for the user
    /// </summary>
    public interface IUserRepository
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
    }
}
