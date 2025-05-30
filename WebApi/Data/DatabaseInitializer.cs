namespace WebApi.Data;

public class DatabaseInitializer(IServiceProvider sp) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await using var scope = sp.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await db.Database.EnsureDeletedAsync(stoppingToken);
        if (await db.Database.EnsureCreatedAsync(stoppingToken))
        {
            for (int i = 1000; i < 1100; i++)
            {
                db.Courses.Add(new Course
                {
                    Id = $"work{i}",
                    Slug = $"workshop-{i}",
                    Name = $"Workshop {i}",
                    StartDate = DateTime.UtcNow.AddDays(i),
                    MaxCapacity = 20,
                    Price = Random.Shared.Next(999,9999)
                });
            }

            await db.SaveChangesAsync(stoppingToken);
        }
    }
}