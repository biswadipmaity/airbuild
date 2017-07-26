namespace Server.ViewModel
{
    using Server.TypeDefinition;
    using System.Collections.Generic;
    using Server.Helper;

    public class IndexViewModel
    {
        public IndexViewModel()
        {            
            Devices = Database.Devices;
        }

        public IList<Device> Devices;
    }
}