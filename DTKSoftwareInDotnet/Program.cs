using DTKSoftwareInDotnet.Extensions;
using DTKSoftwareInDotnet.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder()
    .ConfigureServices((context, services) =>
    {
        services.RegisterCustomServices();
    })
    .Build();

IBaseVideoStreamHandler videoStreamHandler = ActivatorUtilities.CreateInstance<DTKVideoStreamHandler>(host.Services);
videoStreamHandler.Process();

Console.ReadKey();
