using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Client.Forms;
using Connectors.IIS;
using Sender;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IISSettings form = new IISSettings();
            form.Show();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            IISConnector iis = new IISConnector();
            await iis.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OptionsForm form = new OptionsForm();
            form.Show();
        }
    }
}
