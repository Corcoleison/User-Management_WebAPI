using UserManagement.Domain.Models;

namespace UserManagement.Domain.RepositoryInterfaces
{
    /// <summary>
    /// Interface for the PaymentMethod
    /// </summary>
    public interface IPaymentMethodRepository
    {
        /// <summary>
        /// Gets all PaymentMethods
        /// </summary>
        /// <returns></returns>
        Task<ICollection<PaymentMethod>> GetAllPaymentMethods();

        /// <summary>
        /// Gets PaymentMethod by id
        /// </summary>
        /// <returns></returns>
        Task<PaymentMethod?> GetPaymentMethod(int id);

        /// <summary>
        /// Creates PaymentMethod with parameters
        /// </summary>
        /// <returns></returns>
        Task<PaymentMethod> CreatePaymentMethod(PaymentMethod paymentMethod);

        /// <summary>
        /// Updates a PaymentMethod with parameters
        /// </summary>
        /// <returns></returns>
        Task<PaymentMethod?> UpdatePaymentMethod(PaymentMethod paymentMethod);

        /// <summary>
        /// Deletes a PaymentMethod by id
        /// </summary>
        /// <returns></returns>
        Task<PaymentMethod?> DeletePaymentMethod(int id);

        /// <summary>
        /// Gets all payments methods of a user by his id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ICollection<PaymentMethod>> GetAllPaymentMethodsByUserId(int userId);
    }
}
