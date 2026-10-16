using Microsoft.Extensions.DependencyInjection;
using OSK.Petra.Acquisitions;

namespace OSK.Petra.Godot.Acquisitions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the acquisition system for Godot to the dependency container
    /// </summary>
    /// <param name="services">The services to add dependencies to</param>
    /// <returns>The services for chaining</returns>
    public static IServiceCollection AddGodotAcquisitions(this IServiceCollection services)
    {
        services.AddAcquisitions();

        return services;
    }
}
