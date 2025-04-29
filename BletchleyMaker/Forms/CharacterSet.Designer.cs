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
            characterBox.Location = new Point(108, 20);
            characterBox.Margin = new Padding(2, 1, 2, 1);
            characterBox.Name = "characterBox";
            characterBox.Size = new Size(303, 23);
            characterBox.TabIndex = 0;
            characterBox.TextChanged += characterBox_TextChanged;
            // 
            // okButton
            // 
            okButton.Location = new Point(231, 45);
            okButton.Margin = new Padding(2, 1, 2, 1);
            okButton.Name = "okButton";
            okButton.Size = new Size(81, 22);
            okButton.TabIndex = 1;
            okButton.Text = "OK";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(316, 45);
            cancelButton.Margin = new Padding(2, 1, 2, 1);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(81, 22);
            cancelButton.TabIndex = 2;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // resetChars
            // 
            resetChars.Location = new Point(11, 20);
            resetChars.Margin = new Padding(2, 1, 2, 1);
            resetChars.Name = "resetChars";
            resetChars.Size = new Size(71, 22);
            resetChars.TabIndex = 3;
            resetChars.Text = "Reset";
            resetChars.UseVisualStyleBackColor = true;
            resetChars.Click += resetChars_Click;
            // 
            // CharacterSet
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(421, 94);
            Controls.Add(resetChars);
            Controls.Add(cancelButton);
            Controls.Add(okButton);
            Controls.Add(characterBox);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(2, 1, 2, 1);
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