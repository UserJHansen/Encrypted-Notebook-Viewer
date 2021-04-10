using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Encrypted_Notebook
{
    public partial class MainWindow : Form
    {
        public NotebookHandler Notebook;
        private readonly TopBarEventHandlers TopBarEventHandler = new TopBarEventHandlers();
        public FileInfo CurrentFile;
        public EncryptedData Data;
        public bool IsNotebook;
        public JWC.MruStripMenu StripMenu;
        public MainWindow()
        {
            InitializeComponent();
        }

        #region Font Stuff
        public void ChangeFontStyleForSelectedText(RichTextBox Textbox, string familyName, float? emSize, FontStyle? fontStyle, bool? enableFontStyle)
        {
            int txtStartPosition = Textbox.SelectionStart;
            int selectionLength = Textbox.SelectionLength;
            if (selectionLength > 0)
                using (RichTextBox txtTemp = new RichTextBox())
                {
                    txtTemp.Rtf = Textbox.SelectedRtf;
                    for (int i = 0; i < selectionLength; ++i)
                    {
                        txtTemp.Select(i, 1);
                        txtTemp.SelectionFont = RenderFont(txtTemp.SelectionFont, familyName, emSize, fontStyle, enableFontStyle);
                    }

                    txtTemp.Select(0, selectionLength);
                    Textbox.SelectedRtf = txtTemp.SelectedRtf;
                    Textbox.Select(txtStartPosition, selectionLength);
                }
        }
        private Font RenderFont(Font originalFont, string familyName, float? emSize, FontStyle? fontStyle, bool? enableFontStyle)
        {
            Font newFont;
            FontStyle? newStyle = null;
            if (fontStyle.HasValue)
            {
                if (fontStyle.HasValue && fontStyle == FontStyle.Regular)
                    newStyle = fontStyle.Value;
                else if (originalFont != null && enableFontStyle.HasValue && enableFontStyle.Value)
                    newStyle = originalFont.Style | fontStyle.Value;
                else
                    newStyle = originalFont.Style & ~fontStyle.Value;
            }

            newFont = new Font(!string.IsNullOrEmpty(familyName) ? familyName : originalFont.FontFamily.Name,
                                emSize ?? originalFont.Size,
                                newStyle ?? originalFont.Style);
            return newFont;
        }
        #endregion

        private void MainWindow_Load(object sender, EventArgs e)
        {
            StripMenu = TopBarEventHandler.BindEventHandlers(this);

            if ((string)Settings.Default["KeyFilePath"] == "" || !File.Exists((string)Settings.Default["KeyFilePath"]))
            {
                MessageBox.Show("Please Select or Create a new Key in Settings");
                return;
            }

            if ((string)Settings.Default["CurrentFile"] != "" && (string)Settings.Default["CurrentFile"] != null)
            {
                CurrentFile = new FileInfo((string)Settings.Default["CurrentFile"]);
                if (CurrentFile.Extension == ".jed" || CurrentFile.Extension == ".jen")
                {
                    switch (CurrentFile.Extension)
                    {
                        case ".jed":
                            IsNotebook = false;
                            Data = new EncryptedData
                            {
                                Data = Encryption.Decrypt(File.ReadAllText((string)Settings.Default["CurrentFile"]), JsonConvert.DeserializeObject<KeyFile>(File.ReadAllText((string)Settings.Default["KeyFilePath"])), (int)Settings.Default["Iterations"])
                            };

                            LoadFile(Data);
                            break;
                        case ".jen":
                            IsNotebook = true;
                            EncrytedNotebook Notebook = JsonConvert.DeserializeObject<EncrytedNotebook>(Encryption.Decrypt(File.ReadAllText((string)Settings.Default["CurrentFile"]), JsonConvert.DeserializeObject<KeyFile>(File.ReadAllText((string)Settings.Default["KeyFilePath"])), (int)Settings.Default["Iterations"]));

                            LoadNotebook(Notebook);
                            break;
                    }
                }
            }
        }
        public void CreateEmptyTab(string Name)
        {
            Notebook.Pages.Add(new TabPage());
            Notebook.TextBoxes.Add(new RichTextBox());
            int i = Notebook.Pages.Count - 1;
            Notebook.Pages[i].Text = Name;
            Notebook.Pages[i].UseVisualStyleBackColor = true;
            Notebook.Pages[i].Controls.Add(this.Notebook.TextBoxes[i]);
            Notebook.TextBoxes[i].Dock = DockStyle.Fill;
            Notebook.tabControl.Controls.Add(this.Notebook.Pages[i]);
        }
        public void LoadNotebook(EncrytedNotebook Notebook)
        {
            StripMenu.AddFile((string)Settings.Default["CurrentFile"]);
            StripMenu.SaveToRegistry();
            Content.Controls.Clear();
            this.Notebook = new NotebookHandler();
            this.Notebook.tabControl.SelectedIndexChanged += TopBarEventHandler.TabControl_SelectedIndexChanged;

            int i = 0;
            foreach (EncryptedPage Page in Notebook.EncryptedPages)
            {
                this.Notebook.Pages.Add(new TabPage());
                this.Notebook.TextBoxes.Add(new RichTextBox());
                this.Notebook.Pages[i].Text = Page.Title;
                this.Notebook.Pages[i].UseVisualStyleBackColor = true;
                this.Notebook.Pages[i].Controls.Add(this.Notebook.TextBoxes[i]);
                this.Notebook.TextBoxes[i].Rtf = Page.Contents.Data;
                this.Notebook.TextBoxes[i].Dock = DockStyle.Fill;
                this.Notebook.tabControl.Controls.Add(this.Notebook.Pages[i]);

                i++;
            }
            this.Notebook.tabControl.Dock = DockStyle.Fill;
            Content.Controls.Add(this.Notebook.tabControl);
            RenameStripMenuItem.Enabled = true;
            CreateEmptyTab("New Tab");
        }
        public void LoadFile(EncryptedData Data)
        {
            RenameStripMenuItem.Enabled = false;
            StripMenu.AddFile((string)Settings.Default["CurrentFile"]);
            StripMenu.SaveToRegistry();
            this.Data = Data;
            Content.Controls.Clear();

            this.Data.Box.Rtf = Data.Data;
            this.Data.Box.Dock = DockStyle.Fill;

            Content.Controls.Add(Data.Box);
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.Save();
            StripMenu.SaveToRegistry();
        }
    }
}
