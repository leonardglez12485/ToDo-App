using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using ToDoApp.Infrastructure.Presistence;

namespace ToDoApp.Infrastructure;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ToDoDbContext>
{
    public ToDoDbContext CreateDbContext(string[] args)
    {
        // Ruta al proyecto de inicio (ajusta si tu estructura cambia)
        var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../ToDoApp.API");
        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        var optionsBuilder = new DbContextOptionsBuilder<ToDoDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new ToDoDbContext(optionsBuilder.Options);
    }
}