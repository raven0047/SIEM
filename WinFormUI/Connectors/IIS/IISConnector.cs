using Sender;
using Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Connectors.IIS
{
    public class IISConnector
    {   string LogPath { set; get; }
        long LastFileSize { set; get; }
        RabbitMQSender Rabbit { set; get; }
        public IISConnector()
        {
            SettingsHandler settings = new SettingsHandler();
            LogPath = settings.IISLogPath;
            Rabbit = new RabbitMQSender("IISConnector");
        }

        public async Task Start()
        {
            FileInfo file = new FileInfo(LogPath);

            using (FileStream fs = new FileStream(LogPath, FileMode.Open,
                    FileAccess.Read, FileShare.ReadWrite))
            {
                LastFileSize = fs.Length;
            }
            while (true) {

                List<string> messageList = new List<string>();

                using (FileStream fs = new FileStream(LogPath, FileMode.Open,
                       FileAccess.Read, FileShare.ReadWrite))
                {
                    // Set stream position
                    long newFileSize = fs.Length;
                    if (newFileSize >= LastFileSize) fs.Position = LastFileSize;
                    if (newFileSize < LastFileSize) LastFileSize = 0; // If the file was overwritten

                    // Read last lines
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        string newFileLines = null;
                        newFileLines = sr.ReadToEnd();
                        if (newFileLines != null)
                        {
                            LastFileSize = newFileSize;
                            if (newFileLines.Length > 0)
                                messageList.AddRange(newFileLines.Split(new string[] { Environment.NewLine },
                                    StringSplitOptions.RemoveEmptyEntries));
                        }
                    }
                }
                if (messageList.Count > 0) Rabbit.Send(messageList);
                await Task.Delay(1000);
            }
        }

 
    }
}
