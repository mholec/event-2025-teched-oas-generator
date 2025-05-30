using Microsoft.EntityFrameworkCore;

namespace WebApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Course> Courses { get; set; }
}