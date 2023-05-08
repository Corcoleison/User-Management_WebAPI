using System.Collections;
using System.ComponentModel.DataAnnotations;
using UserManagement.Application.Business.Service;
using UserManagement.Application.Business.ServiceInterfaces;
using UserManagement.Domain.Models;
using UserManagement.Domain.RepositoryInterfaces;

namespace PaymentMethodManagement.Application.Business.Service
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly IUserService _userService;

        public PaymentMethodService(IPaymentMethodRepository PaymentMethodRepository, IUserService userService)
        {
            _paymentMethodRepository = PaymentMethodRepository;
            _userService = userService;
        }

        public async Task<PaymentMethod?> CreatePaymentMethod(PaymentMethod paymentMethod)
        {
            //Look at payment methods of that user
            var user = await _userService.GetUser(paymentMethod.UserId);
            if(user == null)
            {
                return null;
            }

            //It gets added to know if there are duplications or more than 5 later in the validation
            user.PaymentMethods.Add(paymentMethod);

             UserManagementValidator.ValidatePaymentMethods(user.PaymentMethods);

            return await _paymentMethodRepository.CreatePaymentMethod(paymentMethod);
        }

        public Task<PaymentMethod?> DeletePaymentMethod(int id)
        {
            return _paymentMethodRepository.DeletePaymentMethod(id);
        }

        public async Task<ICollection<PaymentMethod>> GetAllPaymentMethods()
        {
            return await _paymentMethodRepository.GetAllPaymentMethods();
        }

        public async Task<ICollection<PaymentMethod>> GetAllPaymentMethodsByUserId(int userId)
        {
            return await _paymentMethodRepository.GetAllPaymentMethodsByUserId(userId);
        }

        public Task<PaymentMethod?> GetPaymentMethod(int id)
        {
            return _paymentMethodRepository.GetPaymentMethod(id);
        }

        public async Task<PaymentMethod?> UpdatePaymentMethod(PaymentMethod paymentMethod)
        {
            //Look at actual payment to get userId
            var paymentList = await GetNewUpdatedList(paymentMethod);
            if(paymentList == null)
            {
                return null;
            }

            //Check again the list to search for duplicates
            UserManagementValidator.ValidatePaymentMethods(paymentList);

            //We are good to go and update it
            return await _paymentMethodRepository.UpdatePaymentMethod(paymentMethod);
        }

        private async Task<ICollection<PaymentMethod>?> GetNewUpdatedList(PaymentMethod paymentMethod)
        {
            var actualPayment = await _paymentMethodRepository.GetPaymentMethod(paymentMethod.Id);
            if (actualPayment == null)
            {
                return null;
            }

            //Gets all the payments of that user and modifies the one that exists
            var paymentList = await _paymentMethodRepository.GetAllPaymentMethodsByUserId(actualPayment.UserId);
            var paymentToChange = paymentList.FirstOrDefault(x => x.PaymentType == paymentMethod.PaymentType);
            if (paymentToChange == null)
            {
                return null;
            }
            paymentToChange.PaymentType = paymentMethod.PaymentType;

            return paymentList;
        }
    }
}
