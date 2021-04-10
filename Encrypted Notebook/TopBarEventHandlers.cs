using System;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Forms;

namespace Encrypted_Notebook
{
    class TopBarEventHandlers
    {
        private MainWindow UI;
        public JWC.MruStripMenu stripMenu;

        #region All the Event Handlers
        public JWC.MruStripMenu BindEventHandlers(MainWindow UI)
        {
            this.UI = UI;
            UI.SettingsStripMenuItem.Click += SettingsStripMenuItem_Click;
            UI.exitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;
            UI.selectAllToolStripMenuItem.Click += SelectAllToolStripMenuItem_Click;
            UI.copyToolStripMenuItem.Click += CopyToolStripMenuItem_Click;
            UI.cutToolStripMenuItem.Click += CutToolStripMenuItem_Click;
            UI.FormattingStripMenuItem.Click += FormattingStripMenuItem_Click;
            UI.fontDialog.Apply += FontDialog_Apply;
            UI.openToolStripMenuItem.Click += OpenToolStripMenuItem_Click;
            UI.newKeyStripMenuItem.Click += NewKeyStripMenuItem_Click;
            UI.newFileStripMenuItem4.Click += NewFileStripMenuItem4_Click;
            UI.NewNotebookStripMenuItem3.Click += NewNotebookStripMenuItem3_Click;
            UI.saveToolStripMenuItem.Click += SaveToolStripMenuItem_Click;
            UI.RenameStripMenuItem.Click += RenameStripMenuItem_Click;

            stripMenu = new JWC.MruStripMenu(UI.RecentFilesStripMenuItem, ClickedHandler, "SOFTWARE\\EncryptedNotebook\\RecentFiles", true, 5);
            return stripMenu;
        }

        private void RenameStripMenuItem_Click(object sender, EventArgs e)
        {
            string result = UI.Notebook.Pages[UI.Notebook.CurrentPage].Text;
            if (Input.InputBox("Rename Tab", "Enter The New Tab Name:", ref result) == DialogResult.OK)
            {
                UI.Notebook.Pages[UI.Notebook.CurrentPage].Text = result;
            }
        }

        private void ClickedHandler(int number, string FileName)
        {
            if (FileName == "" || !File.Exists(FileName))
            {
                MessageBox.Show("Please Select or Create a new Key in Settings");
                return;
            }

            Settings.Default["CurrentFile"] = FileName;
            if ((string)Settings.Default["CurrentFile"] != "" && (string)Settings.Default["CurrentFile"] != null)
            {
                FileInfo CurrentFile = new FileInfo(FileName);
                if (CurrentFile.Extension == ".jed" || CurrentFile.Extension == ".jen")
                {
                    switch (CurrentFile.Extension)
                    {
                        case ".jed":
                            UI.IsNotebook = false;
                            UI.Data = new EncryptedData
                            {
                                Data = Encryption.Decrypt(File.ReadAllText((string)Settings.Default["CurrentFile"]), JsonConvert.DeserializeObject<KeyFile>(File.ReadAllText((string)Settings.Default["KeyFilePath"])), (int)Settings.Default["Iterations"])
                            };

                            UI.LoadFile(UI.Data);
                            break;
                        case ".jen":
                            UI.IsNotebook = true;
                            EncrytedNotebook Notebook = JsonConvert.DeserializeObject<EncrytedNotebook>(Encryption.Decrypt(File.ReadAllText((string)Settings.Default["CurrentFile"]), JsonConvert.DeserializeObject<KeyFile>(File.ReadAllText((string)Settings.Default["KeyFilePath"])), (int)Settings.Default["Iterations"]));

                            UI.LoadNotebook(Notebook);
                            break;
                    }
                }
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Notebook.Save(UI);
        }

        private void NewNotebookStripMenuItem3_Click(object sender, EventArgs e)
        {
            UI.NewNotebookFileDialog.ShowDialog();

            EncrytedNotebook notebook = new EncrytedNotebook();

            notebook.EncryptedPages.Add(new EncryptedPage());

            notebook.EncryptedPages[0].Title = "Page 1";
            notebook.EncryptedPages[0].Contents = new EncryptedData
            {
                Data = "",
                Box = null
            };

            if (UI.NewNotebookFileDialog.FileName == null || UI.NewNotebookFileDialog.FileName == "")
                return;

            StreamWriter file = File.CreateText(UI.NewNotebookFileDialog.FileName);
            string jsonString;
            jsonString = JsonConvert.SerializeObject(notebook);
            jsonString = Encryption.Encrypt(jsonString, JsonConvert.DeserializeObject<KeyFile>(File.ReadAllText((string)Settings.Default["KeyFilePath"])), (int)Settings.Default["Iterations"]);
            file.Write(jsonString);
            file.Close();

            Settings.Default["CurrentFile"] = UI.NewNotebookFileDialog.FileName;
            Settings.Default.Save();

            UI.IsNotebook = true;
            EncrytedNotebook Notebook = JsonConvert.DeserializeObject<EncrytedNotebook>(Encryption.Decrypt(File.ReadAllText((string)Settings.Default["CurrentFile"]), JsonConvert.DeserializeObject<KeyFile>(File.ReadAllText((string)Settings.Default["KeyFilePath"])), (int)Settings.Default["Iterations"]));

            UI.LoadNotebook(Notebook);
        }

        private void NewFileStripMenuItem4_Click(object sender, EventArgs e)
        {
            UI.NewFileFileDialog.ShowDialog();

            if (UI.NewFileFileDialog.FileName == null || UI.NewFileFileDialog.FileName == "")
                return;

            StreamWriter file = File.CreateText(UI.NewFileFileDialog.FileName);
            string EncFile = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\nouicompat\\deflang1033{\\fonttbl{\\f0\\fnil Segoe UI;}}\r\n{\\*\\generator Riched20 10.0.19041}\\viewkind4\\uc1 \r\n\\pard\\f0\\fs18\\par\r\n}\r\n";
            EncFile = Encryption.Encrypt(EncFile, JsonConvert.DeserializeObject<KeyFile>(File.ReadAllText((string)Settings.Default["KeyFilePath"])), (int)Settings.Default["Iterations"]);
            file.Write(EncFile);
            file.Close();

            Settings.Default["CurrentFile"] = UI.NewFileFileDialog.FileName;
            Settings.Default.Save();

            UI.IsNotebook = false;
            UI.Data = new EncryptedData
            {
                Data = File.ReadAllText((string)Settings.Default["CurrentFile"])
            };
            UI.Data.Data = Encryption.Decrypt(UI.Data.Data, JsonConvert.DeserializeObject<KeyFile>(File.ReadAllText((string)Settings.Default["KeyFilePath"])), (int)Settings.Default["Iterations"]);

            UI.LoadFile(UI.Data);
        }

        private void NewKeyStripMenuItem_Click(object sender, EventArgs e)
        {
            new KeyCreator().ShowDialog();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI.OpenFileFileDialog.ShowDialog();

            if (UI.OpenFileFileDialog.FileName == "")
                return;

            Settings.Default["CurrentFile"] = UI.OpenFileFileDialog.FileName;
            Settings.Default.Save();

            if ((string)Settings.Default["KeyFilePath"] == "" || !File.Exists((string)Settings.Default["KeyFilePath"]))
            {
                MessageBox.Show("Please Select or Create a new Key in Settings");
                return;
            }

            UI.CurrentFile = new FileInfo((string)Settings.Default["CurrentFile"]);
            switch (UI.CurrentFile.Extension)
            {
                case ".jed":
                    UI.IsNotebook = false;
                    EncryptedData Data = new EncryptedData
                    {
                        Data = File.ReadAllText((string)Settings.Default["CurrentFile"])
                    };
                    Data.Data = Encryption.Decrypt(Data.Data, JsonConvert.DeserializeObject<KeyFile>(File.ReadAllText((string)Settings.Default["KeyFilePath"])), (int)Settings.Default["Iterations"]);

                    UI.LoadFile(Data);
                    break;
                case ".jen":
                    UI.IsNotebook = true;
                    EncrytedNotebook Notebook = JsonConvert.DeserializeObject<EncrytedNotebook>(Encryption.Decrypt(File.ReadAllText((string)Settings.Default["CurrentFile"]), JsonConvert.DeserializeObject<KeyFile>(File.ReadAllText((string)Settings.Default["KeyFilePath"])), (int)Settings.Default["Iterations"]));

                    UI.LoadNotebook(Notebook);
                    break;
            }
        }

        private void FontDialog_Apply(object sender, EventArgs e)
        {
            if (UI.IsNotebook)
            {
                UI.ChangeFontStyleForSelectedText(UI.Notebook.TextBoxes[UI.Notebook.CurrentPage], UI.fontDialog.Font.Name, UI.fontDialog.Font.Size, UI.fontDialog.Font.Style, true);
            }
            else if (UI.Data != null)
            {
                UI.ChangeFontStyleForSelectedText(UI.Data.Box, UI.fontDialog.Font.Name, UI.fontDialog.Font.Size, UI.fontDialog.Font.Style, true);
            }
        }
        private void FormattingStripMenuItem_Click(object sender, EventArgs e)
        {
            if (UI.IsNotebook || UI.Data != null)
            {
                UI.fontDialog.ShowDialog();
                if (UI.IsNotebook)
                {
                    UI.ChangeFontStyleForSelectedText(UI.Notebook.TextBoxes[UI.Notebook.CurrentPage], UI.fontDialog.Font.Name, UI.fontDialog.Font.Size, UI.fontDialog.Font.Style, true);
                }
                else if (UI.Data != null)
                {
                    UI.ChangeFontStyleForSelectedText(UI.Data.Box, UI.fontDialog.Font.Name, UI.fontDialog.Font.Size, UI.fontDialog.Font.Style, true);
                }
            }
        }
        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (UI.IsNotebook)
            {
                Clipboard.SetDataObject(UI.Notebook.TextBoxes[UI.Notebook.CurrentPage].Text);
                UI.Notebook.TextBoxes[UI.Notebook.CurrentPage].Text = "";
            }
            else if (UI.Data != null)
            {
                Clipboard.SetDataObject(UI.Data.Box.Text);
                UI.Data.Box.Text = "";
            }
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (UI.IsNotebook)
            {
                Clipboard.SetDataObject(UI.Notebook.TextBoxes[UI.Notebook.CurrentPage].Text);
            }
            else if (UI.Data != null)
            {
                Clipboard.SetDataObject(UI.Data.Box.Text);
            }
        }

        public void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            UI.Notebook.CurrentPage = UI.Notebook.tabControl.SelectedIndex;
            if (UI.Notebook.CurrentPage == UI.Notebook.Pages.Count - 1)
            {
                string result = "New Tab";
                if (Input.InputBox("New Tab", "Enter New Tab Name:", ref result) == DialogResult.OK)
                {
                    UI.Notebook.Pages[UI.Notebook.Pages.Count - 1].Text = result;
                    UI.CreateEmptyTab("New Tab");
                }
            }
        }

        private void SelectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (UI.IsNotebook)
            {
                UI.Notebook.TextBoxes[UI.Notebook.CurrentPage].SelectAll();
                UI.Notebook.TextBoxes[UI.Notebook.CurrentPage].Focus();
            }
            else if (UI.Data != null)
            {
                UI.Data.Box.SelectAll();
                UI.Data.Box.Focus();
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
            stripMenu.SaveToRegistry();
        }

        private void SettingsStripMenuItem_Click(object sender, EventArgs e)
        {
            new SettingsUI().ShowDialog();
        }
        #endregion
    }
}
