namespace BletchleyMaker.Forms
{
    partial class AutomationPrompt
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
            automationBox = new TextBox();
            clearAuto = new Button();
            OKButton = new Button();
            cancelButton = new Button();
            SuspendLayout();
            // 
            // automationBox
            // 
            automationBox.Location = new Point(254, 34);
            automationBox.Name = "automationBox";
            automationBox.Size = new Size(453, 39);
            automationBox.TabIndex = 0;
            // 
            // clearAuto
            // 
            clearAuto.Location = new Point(43, 34);
            clearAuto.Name = "clearAuto";
            clearAuto.Size = new Size(139, 46);
            clearAuto.TabIndex = 1;
            clearAuto.Text = "Clear";
            clearAuto.UseVisualStyleBackColor = true;
            clearAuto.Click += clearAuto_Click;
            // 
            // OKButton
            // 
            OKButton.Location = new Point(380, 82);
            OKButton.Name = "OKButton";
            OKButton.Size = new Size(150, 46);
            OKButton.TabIndex = 2;
            OKButton.Text = "OK";
            OKButton.UseVisualStyleBackColor = true;
            OKButton.Click += OKButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(557, 82);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(150, 46);
            cancelButton.TabIndex = 3;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // AutomationPrompt
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(731, 140);
            ControlBox = false;
            Controls.Add(cancelButton);
            Controls.Add(OKButton);
            Controls.Add(clearAuto);
            Controls.Add(automationBox);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AutomationPrompt";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Edit Automation";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox automationBox;
        private Button clearAuto;
        private Button OKButton;
        private Button cancelButton;
    }
}