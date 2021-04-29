using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Client.Models;

namespace Client
{
    public class SettingsHandler
    {
        UserSettings _userSettings;

        public string IISLogPath { set
            {
                _userSettings.IISLogPath = value;
            }
            get
            {
                return _userSettings.IISLogPath;
            }
        }

        public SettingsHandler()
        {
            LoadSettings();
        }

        void LoadSettings()
        {
            string json;
            using (StreamReader fs = new StreamReader("UserSettings.json"))
            {
                json = fs.ReadToEnd();               
            }
            _userSettings = JsonSerializer.Deserialize<UserSettings>(json);
        }

       public  async Task SaveSettings()
        {
            using (FileStream fs = new FileStream("UserSettings.json", FileMode.Create))
            {
                await JsonSerializer.SerializeAsync<UserSettings>(fs, _userSettings);
            }
        }
    }
}
