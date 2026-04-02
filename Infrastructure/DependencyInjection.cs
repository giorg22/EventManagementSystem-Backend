using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // SQL Server / DbContext რეგისტრაცია
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));


        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IEventService, EventService>();
        services.AddScoped<ITicketService, TicketService>();
        services.AddScoped<IAnalyticsService, AnalyticsService>();


        // Repositories (BaseRepository-ს მემკვიდრეები)
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<ITicketRepository, TicketRepository>();
        services.AddScoped<IParticipantRepository, ParticipantRepository>();
        services.AddScoped<IPurchaseRepository, PurchaseRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IHallRepository, HallReposotory>();
        services.AddScoped<IReviewRepository, ReviewRepository>();

        // გარე სერვისები (Email, Payment, Auth)
        services.AddScoped<IPaymentService, MockPaymentService>();
        services.AddScoped<IFileService, FileService>();
        // services.AddScoped<IEmailService, EmailService>(); // თუ გაქვთ იმპლემენტაცია

        return services;
    }
}