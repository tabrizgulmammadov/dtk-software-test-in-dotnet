using DTKSoftwareInDotnet.Common.Configuration;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace DTKSoftwareInDotnet.Helpers
{
    public class VideoStreamHelper
    {
        public static VideoStreamConfiguration GetVideoStreamConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            var settings = configuration.Get<Settings>();

            return settings.VideoStreamConfiguration;
        }
    }
}
