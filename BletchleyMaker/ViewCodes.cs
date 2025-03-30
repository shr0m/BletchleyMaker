using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BletchleyMaker
{
    public partial class ViewCodes : Form
    {
        int currentPos = 0;
        List<CheckBox> addCodes = new List<CheckBox>();
        private Form1 mainForm;
        public ViewCodes(List<string> savedCodes, Form1 form)
        {
            InitializeComponent();

            mainForm = form;

            for (int i = 0; i < savedCodes.Count; i++)
            {
                UpdateForm(savedCodes[i]);
            }
        }

        private void removeSel_Click(object sender, EventArgs e)
        {
            for (int i = addCodes.Count - 1; i >= 0; i--)
            {
                if (addCodes[i].Checked)
                {
                    Controls.Remove(addCodes[i]);
                    addCodes[i].Dispose();
                    addCodes.RemoveAt(i);

                    mainForm.RemoveCode(i);
                }
            }

            this.Close();
        }

        public void UpdateForm(string add)
        {
            CheckBox cb = new CheckBox();
            cb.Text = add;
            cb.Location = new Point(10, 30 * currentPos + 10);
            cb.AutoSize = true;
            addCodes.Add(cb);
            Controls.Add(cb);

            currentPos++;
        }

        private void clearAll_Click(object sender, EventArgs e)
        {
            for (int i = addCodes.Count - 1; i >= 0; i--)
            {
                Controls.Remove(addCodes[i]);
                addCodes[i].Dispose();
                addCodes.RemoveAt(i);
                mainForm.RemoveCode(i);
            }

            this.Close();
        }
    }
}
