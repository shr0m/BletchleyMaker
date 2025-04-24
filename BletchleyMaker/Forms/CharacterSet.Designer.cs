namespace BletchleyMaker.Forms
{
    partial class CharacterSet
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
            characterBox = new TextBox();
            okButton = new Button();
            cancelButton = new Button();
            SuspendLayout();
            // 
            // characterBox
            // 
            characterBox.Location = new Point(75, 38);
            characterBox.Name = "characterBox";
            characterBox.Size = new Size(755, 39);
            characterBox.TabIndex = 0;
            // 
            // okButton
            // 
            okButton.Location = new Point(536, 93);
            okButton.Name = "okButton";
            okButton.Size = new Size(150, 46);
            okButton.TabIndex = 1;
            okButton.Text = "OK";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(716, 93);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(150, 46);
            cancelButton.TabIndex = 2;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // CharacterSet
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(894, 151);
            Controls.Add(cancelButton);
            Controls.Add(okButton);
            Controls.Add(characterBox);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CharacterSet";
            ShowIcon = false;
            Text = "Edit Character Set";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox characterBox;
        private Button okButton;
        private Button cancelButton;
    }
}