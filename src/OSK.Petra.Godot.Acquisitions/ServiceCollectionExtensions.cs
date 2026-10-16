using Microsoft.Extensions.DependencyInjection;
using OSK.Petra.Acquisitions;

namespace OSK.Petra.Godot.Acquisitions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGodotAcquisitions(this IServiceCollection services)
    {
        services.AddAcquisitions();

        return services;
    }
}
