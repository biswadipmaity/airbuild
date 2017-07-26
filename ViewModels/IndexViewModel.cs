namespace Server.ViewModel
{
    using Server.TypeDefinition;
    using System.Collections.Generic;
    using Server.Helper;

    public class IndexViewModel
    {
        public IndexViewModel()
        {
            //Device d1 = new Device("aa:bb:cc:dd:ee","Device-Biswadip",true,"001");
            //Device d2 = new Device("bb:cc:dd:ee:aa","Device-Nagaraju",false,"002");
            //Device d3 = new Device("cc:dd:ee:aa:bb","Device-Vishal",false,"003");
            //Devices = new List<Device>{d1, d2, d3};
            
            Devices = Database.Devices;
        }

        public IList<Device> Devices;
    }
}