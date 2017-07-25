using Server.TypeDefinition;

namespace Server.ViewModel
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
            Device d1 = new Device("aa:bb:cc:dd:ee","Device-Biswadip",true,true,"001");
            Device d2 = new Device("bb:cc:dd:ee:aa","Device-Nagaraju",true,true,"001");
            Device d3 = new Device("cc:dd:ee:aa:bb","Device-Vishal",true,true,"001");
            Devices = new Devices()
        }

        public IList<Device> Devices;
    }
}