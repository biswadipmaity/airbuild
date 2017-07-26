
namespace Server.Helper
{
    using Server.TypeDefinition;
    using System.Collections.Generic;

    public static class Database{ 
        public static IList<Device> Devices = new List<Device>();

        public static string getNickName(string mac)
        {
            foreach(var device in Devices)
            {
                if(device.MacID.Equals(mac))
                {
                    return device.Nickname;
                }
            }

            return "-";
        }

        public static void setNickName(string mac, string nick)
        {
            foreach(var device in Devices)
            {
                if(device.MacID.Equals(mac))
                {
                    device.Nickname = nick;
                    break;
                }
            }

            return;
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

        public static Device.DeviceStatus getStatus(string mac)
        {
            foreach(var device in Devices)
            {
                if(device.MacID.Equals(mac))
                {
                    return device.currentStatus;
                }
            }

            return Device.DeviceStatus.Idle;
        }

        public static void setStatus(string mac,Device.DeviceStatus status)
        {
            foreach(var device in Devices)
            {
                if(device.MacID.Equals(mac))
                {
                    device.currentStatus = status;
                    return;
                }
            }

            return;
        }

        public static void updateVersion(string mac, string version)
        {
            foreach(var device in Devices)
            {
                if(device.MacID.Equals(mac))
                {
                    device.currentBuild = version;
                    return;
                }
            }

            return;
        }
    }
}