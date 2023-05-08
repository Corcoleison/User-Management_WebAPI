using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Models.Enums;

namespace UserManagement.Domain.Models
{
    /// <summary>
    /// The user with his payment methods
    /// </summary>
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public ICollection<PaymentMethod> PaymentMethods { get; set; } = new List<PaymentMethod>();

    }
}
