using System.Management;

namespace MAS_v2.Security
{
    public class HardwareID
    {
        public string GetID()
        {
            var mbs = new ManagementObjectSearcher("Select ProcessorId From Win32_processor");
            var mbsList = mbs.Get();
            var id = "";
            foreach (ManagementObject mo in mbsList)
            {
                id = mo["ProcessorId"].ToString();
                break;
            }

            return id;
        }
    }
}