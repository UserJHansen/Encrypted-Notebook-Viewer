namespace Encrypted_Notebook
{
	partial class SettingsUI
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsUI));
            this.label1 = new System.Windows.Forms.Label();
            this.IterationAmount = new System.Windows.Forms.NumericUpDown();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.KeyFileLocationButton = new System.Windows.Forms.Button();
            this.KeyFileTextBox = new System.Windows.Forms.TextBox();
            this.KeyFileFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.CreateKeyButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.IterationAmount)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Iteration Amount";
            this.toolTip1.SetToolTip(this.label1, "The amount of times the encryption will iterate over the data");
            // 
            // IterationAmount
            // 
            this.IterationAmount.Location = new System.Drawing.Point(130, 7);
            this.IterationAmount.Name = "IterationAmount";
            this.IterationAmount.Size = new System.Drawing.Size(120, 23);
            this.IterationAmount.TabIndex = 1;
            this.IterationAmount.ValueChanged += new System.EventHandler(this.IterationAmount_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Key File Location";
            // 
            // KeyFileLocationButton
            // 
            this.KeyFileLocationButton.Location = new System.Drawing.Point(114, 39);
            this.KeyFileLocationButton.Name = "KeyFileLocationButton";
            this.KeyFileLocationButton.Size = new System.Drawing.Size(75, 23);
            this.KeyFileLocationButton.TabIndex = 3;
            this.KeyFileLocationButton.Text = "Select";
            this.KeyFileLocationButton.UseVisualStyleBackColor = true;
            this.KeyFileLocationButton.Click += new System.EventHandler(this.KeyFileLocationButton_Click);
            // 
            // KeyFileTextBox
            // 
            this.KeyFileTextBox.Location = new System.Drawing.Point(12, 68);
            this.KeyFileTextBox.Name = "KeyFileTextBox";
            this.KeyFileTextBox.ReadOnly = true;
            this.KeyFileTextBox.Size = new System.Drawing.Size(258, 23);
            this.KeyFileTextBox.TabIndex = 4;
            // 
            // KeyFileFileDialog
            // 
            this.KeyFileFileDialog.Filter = "James Key File|*.jek";
            // 
            // CreateKeyButton
            // 
            this.CreateKeyButton.Location = new System.Drawing.Point(195, 39);
            this.CreateKeyButton.Name = "CreateKeyButton";
            this.CreateKeyButton.Size = new System.Drawing.Size(75, 23);
            this.CreateKeyButton.TabIndex = 3;
            this.CreateKeyButton.Text = "Create";
            this.CreateKeyButton.UseVisualStyleBackColor = true;
            this.CreateKeyButton.Click += new System.EventHandler(this.CreateKeyButton_Click);
            // 
            // SettingsUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 105);
            this.Controls.Add(this.CreateKeyButton);
            this.Controls.Add(this.KeyFileTextBox);
            this.Controls.Add(this.KeyFileLocationButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.IterationAmount);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsUI";
            this.Text = "Settings";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SettingsUI_Closed);
            this.Load += new System.EventHandler(this.SettingsUI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.IterationAmount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown IterationAmount;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button KeyFileLocationButton;
		private System.Windows.Forms.TextBox KeyFileTextBox;
		private System.Windows.Forms.OpenFileDialog KeyFileFileDialog;
        private System.Windows.Forms.Button CreateKeyButton;
    }
}