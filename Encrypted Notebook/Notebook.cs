using System;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Forms;

namespace Encrypted_Notebook
{
    class Notebook
    {
        public static void Save(MainWindow UI)
        {
            if ((string)Settings.Default["KeyFilePath"] == "" || !File.Exists((string)Settings.Default["KeyFilePath"]))
            {
                MessageBox.Show("Please Select or Create a new Key in Settings");
                return;
            }

            if ((string)Settings.Default["CurrentFile"] != "")
            {
                FileInfo CurrentFile = new FileInfo((string)Settings.Default["CurrentFile"]);
                if (CurrentFile.Extension == ".jed" || CurrentFile.Extension == ".jen")
                {
                    switch (CurrentFile.Extension)
                    {
                        case ".jed":
                            StreamWriter file = File.CreateText((string)Settings.Default["CurrentFile"]);
                            string Data = UI.Data.Box.Rtf;
                            Data = Encryption.Encrypt(Data, JsonConvert.DeserializeObject<KeyFile>(File.ReadAllText((string)Settings.Default["KeyFilePath"])), (int)Settings.Default["Iterations"]);
                            file.Write(Data);
                            file.Close();

                            break;
                        case ".jen":
                            EncrytedNotebook Buffer = new EncrytedNotebook();
                            int i = 0;
                            foreach (RichTextBox Page in UI.Notebook.TextBoxes)
                            {
                                if (Page.Text != "" || UI.Notebook.Pages[i].Text != "New Tab")
                                {
                                    EncryptedPage PageBuffer = new EncryptedPage
                                    {
                                        Contents = new EncryptedData
                                        {
                                            Data = Page.Rtf,
                                            Box = null
                                        },
                                        Title = UI.Notebook.Pages[i].Text
                                    };
                                    Buffer.EncryptedPages.Add(PageBuffer);
                                }

                                i++;
                            }

                            using (StreamWriter fileHandler = File.CreateText((string)Settings.Default["CurrentFile"]))
                            {
                                string jsonString;
                                jsonString = JsonConvert.SerializeObject(Buffer);
                                jsonString = Encryption.Encrypt(jsonString, JsonConvert.DeserializeObject<KeyFile>(File.ReadAllText((string)Settings.Default["KeyFilePath"])), (int)Settings.Default["Iterations"]);
                                fileHandler.Write(jsonString);
                                fileHandler.Close();
                            }
                            break;
                    }
                } else
                {
                    MessageBox.Show("The File you have open is not a valid File Type");
                    return;
                }
            }
        }
    }
}
