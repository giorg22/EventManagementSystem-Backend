public interface IUserRepository : IBaseRepository<User, int>
{
    Task<User?> GetByEmailAsync(string email);
    Task<bool> EmailExistsAsync(string email);
}