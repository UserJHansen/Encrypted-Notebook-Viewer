using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Encrypted_Notebook
{
    public partial class KeyCreator : Form
    {
        string keylocation;
        readonly KeyFile keyFile = new KeyFile();
        public KeyCreator()
        {
            InitializeComponent();
        }
        public string[] CreateKeys(int length, int Amount)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*";
            string[] str = new string[Amount];
            StringBuilder[] res = new StringBuilder[Amount];
            for (int i = 0; i < res.Length; i++)
                res[i] = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
                str[i] = "";
            Random rnd = new Random();
            int tmplength = length;
            for (int i = 0; i < res.Length; i++)
            {
                while (0 < tmplength--)
                {
                    res[i].Append(valid[rnd.Next(valid.Length)]);
                }
                tmplength = length;
                str[i] = res[i].ToString();
            }
            return str;
        }

        private void KeyFileLocBtn_Click(object sender, EventArgs e)
        {
            KeyFileSaveDialog.ShowDialog();
            keylocation = KeyFileSaveDialog.FileName;
            KeyLocTxtBox.Text = KeyFileSaveDialog.FileName;
        }

        private void KeyCreator_Load(object sender, EventArgs e)
        {
            KeyFiles.Items.Clear();
            keyFile.Keys = CreateKeys(50, (int)Keycount.Value);
            for (int i = 0; i < Keycount.Value; i++)
            {
                KeyFiles.Items.Add(keyFile.Keys[i]);
            }
        }

        private void Keycount_ValueChanged(object sender, EventArgs e)
        {
            KeyFiles.Items.Clear();
            keyFile.Keys = CreateKeys(50, (int)Keycount.Value);
            for (int i = 0; i < Keycount.Value; i++)
            {
                KeyFiles.Items.Add(keyFile.Keys[i]);
            }
        }

        private void KeyRandomiseBtn_Click(object sender, EventArgs e)
        {
            KeyFiles.Items.Clear();
            keyFile.Keys = CreateKeys(50, (int)Keycount.Value);
            for (int i = 0; i < Keycount.Value; i++)
            {
                KeyFiles.Items.Add(keyFile.Keys[i]);
            }
        }

        private void KeyFileCreateBtn_Click(object sender, EventArgs e)
        {
            if (keylocation == "" || keylocation == null)
            {
                MessageBox.Show("Please set a key Location");
                return;
            }
            using (StreamWriter file = File.CreateText(keylocation))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, keyFile);
            }

            Settings.Default["KeyFilePath"] = keylocation;

            Close();
        }
    }
}
