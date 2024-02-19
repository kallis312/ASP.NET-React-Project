using Microsoft.EntityFrameworkCore;

namespace MyApp.Models;

public class DatabaseContext : DbContext
{
  public DatabaseContext(DbContextOptions<DatabaseContext> options)
      : base(options) { }

  public DbSet<Todo> Todos => Set<Todo>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Todo>()
        .Property(e => e.CreatedAt)
        .HasDefaultValueSql("now()");
  }
}