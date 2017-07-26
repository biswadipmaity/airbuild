
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

        public static Dictionary<string, bool> PPDevice = new Dictionary<string, bool>
        {
            { "aa:bb:cc:dd:ee", true },
            { "bb:cc:dd:ee:aa", false },
            { "cc:dd:ee:aa:bb", false }
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
            if (PPDevice.ContainsKey(mac)) 
            {
                return PPDevice[mac];
            }

            return false;
        }
    }
}