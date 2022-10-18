using DTKSoftwareInDotnet.Common.Session;
using DTKSoftwareInDotnet.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DTKSoftwareInDotnet.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterCustomServices(this IServiceCollection services)
        {
            services.AddSingleton<ISessionService, SessionService>();
            services.AddSingleton<IBaseVideoStreamHandler, DTKVideoStreamHandler>();
        }
    }
}
