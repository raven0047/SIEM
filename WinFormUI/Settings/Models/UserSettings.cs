using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Settings.Models
{
    public class UserSettings
    {
        public string IISLogPath { set; get; }
        public RabbitSettings Rabbit { set; get; }
    }
}
