namespace BletchleyMaker
{
    partial class ViewCodes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewCodes));
            clearAll = new Button();
            removeSel = new Button();
            SuspendLayout();
            // 
            // clearAll
            // 
            clearAll.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            clearAll.Location = new Point(342, 315);
            clearAll.Name = "clearAll";
            clearAll.Size = new Size(75, 23);
            clearAll.TabIndex = 0;
            clearAll.Text = "Clear All";
            clearAll.UseVisualStyleBackColor = true;
            clearAll.Click += clearAll_Click;
            // 
            // removeSel
            // 
            removeSel.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            removeSel.Location = new Point(200, 315);
            removeSel.Name = "removeSel";
            removeSel.Size = new Size(124, 23);
            removeSel.TabIndex = 1;
            removeSel.Text = "Remove Selected";
            removeSel.UseVisualStyleBackColor = true;
            removeSel.Click += removeSel_Click;
            // 
            // ViewCodes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(428, 346);
            Controls.Add(removeSel);
            Controls.Add(clearAll);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "ViewCodes";
            Text = "ViewCodes";
            ResumeLayout(false);
        }

        #endregion

        private Button clearAll;
        private Button removeSel;
    }
}