using Microsoft.Extensions.DependencyInjection;

namespace ToDoApp.Aplication;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services
        )
    {
        services.AddMediatR(config =>
              {
                  config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
                 // config.AddOpenBehavior(typeof(ValidationBehavior<,>));
              });
        //services.AddValidatorsFromAssemblies([typeof(DependencyInjection).Assembly]);
        //services.AddValidatorsFromAssemblyContaining<CursoCreateCommand>();
        //services.AddAutoMapper(typeof(MappingProfile).Assembly);
        return services;
    }
}