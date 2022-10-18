using System.Net.NetworkInformation;

namespace DTKSoftwareInDotnet.Common.Session
{
    public class SessionService : ISessionService
    {
        public string GetCurrentDeviceMacAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String macAddress = string.Empty;

            foreach (NetworkInterface adapter in nics)
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();
                macAddress = adapter.GetPhysicalAddress().ToString();
                break;
            }

            return macAddress;
        }
    }
}
