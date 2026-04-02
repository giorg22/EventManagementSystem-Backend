
public class MockPaymentService : IPaymentService
{
    public async Task<string> ProcessPaymentAsync(decimal amount)
    {
        // იმიტაცია: თითქოს ბანკს დაუკავშირდა
        await Task.Delay(500);
        return "PAY-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
    }
}