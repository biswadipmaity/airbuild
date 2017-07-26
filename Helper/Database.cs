
namespace Server.Helper
{
    using Server.TypeDefinition;
    using System.Collections.Generic;

    public static class Database{ 
        public static IList<Device> Devices = new List<Device>();

        public static Dictionary<string, string> Nicknames = new Dictionary<string, string>
        {
            { "aa:bb:cc:dd:ee", "Device-Biswadip" },
            { "bb:cc:dd:ee:aa", "Device-Nagaraju" },
            { "cc:dd:ee:aa:bb", "Device-Vishal" }
        };

        public static string getNickName(string mac)
        {
            if (Nicknames.ContainsKey(mac)) 
            {
                return Nicknames[mac];
            }

            return string.Empty;
        }

        public static bool isPPDevice(string mac)
        {
            foreach(var device in Devices)
            {
                if(device.MacID.Equals(mac))
                {
                    return device.isPreProductionDevice;
                }
            }
            return false;
        }

        public static bool hasMacId(string mac)
        {
            foreach(var device in Devices)
            {
                if(device.MacID.Equals(mac))
                {
                    return true;
                }
            }

            return false;
        }

        public static void switchEnvironmnet(string mac)
        {
            foreach(var device in Devices)
            {
                if(device.MacID.Equals(mac))
                {
                    device.isPreProductionDevice = !device.isPreProductionDevice;
                    return;
                }
            }

            return;
        }
    }
}