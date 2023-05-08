using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using UserManagement.Domain.Models;
using UserManagement.Domain.RepositoryInterfaces;

namespace UserManagement.Data.Repository
{
    public class PaymentMethodRepository : IPaymentMethodRepository
    {
        private readonly UserManagementContext _context;
        public PaymentMethodRepository(UserManagementContext context)
        {
            _context = context;
        }

        public async Task<PaymentMethod> CreatePaymentMethod(PaymentMethod paymentMethod)
        {
            _context.PaymentMethods.Add(paymentMethod);
            await _context.SaveChangesAsync();

            return paymentMethod;
        }

        public async Task<PaymentMethod?> DeletePaymentMethod(int id)
        {
            var actualPaymentMethod = await _context.PaymentMethods.FirstAsync(x => x.Id == id);

            if (actualPaymentMethod == null) {
                return null;
            }
            _context.Attach(actualPaymentMethod);
            _context.Remove(actualPaymentMethod);
            await _context.SaveChangesAsync();

            return actualPaymentMethod;
        }

        public async Task<ICollection<PaymentMethod>> GetAllPaymentMethods()
        {
            return await _context.PaymentMethods.Include(u => u.User).ToListAsync();
        }

        public async Task<ICollection<PaymentMethod>> GetAllPaymentMethodsByUserId(int userId)
        {
            return await _context.PaymentMethods.Include(u => u.User).Where(x => x.UserId == userId).ToListAsync();
        }


        public async Task<PaymentMethod?> GetPaymentMethod(int id)
        {
            return await _context.PaymentMethods.Include(u => u.User).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PaymentMethod?> UpdatePaymentMethod(PaymentMethod paymentMethod)
        {
            var paymentToDelete = await _context.PaymentMethods.FirstOrDefaultAsync(x => x.Id == paymentMethod.Id);
            if (paymentToDelete == null)
            {
                return null;
            }

            paymentToDelete.Id = paymentMethod.Id;
            paymentToDelete.PaymentType = paymentMethod.PaymentType;

            await _context.SaveChangesAsync();

            return paymentMethod;
        }
    }
}
