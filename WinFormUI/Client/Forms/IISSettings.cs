using Settings;
using System;
using System.Windows.Forms;

namespace Client.Forms
{
    public partial class IISSettings : Form
    {
        private string _iisLogs;
        SettingsHandler _settings;
        public IISSettings()
        {
            InitializeComponent();
            _settings = new SettingsHandler();

            if (_settings.IISLogPath != null && _settings.IISLogPath != "")
            {
                PathTextBox.Text = _settings.IISLogPath;
                _iisLogs = _settings.IISLogPath;
            }
        }

        private void CencelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BrowseBtn_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            _iisLogs = openFileDialog1.FileName;
            PathTextBox.Text = _iisLogs;
        }

        private async void SaveBtn_Click(object sender, EventArgs e)
        {
            if (_iisLogs != null && _iisLogs != "")
            {
                _settings.IISLogPath = _iisLogs;
                await _settings.SaveSettings();
                this.Close();
                return;
            }
            else MessageBox.Show("Path is empty", "Error", MessageBoxButtons.OK);           
        }

    }
}
