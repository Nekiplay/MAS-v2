using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;

namespace MAS_v2.Security
{
    public class SecurityManager
    {
        public Settings settings = new Settings();

        public class Settings
        {
            public int FakeMenu = 2;
            public string Password = "123123";
        }
        public void LoadCFG()
        {
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey software = currentUserKey.OpenSubKey("SOFTWARE", true);
            if (currentUserKey.OpenSubKey("MAS") == null)
            {
                software.CreateSubKey("MAS");
            }
            RegistryKey mas = software.OpenSubKey("MAS", true);
            if (mas.GetValue("Security Settings") != null)
            {
                settings = JsonConvert.DeserializeObject<Settings>(mas.GetValue("Security Settings").ToString());
            }
        }
        public void SaveCFG()
        {
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey software = currentUserKey.OpenSubKey("SOFTWARE", true);
            if (currentUserKey.OpenSubKey("MAS") == null)
            {
                software.CreateSubKey("MAS");
            }
            RegistryKey mas = software.OpenSubKey("MAS", true);

            File.Create("temp.txt").Close();

            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;
            serializer.Formatting = Formatting.Indented;
            using (StreamWriter sw = new StreamWriter("temp.txt"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, settings);
            }
            using (StreamReader sr = new StreamReader("temp.txt"))
            {
                mas.SetValue("Security Settings", sr.ReadToEnd());
            }
            File.Delete("temp.txt");
        }
    }
}
