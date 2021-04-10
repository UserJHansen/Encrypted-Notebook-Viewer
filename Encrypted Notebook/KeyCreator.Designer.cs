namespace Encrypted_Notebook
{
    partial class KeyCreator
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KeyCreator));
            this.KeyFileCreateBtn = new System.Windows.Forms.Button();
            this.KeyFiles = new System.Windows.Forms.ListBox();
            this.KeyLocTxtBox = new System.Windows.Forms.TextBox();
            this.KeyFileLocBtn = new System.Windows.Forms.Button();
            this.KeyRandomiseBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Keycount = new System.Windows.Forms.NumericUpDown();
            this.KeyFileSaveDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.Keycount)).BeginInit();
            this.SuspendLayout();
            // 
            // KeyFileCreateBtn
            // 
            this.KeyFileCreateBtn.Location = new System.Drawing.Point(201, 197);
            this.KeyFileCreateBtn.Name = "KeyFileCreateBtn";
            this.KeyFileCreateBtn.Size = new System.Drawing.Size(187, 34);
            this.KeyFileCreateBtn.TabIndex = 0;
            this.KeyFileCreateBtn.Text = "Generate Key File";
            this.KeyFileCreateBtn.UseVisualStyleBackColor = true;
            this.KeyFileCreateBtn.Click += new System.EventHandler(this.KeyFileCreateBtn_Click);
            // 
            // KeyFiles
            // 
            this.KeyFiles.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.KeyFiles.FormattingEnabled = true;
            this.KeyFiles.ItemHeight = 15;
            this.KeyFiles.Location = new System.Drawing.Point(12, 62);
            this.KeyFiles.Name = "KeyFiles";
            this.KeyFiles.Size = new System.Drawing.Size(376, 120);
            this.KeyFiles.TabIndex = 1;
            // 
            // KeyLocTxtBox
            // 
            this.KeyLocTxtBox.Enabled = false;
            this.KeyLocTxtBox.Location = new System.Drawing.Point(12, 12);
            this.KeyLocTxtBox.Name = "KeyLocTxtBox";
            this.KeyLocTxtBox.Size = new System.Drawing.Size(209, 23);
            this.KeyLocTxtBox.TabIndex = 2;
            // 
            // KeyFileLocBtn
            // 
            this.KeyFileLocBtn.Location = new System.Drawing.Point(229, 12);
            this.KeyFileLocBtn.Name = "KeyFileLocBtn";
            this.KeyFileLocBtn.Size = new System.Drawing.Size(161, 23);
            this.KeyFileLocBtn.TabIndex = 3;
            this.KeyFileLocBtn.Text = "Select Key File Location";
            this.KeyFileLocBtn.UseVisualStyleBackColor = true;
            this.KeyFileLocBtn.Click += new System.EventHandler(this.KeyFileLocBtn_Click);
            // 
            // KeyRandomiseBtn
            // 
            this.KeyRandomiseBtn.Location = new System.Drawing.Point(12, 197);
            this.KeyRandomiseBtn.Name = "KeyRandomiseBtn";
            this.KeyRandomiseBtn.Size = new System.Drawing.Size(183, 34);
            this.KeyRandomiseBtn.TabIndex = 4;
            this.KeyRandomiseBtn.Text = "Randomise Keys";
            this.KeyRandomiseBtn.UseVisualStyleBackColor = true;
            this.KeyRandomiseBtn.Click += new System.EventHandler(this.KeyRandomiseBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(13, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 21);
            this.label1.TabIndex = 5;
            this.label1.Text = "Current Keys";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(142, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 19);
            this.label2.TabIndex = 6;
            this.label2.Text = "Amount of Keys";
            // 
            // Keycount
            // 
            this.Keycount.Location = new System.Drawing.Point(255, 36);
            this.Keycount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Keycount.Name = "Keycount";
            this.Keycount.Size = new System.Drawing.Size(120, 23);
            this.Keycount.TabIndex = 7;
            this.Keycount.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.Keycount.ValueChanged += new System.EventHandler(this.Keycount_ValueChanged);
            // 
            // KeyFileSaveDialog
            // 
            this.KeyFileSaveDialog.Filter = "James Key File|*.jek";
            // 
            // KeyCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 242);
            this.Controls.Add(this.Keycount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.KeyRandomiseBtn);
            this.Controls.Add(this.KeyFileLocBtn);
            this.Controls.Add(this.KeyLocTxtBox);
            this.Controls.Add(this.KeyFiles);
            this.Controls.Add(this.KeyFileCreateBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "KeyCreator";
            this.Text = "KeyCreator";
            this.Load += new System.EventHandler(this.KeyCreator_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Keycount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button KeyFileCreateBtn;
        private System.Windows.Forms.ListBox KeyFiles;
        private System.Windows.Forms.TextBox KeyLocTxtBox;
        private System.Windows.Forms.Button KeyFileLocBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button KeyRandomiseBtn;
        private System.Windows.Forms.NumericUpDown Keycount;
        private System.Windows.Forms.SaveFileDialog KeyFileSaveDialog;
    }
}