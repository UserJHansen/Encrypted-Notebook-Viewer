using System;
using System.Windows.Forms;

namespace Encrypted_Notebook
{
	public partial class SettingsUI : Form
	{
		public SettingsUI()
		{
			InitializeComponent();
		}

		private void IterationAmount_ValueChanged(object sender, EventArgs e)
		{
			Settings.Default["Iterations"] = (int)IterationAmount.Value;
		}

		private void SettingsUI_Closed(object sender, FormClosedEventArgs e)
		{
			Settings.Default.Save();
		}

		private void KeyFileLocationButton_Click(object sender, EventArgs e)
		{
			KeyFileFileDialog.ShowDialog();

			Settings.Default["KeyFilePath"] = KeyFileFileDialog.FileName;
			KeyFileTextBox.Text = KeyFileFileDialog.FileName;
		}

		private void CreateKeyButton_Click(object sender, EventArgs e)
		{
			new KeyCreator().ShowDialog();
		}

		private void SettingsUI_Load(object sender, EventArgs e)
		{
			IterationAmount.Value = (int)Settings.Default["Iterations"];
			KeyFileTextBox.Text = (string)Settings.Default["KeyFilePath"];
		}
	}
}