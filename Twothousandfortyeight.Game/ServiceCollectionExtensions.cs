using Microsoft.Extensions.DependencyInjection;
using Twothousandfortyeight.Game.Services;

namespace Twothousandfortyeight.Game;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection Add2048GameServices(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddSingleton<IGameService, GameService>();
    }
}