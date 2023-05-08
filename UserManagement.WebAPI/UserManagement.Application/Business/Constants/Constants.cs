namespace UserManagement.Application.Business.Constants
{
    public static class Constants
    {
        public static class ErrorMessages
        {
            public const string DuplicationPaymentError = "User can not have duplicated payment methods";
            public const string NumberOfPaymentaError = "User can not have more than 5 Payments Methods";
            public const string DefaultPaymentError = "User must have one default payment";
            public const string EnumPaymentTypeError = "Incorrect Payment Type";
        }
    }
}
