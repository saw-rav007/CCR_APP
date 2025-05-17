using CCR.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CCR.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
}
