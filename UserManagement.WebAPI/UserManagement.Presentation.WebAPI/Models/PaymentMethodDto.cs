using UserManagement.Domain.Models.Enums;

namespace UserManagement.Presentation.WebAPI.Models
{
    /// <summary>
    /// The Payment Method
    /// </summary>
    public class PaymentMethodDto
    {
        public PaymentType PaymentType { get; set; }
        public string AdditionalInfo { get; set; } = string.Empty;
        public bool Default { get; set; }
    }
}
