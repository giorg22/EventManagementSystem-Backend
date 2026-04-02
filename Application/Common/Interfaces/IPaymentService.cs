public interface IPaymentService
{
    // აბრუნებს ტრანზაქციის ID-ს წარმატების შემთხვევაში
    Task<string> ProcessPaymentAsync(decimal amount);
}