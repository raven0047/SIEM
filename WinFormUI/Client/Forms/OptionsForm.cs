using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Settings;
using Settings.Models;

namespace Client.Forms
{
    public partial class OptionsForm : Form
    {
        SettingsHandler Settings { set; get; }
        public OptionsForm()
        {
            InitializeComponent();
             Settings = new SettingsHandler();

            if (Settings.Rabbit != null)
            {
                if (Settings.Rabbit.IpServer != null && Settings.Rabbit.IpServer != "")
                {
                    textBox1.Text = Settings.Rabbit.IpServer;
                }
                if (Settings.Rabbit.QueueName != null && Settings.Rabbit.QueueName != "")
                {
                    textBox2.Text = Settings.Rabbit.QueueName;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var ip = textBox1.Text;
            var queue = textBox2.Text;
            RabbitSettings rabbit = new RabbitSettings() { IpServer = ip, QueueName = queue };
            Settings.Rabbit = rabbit;
            await Settings.SaveSettings();
            this.Close();
        }
    }
}
