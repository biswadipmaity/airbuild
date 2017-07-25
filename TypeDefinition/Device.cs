namespace Server.TypeDefinition
{
    public class Device
    {
        public Device(string MacID,
            string Nickname,
            boolean isPreProductionDevice,
            boolean isDevDevice,
            string currentBuild)
        {
            this.MacID = MacID;
            this.Nickname = Nickname;
            this.isPreProductionDevice = isPreProductionDevice;
            this.isDevDevice = isDevDevice;
            this.currentBuild = currentBuild;
        }

        public string MacID;

        public string Nickname;

        public boolean isPreProductionDevice;

        public boolean isDevDevice;

        public string currentBuild;
    }
}