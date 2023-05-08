using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Domain.Models.Enums
{
    /// <summary>
    /// The type of payments
    /// </summary>
    public enum PaymentType
    {
        AmericanExpress = 0,
        Visa = 1,
        MasterCard = 2,
        Paypal = 3,
        BankAccount = 4,
        PaySafeCard = 5,
    }
}
