using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Models;
using UserManagement.Domain.Models.Enums;
using UserManagement.Domain.RepositoryInterfaces;

namespace UserManagement.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManagementContext _context;
        public UserRepository(UserManagementContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User?> DeleteUser(int id)
        {
            var userToDelete = await _context.Users.Include(u => u.PaymentMethods).FirstOrDefaultAsync(x => x.Id == id);
            if(userToDelete == null)
            {
                return null;
            }

             _context.Attach(userToDelete);
             _context.Remove(userToDelete);
            await _context.SaveChangesAsync();

            return userToDelete;
        }

        public async Task<ICollection<User>> GetAllUsers()
        {
            return await _context.Users.Include(u => u.PaymentMethods).ToListAsync();
        }

        public async Task<User?> GetUser(int id)
        {
            return await _context.Users.Include(u => u.PaymentMethods).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User?> UpdateUser(User user)
        {
            var userToUpdate = await _context.Users.Include(u => u.PaymentMethods).FirstOrDefaultAsync(x => x.Id == user.Id);
            if (userToUpdate == null)
            {
                return null;
            }

            userToUpdate.Id = userToUpdate.Id;
            userToUpdate.Email = user.Email;
            userToUpdate.Name = user.Name;
            userToUpdate.PaymentMethods = user.PaymentMethods;
            await _context.SaveChangesAsync();

            return user;
        }
    }
}
