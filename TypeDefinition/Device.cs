namespace Server.TypeDefinition
{
    using System;

    public class Device
    {
        public enum DeviceStatus{
            Idle,
            UpdateStarted,
            UpdateFinished
        };
        
        public Device(string MacID,
            string Nickname,
            bool isPreProductionDevice,
            string currentBuild)
        {
            this.MacID = MacID;
            this.Nickname = Nickname;
            this.isPreProductionDevice = isPreProductionDevice;
            this.currentBuild = currentBuild;
            this.currentStatus = DeviceStatus.Idle;
        }

        public string MacID;

        public string Nickname;

        public bool isPreProductionDevice;

        public string currentBuild;

        public DeviceStatus currentStatus;
    }
}