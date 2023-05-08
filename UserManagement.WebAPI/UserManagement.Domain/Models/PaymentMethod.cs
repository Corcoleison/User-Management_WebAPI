using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Models.Enums;

namespace UserManagement.Domain.Models
{
    /// <summary>
    /// The Payment Method
    /// </summary>
    public class PaymentMethod
    {
        public int Id { get; set; }
        public PaymentType PaymentType { get; set; }
        public string AdditionalInfo { get; set; } = string.Empty;
        public bool Default { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
