using Microsoft.EntityFrameworkCore;
using ToDoApp.Domain;

namespace ToDoApp.Infrastructure.Presistence;

public class ToDoDbContext : DbContext
{
    public ToDoDbContext(DbContextOptions<ToDoDbContext> options) 
        : base(options)
    {
    }

    public DbSet<Taske>? Taske { get; set; }
    public DbSet<Person>? Person { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ToDoDbContext).Assembly);

        modelBuilder.Entity<Taske>().ToTable("tasks");
        modelBuilder.Entity<Person>().ToTable("persons");

        modelBuilder.Entity<Taske>()
        .HasOne(t => t.Person)
        .WithMany(p => p.Taske)
        .HasForeignKey(t => t.PersonId)
         .IsRequired(false); 

    }
}