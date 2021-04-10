namespace Encrypted_Notebook
{
    public class KeyFile
    {
        public string[] Keys;
    }
    public class EncryptedData
    {
        public string Data;
        public System.Windows.Forms.RichTextBox Box = new System.Windows.Forms.RichTextBox();
    }
    public class EncryptedPage
    {
        public string Title;
        public EncryptedData Contents;
    }
    public class EncrytedNotebook
    {
        public int CurrentPage = 0;
        public System.Collections.Generic.List<EncryptedPage> EncryptedPages = new System.Collections.Generic.List<EncryptedPage>();
    }
    public class NotebookHandler
    {
        public int CurrentPage = 0;
        public DraggableTabControl.DraggableTabControl tabControl = new DraggableTabControl.DraggableTabControl();
        public System.Collections.Generic.List<System.Windows.Forms.TabPage> Pages = new System.Collections.Generic.List<System.Windows.Forms.TabPage>();
        public System.Collections.Generic.List<System.Windows.Forms.RichTextBox> TextBoxes = new System.Collections.Generic.List<System.Windows.Forms.RichTextBox>();
    }
}
