public interface IPaymentService
{
    Task<string> ProcessPaymentAsync(decimal amount);
}