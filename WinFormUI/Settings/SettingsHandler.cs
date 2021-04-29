using Settings.Models;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Settings
{
    public class SettingsHandler
    {

        UserSettings _userSettings;

        public string IISLogPath
        {
            set
            {
                _userSettings.IISLogPath = value;
            }
            get
            {
                return _userSettings.IISLogPath;
            }
        }

        public RabbitSettings Rabbit { set
            {
                _userSettings.Rabbit = value;
            }
            get 
            {
                return _userSettings.Rabbit;
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

        public async Task SaveSettings()
        {
            using (FileStream fs = new FileStream("UserSettings.json", FileMode.Create))
            {
                await JsonSerializer.SerializeAsync<UserSettings>(fs, _userSettings);
            }
        }
    }
}

