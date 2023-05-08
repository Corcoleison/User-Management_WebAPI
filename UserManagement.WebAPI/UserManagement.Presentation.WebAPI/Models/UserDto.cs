

using UserManagement.Domain.Models.Enums;

namespace UserManagement.Presentation.WebAPI.Models
{
    public class UserDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public IEnumerable<PaymentMethodDto> PaymentMethods { get; set; } = new List<PaymentMethodDto>();
        
    }
}
