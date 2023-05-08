using UserManagement.Domain.Models;
using UserManagement.Domain.Models.Enums;
using static UserManagement.Application.Business.Constants.Constants;

namespace UserManagement.Application.Business.Service
{
    public static class UserManagementValidator
    {
        public static void ValidatePaymentMethods(ICollection<PaymentMethod> paymentMethods)
        {
            var duplication = paymentMethods.GroupBy(x => x.PaymentType).Any(x => x.Count() > 1);
            if (duplication)
            {
                throw new InvalidOperationException(ErrorMessages.DuplicationPaymentError);
            }

            if(paymentMethods.Count > 5)
            {
                throw new InvalidOperationException(ErrorMessages.NumberOfPaymentaError);
            }

            if (paymentMethods.Where(x => x.Default == true).Count() >1 || !paymentMethods.Where(x => x.Default == true).Any())
            {
                throw new InvalidOperationException(ErrorMessages.DefaultPaymentError);
            }

            foreach (var paymentMethod in paymentMethods)
            {
                if (!Enum.IsDefined(typeof(PaymentType), paymentMethod.PaymentType)){
                    throw new InvalidOperationException(ErrorMessages.EnumPaymentTypeError);
                }
            }
        }
    }
}
