using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Models;

namespace UserManagement.Data
{
    public class UserManagementContext : DbContext
    {
        public UserManagementContext(DbContextOptions<UserManagementContext> options) : base(options) {
            
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<PaymentMethod> PaymentMethods => Set<PaymentMethod>();
    }
}
