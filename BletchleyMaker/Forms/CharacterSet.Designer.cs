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
            resetChars = new Button();
            SuspendLayout();
            // 
            // characterBox
            // 
            characterBox.Location = new Point(201, 43);
            characterBox.Margin = new Padding(4, 2, 4, 2);
            characterBox.Name = "characterBox";
            characterBox.Size = new Size(559, 39);
            characterBox.TabIndex = 0;
            // 
            // okButton
            // 
            okButton.Location = new Point(429, 96);
            okButton.Margin = new Padding(4, 2, 4, 2);
            okButton.Name = "okButton";
            okButton.Size = new Size(150, 47);
            okButton.TabIndex = 1;
            okButton.Text = "OK";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(587, 96);
            cancelButton.Margin = new Padding(4, 2, 4, 2);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(150, 47);
            cancelButton.TabIndex = 2;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // resetChars
            // 
            resetChars.Location = new Point(20, 43);
            resetChars.Margin = new Padding(4, 2, 4, 2);
            resetChars.Name = "resetChars";
            resetChars.Size = new Size(132, 47);
            resetChars.TabIndex = 3;
            resetChars.Text = "Reset";
            resetChars.UseVisualStyleBackColor = true;
            resetChars.Click += resetChars_Click;
            // 
            // CharacterSet
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(782, 151);
            ControlBox = false;
            Controls.Add(resetChars);
            Controls.Add(cancelButton);
            Controls.Add(okButton);
            Controls.Add(characterBox);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(4, 2, 4, 2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CharacterSet";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Edit Character Set";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox characterBox;
        private Button okButton;
        private Button cancelButton;
        private Button resetChars;
    }
}