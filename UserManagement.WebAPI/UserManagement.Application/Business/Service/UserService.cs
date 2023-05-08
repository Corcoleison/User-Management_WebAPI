using UserManagement.Application.Business.ServiceInterfaces;
using UserManagement.Domain.Models;
using UserManagement.Domain.Models.Enums;
using UserManagement.Domain.RepositoryInterfaces;

namespace UserManagement.Application.Business.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<User> CreateUser(User user)
        {
            UserManagementValidator.ValidatePaymentMethods(user.PaymentMethods);
            return _userRepository.CreateUser(user);
        }

        public Task<User?> DeleteUser(int id)
        {
            return _userRepository.DeleteUser(id);
        }

        public async Task<ICollection<User>> GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }

        public async Task<(ICollection<User>,int)> GetPaginatedUsers(int pageNumber, int pageSize)
        {
            var users = await _userRepository.GetAllUsers();

            //The method is also returning the totalRecors to avoid accessing a second time the database for this info
            var totalRecords = users.Count;

            //users paged
            var pagedUsers = users.Skip((pageNumber - 1) * pageSize)
               .Take(pageSize).ToList();

            //I return the only one that is Default
            foreach(var user in pagedUsers)
            {
                user.PaymentMethods = user.PaymentMethods.Where(x => x.Default == true).ToList();
            }

            return (pagedUsers, totalRecords);
        }

        public Task<User?> GetUser(int id)
        {
            return _userRepository.GetUser(id);
        }

        public Task<User?> UpdateUser(User user)
        {
            UserManagementValidator.ValidatePaymentMethods(user.PaymentMethods);
            return _userRepository.UpdateUser(user);
        }
    }
}
